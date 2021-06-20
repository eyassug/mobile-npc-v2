
using System;
using BarcodeScanner;
using Rg.Plugins.Popup.Services;
using System.Windows.Input;
using Rg.Plugins.Popup.Contracts;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using ZXing.Mobile;

namespace MobileNPC.Services
{

    public class GS1BarcodeScannerService : PopupBarcodeScannerService
    {
        public GS1BarcodeScannerService(IPopupNavigation navigation) : base(navigation)
        {
        }

        public GS1BarcodeScannerService():this(Rg.Plugins.Popup.Services.PopupNavigation.Instance)
        {
        }

        protected override View GetScannerOverlay()
        {
            var overlay = base.GetScannerOverlay() as ZXingDefaultOverlay;
            overlay.TopText = TopText();
            overlay.BottomText = BottomText();
            overlay.ShowFlashButton = true;
            return overlay;
        }
        protected override string TopText() => "Hold your phone up to the barcode";
        protected override string BottomText() => "Scanning will happen automatically";
        protected override MobileBarcodeScanningOptions GetScanningOptions()
        {
            var options = base.GetScanningOptions();
            options.AssumeGS1 = true;
            options.AutoRotate = true;
            options.TryHarder = true;
            options.DelayBetweenContinuousScans = 1000;
            options.UseNativeScanning = true;
            options.UseFrontCameraIfAvailable = false;
            return options;
        }

        protected override bool OnBackButtonPressed()
        {
            PopupNavigation.RemovePageAsync(this);
            return base.OnBackButtonPressed();
        }

        protected override Thickness GetDefaultPadding() =>
            new Thickness(20, 80);
    }
}
