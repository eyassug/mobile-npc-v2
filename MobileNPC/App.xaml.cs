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
            AppCenter.Start("android=c106f68d-0716-451d-94d2-8d233cf9cc86;ios=4675c0bf-7208-4873-969a-235251ceb5b0;",
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
