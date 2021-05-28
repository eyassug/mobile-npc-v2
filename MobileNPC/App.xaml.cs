using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using AP.MobileToolkit.Fonts;

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

            var bootstrapper = new SextantAppBootstrapper();
            MainPage = bootstrapper.CreateMainPage();
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
