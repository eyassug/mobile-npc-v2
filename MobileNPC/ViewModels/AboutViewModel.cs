using System;
using System.Windows.Input;
using BarcodeScanner;
using ReactiveUI;
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
            OpenWebCommand = new Command(async () =>
            {
                var result = await BarcodeScanner.ReadBarcodeAsync();
            });
        }

        public ICommand OpenWebCommand { get; }
    }
}