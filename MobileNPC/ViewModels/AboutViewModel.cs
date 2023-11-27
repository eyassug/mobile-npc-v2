using System;
using System.Windows.Input;
using BarcodeScanner;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using Rg.Plugins.Popup.Services;
using Sextant;
using Splat;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileNPC.ViewModels
{
    public class AboutViewModel : BaseViewModel, ITabViewModel
    {
        public override string Id => "About";
        private IBarcodeScannerService BarcodeScanner { get; }
        public string TabTitle => Id;

        public ImageSource TabIcon => "";

        public AboutViewModel(IViewStackService viewStackService = null): base(viewStackService)
        {
            BarcodeScanner = Locator.Current.GetService<IBarcodeScannerService>();
            OpenPrivacyPolicy = new Command(async () =>
            {
                await Browser.OpenAsync("https://malawinpc.netlify.app/privacy");
            });
            OpenTermsAndConditions = new Command(async () =>
            {
                await Browser.OpenAsync("https://malawinpc.netlify.app/terms");
            });
            CountryName = Configuration.AppConstants.EnvironmentName;
        }

        public ICommand OpenPrivacyPolicy { get; }
        public ICommand OpenTermsAndConditions { get; }
        [Reactive]
        public string CountryName { get; set; }
    }
}