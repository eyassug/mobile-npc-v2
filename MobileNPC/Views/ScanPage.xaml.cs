﻿
using Xamarin.Forms;
using ReactiveUI;
using MobileNPC.ViewModels;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using System.Reflection;

namespace MobileNPC.Views
{
    public partial class ScanPage : ReactiveContentPage<ScanViewModel>
    {
        public ScanPage()
        {
            InitializeComponent();
            ImageBarcodeScanner.Source = ImageSource.FromResource("Resources.barcode_scanner.png");
            this.WhenActivated(disposables =>
            {
                this.BindCommand(ViewModel, x => x.ScanCommand, x => x.ButtonScan)
                    .DisposeWith(disposables);
            });
        }
    }
}
