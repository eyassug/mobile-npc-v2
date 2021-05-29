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

namespace MobileNPC.ViewModels
{
    public class ScanViewModel : BaseViewModel, ITabViewModel 
    {
        private readonly Interaction<string, Unit> notFoundInteration;
        public override string Id => "Scan Barcode";
        private readonly IBarcodeScannerService barcodeScannerService;

        public string TabTitle => Id;
        public ImageSource TabIcon => "";

        public Interaction<string, Unit> ProductNotFoundInteraction => notFoundInteration;
        public ReactiveCommand<Unit, Unit> ScanCommand { get; private set; }

        public ScanViewModel(IViewStackService viewStackService) : base(viewStackService)
        {
            barcodeScannerService = Locator.Current.GetService<IBarcodeScannerService>();

            notFoundInteration = new Interaction<string, Unit>();
            ScanCommand = ReactiveCommand.CreateFromTask(async () =>
            {

                var result = await barcodeScannerService.ReadBarcodeAsync() ?? "1".PadLeft('0');
                if (result != null)
                    await NavigationService.PushPage(new ProductViewModel(ViewStackService), new NavigationParameter { { "parameter", "gtin" } });
            });
        }
    }
}
