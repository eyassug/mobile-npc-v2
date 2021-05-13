using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using MobileNPC.Services;
using MobileNPC.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace MobileNPC
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
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
