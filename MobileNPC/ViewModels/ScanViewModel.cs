using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using MobileNPC.Core.Services;
using System;
using BarcodeScanner;
using Splat;
using Sextant;
using System.Reactive.Linq;
using Xamarin.Forms;
using System.Reactive.Disposables;

namespace MobileNPC.ViewModels
{
    public class ScanViewModel : BaseViewModel, ITabViewModel 
    {
        private readonly Interaction<string, Unit> notFoundInteration;
        private readonly Interaction<string, Unit> invalidBarcodeInteration;
        public override string Id => "Scan Barcode";
        private readonly IBarcodeScannerService barcodeScannerService;
        private readonly IGS1ParserService gS1ParserService;
        private readonly IProductService productService;
        public string TabTitle => Id;
        public ImageSource TabIcon => "";

        public Interaction<string, Unit> ProductNotFoundInteraction => notFoundInteration;
        public Interaction<string, Unit> InvalidBarcodeInteraction => invalidBarcodeInteration;
        public ReactiveCommand<Unit, Unit> ScanCommand { get; private set; }

        public ScanViewModel(IViewStackService viewStackService) : base(viewStackService)
        {
            barcodeScannerService = Locator.Current.GetService<IBarcodeScannerService>();
            gS1ParserService = Locator.Current.GetService<IGS1ParserService>();
            productService = Locator.Current.GetService<IProductService>();
            notFoundInteration = new Interaction<string, Unit>();
            invalidBarcodeInteration = new Interaction<string, Unit>();
            ScanCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var barcodeResult = await barcodeScannerService.ReadBarcodeResultAsync();
                if(barcodeResult != null)
                {
                    var gtin = gS1ParserService.GetGTIN(barcodeResult.Text);

                    // Validate GS1 Barcode
                    if(!string.IsNullOrEmpty(gtin))
                    {
                        var product = await productService.GetAsync(gtin);
                        if(product != null)
                            NavigationService.PushPage(new ProductDetailViewModel(ViewStackService),
                                new NavigationParameter { { ProductDetailViewModel.ParameterName, product } })
                                    .Subscribe()
                                    .DisposeWith(Disposables);
                        else
                        {
                            _ = await ProductNotFoundInteraction.Handle("A product with the specified GTIN could not be found!");
                        }
                    }
                    else
                    {
                        _ = await InvalidBarcodeInteraction.Handle("The barcode you scanned is not a GS1 barcode.");
                    }
                }
                else
                {
                    //_ = await InvalidBarcodeInteraction.Handle("The barcode you scanned is not a GS1 barcode.");
                    //var sampleGtin = "18901213006973";
                    //var product = await productService.GetAsync(sampleGtin);
                    //if (product != null)
                    //    NavigationService.PushPage(new ProductDetailViewModel(ViewStackService),
                    //        new NavigationParameter { { ProductDetailViewModel.ParameterName, product } })
                    //            .Subscribe()
                    //            .DisposeWith(Disposables);
                }
                    
            });
        }
    }
}
