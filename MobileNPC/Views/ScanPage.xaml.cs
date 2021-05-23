using System;
using System.Collections.Generic;

using Xamarin.Forms;
using ReactiveUI;
using MobileNPC.ViewModels;
using ReactiveUI.XamForms;
using ZXing.Net.Mobile.Forms;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive;

namespace MobileNPC.Views
{
    public partial class ScanPage : ReactiveContentPage<ScanViewModel>
    {
        //ZXingScannerView zxing;
        //ZXingDefaultOverlay overlay;

        public ScanPage()
        {
            InitializeComponent();
            ViewModel = new ScanViewModel();
            this.WhenActivated(disposables =>
            {
                this.BindCommand(ViewModel, x => x.ScanCommand, x => x.btnScan)
                    .DisposeWith(disposables);
            });
            //this.WhenActivated(disposables =>
            //{
            //    this.Bind(ViewModel, x => x.IsScanning, x => x.zxing.IsScanning)
            //        .DisposeWith(disposables);
            //    this.Bind(ViewModel, x => x.IsAnalyzing, x => x.zxing.IsAnalyzing)
            //        .DisposeWith(disposables);
            //    this.Bind(ViewModel, x => x.IsTorchOn, x => x.zxing.IsTorchOn)
            //        .DisposeWith(disposables);
            //    this.Bind(ViewModel, x => x.Result, x => x.zxing.Result)
            //        .DisposeWith(disposables);
            //    //this.BindCommand(ViewModel, x => x.ScanCommand, x => x.zxing.ScanResultCommand)
            //    //    .DisposeWith(disposables);
            //    //this.BindCommand(ViewModel, x => x.ToggleTorchCommand, x => x.overlay.FlashCommand)
            //    //    .DisposeWith(disposables);
            //    Observable.FromEventPattern(zxing, nameof(zxing.OnScanResult))                   
            //        .InvokeCommand(this, x => x.ViewModel.ScanCommand);

            //    Observable.FromEventPattern(overlay, nameof(overlay.FlashButtonClicked))
            //        .InvokeCommand(this, x => x.ViewModel.ScanCommand);
            //});
            //zxing.OnScanResult += (result) =>
            //	Device.BeginInvokeOnMainThread(async () =>
            //	{

            //		// Stop analysis until we navigate away so we don't keep reading barcodes
            //		zxing.IsAnalyzing = false;

            //		// Show an alert
            //		await DisplayAlert("Scanned Barcode", result.Text, "OK");

            //		// Navigate away
            //		await Navigation.PopAsync();
            //	});

            //overlay = new ZXingDefaultOverlay
            //{
            //    TopText = "Hold your phone up to the barcode",
            //    BottomText = "Scanning will happen automatically",
            //    ShowFlashButton = zxing.HasTorch,
            //    AutomationId = "zxingDefaultOverlay",
            //};
            //overlay.FlashButtonClicked += (sender, e) =>
            //{

            //};
            //var grid = new Grid
            //{
            //	VerticalOptions = LayoutOptions.FillAndExpand,
            //	HorizontalOptions = LayoutOptions.FillAndExpand,
            //};
            //grid.Children.Add(zxing);
            //grid.Children.Add(overlay);

            //// The root page of your application
            //Content = grid;
        }
    }
}
