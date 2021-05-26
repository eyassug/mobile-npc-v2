namespace MobileNPC
{
    using System;
    using MobileNPC.ViewModels;
    using MobileNPC.Views;
    using ReactiveUI;
    using ReactiveUI.XamForms;
    using Sextant;
    using Sextant.XamForms;
    using Splat;
    using Xamarin.Forms;
    using static Sextant.Sextant;

    public class AppBootstrapper : ReactiveObject, IScreen
    {
        [Obsolete]
        public AppBootstrapper(IMutableDependencyResolver dependencyResolver = null, RoutingState router = null)
        {
            
            Router = router ?? new RoutingState();
            RxApp.DefaultExceptionHandler = new SextantDefaultExceptionHandler();

            Instance.InitializeForms();

            RegisterParts(dependencyResolver ?? Locator.CurrentMutable);

            //Router.NavigateAndReset.Execute(new AppShellViewModel(hostScreen: this));

            Locator
                .Current
                .GetService<IViewStackService>()
                .PushPage(new AppShellViewModel(null), null, true, false)
                .Subscribe();
        }

        public RoutingState Router { get; private set; }

        private void RegisterParts(IMutableDependencyResolver dependencyResolver)
        {
            //dependencyResolver.RegisterConstant(this, typeof(IScreen));
#pragma warning disable CS0618 // Type or member is obsolete
            _ = Locator.CurrentMutable
                .RegisterNavigationView(() => new MainNavigationView())
                .RegisterParameterViewStackService()
                .RegisterView<TabPage, TabViewModel>()
                .RegisterView<ScanPage, ScanViewModel>()
#pragma warning restore CS0618 // Type or member is obsolete
                .RegisterView<AboutPage, AboutViewModel>()
                .RegisterView<ProductDetailPage, ProductDetailViewModel>()
                .RegisterView<AppShell, AppShellViewModel>();
            dependencyResolver.Register(() => new Services.GS1BarcodeScannerService(), typeof(BarcodeScanner.IBarcodeScannerService));

        }

        public Page CreateMainPage()
        {
            return Locator.Current.GetNavigationView();
        }
    }
}
