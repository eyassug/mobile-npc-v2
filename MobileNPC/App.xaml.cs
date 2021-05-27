using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileNPC.Services;
using MobileNPC.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using ReactiveUI;
using BarcodeScanner;
using Rg.Plugins.Popup.Services;
using Rg.Plugins.Popup.Contracts;
using AP.MobileToolkit.Fonts;
using static Sextant.Sextant;
using Sextant.XamForms;
using Splat;
using MobileNPC.ViewModels;
using Sextant;

namespace MobileNPC
{
    public partial class App : Application
    {

        public App()
        {
            FontRegistry.RegisterFonts(FontAwesomeBrands.Font,
                                       FontAwesomeRegular.Font,
                                       FontAwesomeSolid.Font);

            InitializeComponent();

            RxApp.DefaultExceptionHandler = new SextantDefaultExceptionHandler();

            Instance.InitializeForms();

            Locator
                .CurrentMutable
                .RegisterView<TabPage, TabViewModel>()
                .RegisterView<HomePage, HomeViewModel>()
                .RegisterView<MainPage, MainViewModel>()
                .RegisterView<ProductDetailPage, ProductViewModel>()
                .RegisterNavigationView(() => new AppNavigationView());

            Locator
                .Current
                .GetService<IViewStackService>()
                .PushPage(new MainViewModel(null), null, true, false)
                .Subscribe();

            MainPage = Locator.Current.GetNavigationView();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=2978adb1-3a4b-4c3f-89fc-22cdd61d0c9c;ios=4675c0bf-7208-4873-969a-235251ceb5b0;",
                  typeof(Analytics), typeof(Crashes));
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
