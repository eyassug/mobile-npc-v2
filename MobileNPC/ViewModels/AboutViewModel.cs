using System;
using System.Windows.Input;
using BarcodeScanner;
using ReactiveUI;
using Rg.Plugins.Popup.Services;
using Sextant;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileNPC.ViewModels
{
    public class AboutViewModel : ViewModelBase
    {
        private IBarcodeScannerService _barcodeScanner { get; }
        public AboutViewModel(IViewStackService viewStackService = null) : base(viewStackService)
        {
            _barcodeScanner = new Services.GS1BarcodeScannerService();
            UrlPathSegment = "About";
            OpenWebCommand = new Command(async () =>
            {
                var result = await _barcodeScanner.ReadBarcodeAsync();
            });
        }

        public ICommand OpenWebCommand { get; }
    }
}