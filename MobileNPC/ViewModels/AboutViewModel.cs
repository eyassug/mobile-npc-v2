using System;
using System.Windows.Input;
using BarcodeScanner;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileNPC.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private IBarcodeScannerService _barcodeScanner { get; }
        public AboutViewModel(IScreen hostScreen = null): base(hostScreen)
        {
            _barcodeScanner = new Services.GS1BarcodeScannerService();
            Title = "About";
            OpenWebCommand = new Command(async () =>
            {
                var result = await _barcodeScanner.ReadBarcodeAsync();
            });
        }

        public ICommand OpenWebCommand { get; }
    }
}