using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using MobileNPC.Core.Services;
using System;
using BarcodeScanner;
using Splat;
using Sextant;

namespace MobileNPC.ViewModels
{
    public class ScanViewModel : ViewModelBase
    {
        private readonly Interaction<string, Unit> notFoundInteration;
        private readonly IProductService productService;
        private readonly IGS1ParserService gS1Parser;
        private readonly IBarcodeScannerService barcodeScannerService;
        public ScanViewModel(IViewStackService viewStackService = null) : base(viewStackService)
        {
            UrlPathSegment = "Scan QR";
            barcodeScannerService = Locator.Current.GetService<IBarcodeScannerService>();
            IsScanning = true;
            IsAnalyzing = true;
            
            notFoundInteration = new Interaction<string, Unit>();
            ScanCommand = ReactiveCommand.CreateFromTask(async () => 
            {

                var result = await barcodeScannerService.ReadBarcodeAsync();
                if(result == null)
                {
                    NavigationService.PushPage(new ProductDetailViewModel());
                }
                else
                    NavigationService.PushPage(new ProductDetailViewModel());
            });
            ToggleTorchCommand = ReactiveCommand.Create(() =>
            {
                if (!IsScanning)
                    IsScanning = true;
                IsTorchOn = !IsTorchOn;
            });

        }

        [Reactive]
        public ZXing.Result Result { get; set; }
        [Reactive]
        public bool IsScanning { get; set; }
        [Reactive]
        public bool IsAnalyzing { get; set; }
        [Reactive]
        public bool IsTorchOn { get; set; }
        public Interaction<string, Unit> ProductNotFoundInteraction => notFoundInteration;
        public ReactiveCommand<Unit, Unit> ScanCommand { get; private set; }
        public ReactiveCommand<Unit, Unit> ToggleTorchCommand { get; private set; }
    }
}
