namespace MobileNPC.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Reactive;
    using System.Reactive.Linq;
    using ReactiveUI;
    using Sextant;
    using Splat;
    using Xamarin.Forms;

    public class HomeViewModel : BaseViewModel, ITabViewModel, IViewModel
    {
        public override string Id => nameof(HomeViewModel);
        readonly BarcodeScanner.IBarcodeScannerService barcodeScannerService = new Services.GS1BarcodeScannerService();

        public string TabTitle => Id;

        public ImageSource TabIcon => "";

        public HomeViewModel(IViewStackService viewStackService) : base(viewStackService)
        {
            OpenModal = ReactiveCommand.CreateFromTask(async () =>
            {

                var result = await barcodeScannerService.ReadBarcodeAsync();
                if (result != null)
                {
                    await ViewStackService.PushPage(new ProductViewModel(ViewStackService));
                }
                else
                    await NavigationService.PushPage(new ProductViewModel(ViewStackService), new NavigationParameter { { "parameter", "gtin" } });
            }, outputScheduler: RxApp.MainThreadScheduler);

            PushPage = ReactiveCommand
                .CreateFromObservable(() =>
                    ViewStackService.PushPage(new ProductViewModel(ViewStackService)),
                    outputScheduler: RxApp.MainThreadScheduler);

            OpenModal.Subscribe(x => Debug.WriteLine("PagePushed"));

        }

        public ReactiveCommand<Unit, Unit> OpenModal { get; set; }
        
        public ReactiveCommand<Unit, Unit> PushPage { get; set; }
        
    }

    public class HomeNavigationViewModel : ReactiveObject
    {
    }
}
