namespace MobileNPC
{
    using MobileNPC.ViewModels;
    using MobileNPC.Views;
    using ReactiveUI;
    using ReactiveUI.XamForms;
    using Splat;
    using Xamarin.Forms;

    public class AppBootstrapper : ReactiveObject, IScreen
    {
        public AppBootstrapper(IMutableDependencyResolver dependencyResolver = null, RoutingState router = null)
        {
            
            Router = router ?? new RoutingState();

            RegisterParts(dependencyResolver ?? Locator.CurrentMutable);

            Router.NavigateAndReset.Execute(new AppShellViewModel(hostScreen: this));
        }

        public RoutingState Router { get; private set; }

        private void RegisterParts(IMutableDependencyResolver dependencyResolver)
        {
            dependencyResolver.RegisterConstant(this, typeof(IScreen));
            dependencyResolver.Register(() => new Services.GS1BarcodeScannerService(), typeof(BarcodeScanner.IBarcodeScannerService));

            dependencyResolver.Register(() => new ScanPage(), typeof(IViewFor<ScanViewModel>));
            dependencyResolver.Register(() => new AppShell(), typeof(IViewFor<AppShellViewModel>));
            dependencyResolver.Register(() => new AboutPage(), typeof(IViewFor<AboutViewModel>));
            dependencyResolver.Register(() => new ProductDetailPage(), typeof(IViewFor<ProductDetailViewModel>));
        }

        public Page CreateMainPage()
        {
            return new RoutedViewHost();
        }
    }
}
