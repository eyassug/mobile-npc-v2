using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using MobileNPC.Core.Services;
using System;
using BarcodeScanner;
using Splat;

namespace MobileNPC.ViewModels
{
    public class ScanViewModel : ViewModelBase
    {
        private readonly Interaction<string, Unit> notFoundInteration;
        private readonly IProductService productService;
        private readonly IGS1ParserService gS1Parser;
        private readonly IBarcodeScannerService barcodeScannerService;
        public ScanViewModel(IProductService productService = null, IGS1ParserService gS1Parser = null, IScreen hostScreen = null) : base(hostScreen)
        {
            UrlPathSegment = "Scan QR";
            this.productService = productService;
            this.gS1Parser = gS1Parser;
            this.barcodeScannerService = Locator.Current.GetService<IBarcodeScannerService>();
            IsScanning = true;
            IsAnalyzing = true;
            
            notFoundInteration = new Interaction<string, Unit>();
            ScanCommand = ReactiveCommand.CreateFromTask(async () => 
            {

                var result = await barcodeScannerService.ReadBarcodeAsync();
                if(result == null)
                {
                    HostScreen.Router.Navigate.Execute(new ProductDetailViewModel("1".PadLeft(14, '0'))).Subscribe();
                }
                else
                    HostScreen.Router.Navigate.Execute(new ProductDetailViewModel(result.PadLeft(14, '0'))).Subscribe();
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
