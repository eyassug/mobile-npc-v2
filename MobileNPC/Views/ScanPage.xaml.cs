
using Xamarin.Forms;
using ReactiveUI;
using MobileNPC.ViewModels;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;
using System.Reflection;
using System.Reactive;

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
                this.BindInteraction(ViewModel,
                        vm => vm.ProductNotFoundInteraction,
                        async context =>
                        {
                            await this.DisplayAlert("Not Found",
                                $"A Product with the specified GTIN could not be found! Please try again!",
                                "OK");
                            context.SetOutput(Unit.Default);
                        }).DisposeWith(disposables);
                this.BindInteraction(ViewModel,
                        vm => vm.InvalidBarcodeInteraction,
                        async context =>
                        {
                            await this.DisplayAlert("Invalid Barcode",
                                $"The Barcode you scanned is invalid. Please scan a valid GS1 Barcode",
                                "OK");
                            context.SetOutput(Unit.Default);
                        }).DisposeWith(disposables);

                this.BindCommand(ViewModel, x => x.ScanCommand, x => x.ButtonScan)
                    .DisposeWith(disposables);
            });
        }
    }
}
