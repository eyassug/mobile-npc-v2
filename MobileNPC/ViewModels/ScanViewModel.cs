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
        public string TabTitle => Id;
        public ImageSource TabIcon => "";

        public Interaction<string, Unit> ProductNotFoundInteraction => notFoundInteration;
        public Interaction<string, Unit> InvalidBarcodeInteraction => invalidBarcodeInteration;
        public ReactiveCommand<Unit, Unit> ScanCommand { get; private set; }

        public ScanViewModel(IViewStackService viewStackService) : base(viewStackService)
        {
            barcodeScannerService = Locator.Current.GetService<IBarcodeScannerService>();
            gS1ParserService = Locator.Current.GetService<IGS1ParserService>();

            notFoundInteration = new Interaction<string, Unit>();
            invalidBarcodeInteration = new Interaction<string, Unit>();
            ScanCommand = ReactiveCommand.CreateFromTask(async () =>
            {

                //var result = await barcodeScannerService.ReadBarcodeAsync() ?? "1".PadLeft('0');
                var barcodeResult = await barcodeScannerService.ReadBarcodeResultAsync();
                if(barcodeResult != null)
                {
                    //var validBarcodeTypes = ZXing.BarcodeFormat.All_1D | ZXing.BarcodeFormat.CODE_128
                    //                        | ZXing.BarcodeFormat.DATA_MATRIX | ZXing.BarcodeFormat.QR_CODE;

                    var gtin = gS1ParserService.GetGTIN(barcodeResult.Text);

                    // Validate GS1 Barcode
                    if(!string.IsNullOrEmpty(gtin))
                        NavigationService.PushPage(new ProductDetailViewModel(ViewStackService), new NavigationParameter { { ProductDetailViewModel.ParameterName, gtin } })
                        .Subscribe()
                        .DisposeWith(Disposables);
                    else
                    {
                        // Invoke InvalidBarcode Interaction
                        //var interactionResult = InvalidBarcodeInteraction.Handle("The barcode you scanned is not a GS1 barcode.");
                    }
                }
                    
            });
        }
    }
}
