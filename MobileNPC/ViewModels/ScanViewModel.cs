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
using Xamarin.Essentials;

namespace MobileNPC.ViewModels
{
    public class ScanViewModel : BaseViewModel, ITabViewModel 
    {
        private readonly Interaction<string, Unit> notFoundInteration;
        private readonly Interaction<string, Unit> invalidBarcodeInteration;
        private readonly Interaction<string, Unit> notConnectedInteraction;
        private readonly Interaction<string, Unit> permissionNotGrantedInteraction;
        public override string Id => "Scan Barcode";
        private readonly IBarcodeScannerService barcodeScannerService;
        private readonly IGS1ParserService gS1ParserService;
        private readonly IProductService productService;
        public string TabTitle => Id;
        public ImageSource TabIcon => "";

        public Interaction<string, Unit> ProductNotFoundInteraction => notFoundInteration;
        public Interaction<string, Unit> InvalidBarcodeInteraction => invalidBarcodeInteration;
        public Interaction<string, Unit> NotConnectedInteraction => notConnectedInteraction;
        public Interaction<string, Unit> PermissionNotGrantedInteraction => permissionNotGrantedInteraction;
        public ReactiveCommand<Unit, Unit> ScanCommand { get; private set; }

        public ScanViewModel(IViewStackService viewStackService) : base(viewStackService)
        {
            barcodeScannerService = Locator.Current.GetService<IBarcodeScannerService>();
            gS1ParserService = Locator.Current.GetService<IGS1ParserService>();
            productService = Locator.Current.GetService<IProductService>();
            notFoundInteration = new Interaction<string, Unit>();
            invalidBarcodeInteration = new Interaction<string, Unit>();
            notConnectedInteraction = new Interaction<string, Unit>();
            permissionNotGrantedInteraction = new Interaction<string, Unit>();
            ScanCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                // Check permission
                var hasPermission = await AssertPermissionsAsync();
                if(!hasPermission)
                {
                    await PermissionNotGrantedInteraction.Handle("You need to enable camera permissions to scan barcodes");
                    return;
                }
                var barcodeResult = await barcodeScannerService.ReadBarcodeResultAsync();
                if (barcodeResult != null)
                {
                    var gtin = gS1ParserService.GetGTIN(barcodeResult.Text);

                    // Validate GS1 Barcode
                    if (!string.IsNullOrEmpty(gtin))
                    {
                        if (NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                        {
                            _ = await NotConnectedInteraction.Handle("You are not connected to the internet!");
                        }
                        else
                        {
                            var product = await productService.GetAsync(gtin);
                            if (product != null)
                                NavigationService.PushPage(new ProductDetailViewModel(ViewStackService),
                                    new NavigationParameter { { ProductDetailViewModel.ParameterName, product } })
                                        .Subscribe()
                                        .DisposeWith(Disposables);
                            else
                            {
                                _ = await ProductNotFoundInteraction.Handle($"A product with the specified GTIN '{gtin}' could not be found!");
                            }
                        }
                    }
                    else
                    {
                        _ = await InvalidBarcodeInteraction.Handle("The barcode you scanned is not a GS1 barcode.");
                    }
                }
            });
        }

        async Task<bool> AssertPermissionsAsync()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.Camera>();
                if (status == PermissionStatus.Granted) return true;

                return await RequestPermissionAsync() == PermissionStatus.Granted;
            }
            catch (Exception)
            {
                return await RequestPermissionAsync() == PermissionStatus.Granted;
            }
        }

        async Task<PermissionStatus> RequestPermissionAsync()
        {
            return await Permissions.RequestAsync<Permissions.Camera>();
        }

    }
}
