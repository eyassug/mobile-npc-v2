namespace MobileNPC
{
    using System;
    using MobileNPC.Core.Services;
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
#if DEBUG
            dependencyResolver.RegisterConstant(new MockProductService(), typeof(IProductService));

#else
            var akeneoOptions = new Akeneo.AkeneoOptions
            {
                ApiEndpoint = new Uri(Configuration.AppConstants.AkeneoUrl),
                ClientId = Configuration.AppConstants.ClientId,
                ClientSecret = Configuration.AppConstants.ClientSecret,
                UserName = Configuration.AppConstants.Username,
                Password = Configuration.AppConstants.Password
            };
            dependencyResolver.RegisterLazySingleton<IProductService>(() => new AttributeProductService(akeneoOptions, Core.Models.ProductConfiguration.Rwanda ));
#endif

            dependencyResolver.RegisterConstant(new GS1ParserService(), typeof(IGS1ParserService));
            dependencyResolver.RegisterConstant(new Services.Connectivity(), typeof(Services.IConnectivity));
            dependencyResolver
                .RegisterView<TabPage, TabViewModel>()
                .RegisterView<MainPage, MainViewModel>()
                .RegisterView<ProductDetailPage, ProductDetailViewModel>()
                .RegisterView<ScanPage, ScanViewModel>()
                .RegisterView<AboutPage, AboutViewModel>()
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
