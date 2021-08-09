using System;

using Android.App;
using Android.OS;
using Android.Util;
using System.Threading.Tasks;
using Android.Content;

namespace MobileNPC.Droid
{
    [Activity(Theme = "@style/MyTheme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        static readonly string TAG = "X:" + typeof(SplashActivity).Name;

        public override void OnCreate(Bundle savedInstanceState, PersistableBundle persistentState)
        {
            base.OnCreate(savedInstanceState, persistentState);
            Log.Debug(TAG, "SplashActivity.OnCreate");
        }

        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { Startup(); });
            startupWork.Start();
        }

        // TODO: Remove
        // Simulates background work that happens behind the splash screen
        async void Startup()
        {
            await Task.CompletedTask;
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}
