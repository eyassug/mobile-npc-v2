using Xamarin.Forms;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace MobileNPC
{
    public partial class App : Application
    {

        public App()
        {
            //FontRegistry.RegisterFonts(FontAwesomeBrands.Font,
            //                           FontAwesomeRegular.Font,
            //                           FontAwesomeSolid.Font);

            InitializeComponent();

            var bootstrapper = new SextantAppBootstrapper();
            MainPage = bootstrapper.CreateMainPage();
        }

        protected override void OnStart()
        {
            AppCenter.Start("android=ce339a09-3624-41db-9d6b-41f778d9e20f;ios=4675c0bf-7208-4873-969a-235251ceb5b0;",
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
