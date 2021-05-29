namespace MobileNPC
{
    using System;
    using MobileNPC.ViewModels;
    using MobileNPC.Views;
    using ReactiveUI;
    using Sextant;
    using Sextant.XamForms;
    using Splat;
    using Xamarin.Forms;
    using static Sextant.Sextant;
    public class SextantAppBootstrapper
    {
        public SextantAppBootstrapper()
        {
            RxApp.DefaultExceptionHandler = new SextantDefaultExceptionHandler();

            Instance.InitializeForms();

#pragma warning disable CS0612 // Type or member is obsolete
            RegisterParts(Locator.CurrentMutable);
#pragma warning restore CS0612 // Type or member is obsolete
            Initialize(Locator.Current);
            
        }

        [Obsolete]
        void RegisterParts(IMutableDependencyResolver dependencyResolver)
        {
            dependencyResolver
                .RegisterView<TabPage, TabViewModel>()
                .RegisterView<HomePage, HomeViewModel>()
                .RegisterView<MainPage, MainViewModel>()
                .RegisterView<ProductDetailPage, ProductViewModel>()
                .RegisterView<ScanPage, ScanViewModel>()
                .RegisterNavigationView(() => new AppNavigationView());
            dependencyResolver.RegisterConstant(new Services.GS1BarcodeScannerService(), typeof(BarcodeScanner.IBarcodeScannerService));
            
        }

        void Initialize(IReadonlyDependencyResolver dependencyResolver)
        {
            dependencyResolver
                .GetService<IParameterViewStackService>()
                .PushPage(new MainViewModel(null), null, true, false)
                .Subscribe();
        }

        public Page CreateMainPage()
        {
            return Locator.Current.GetNavigationView();
        }
    }
}
