
using Xamarin.Forms;
using ReactiveUI;
using MobileNPC.ViewModels;
using ReactiveUI.XamForms;
using System.Reactive.Disposables;

namespace MobileNPC.Views
{
    public partial class ScanPage : ReactiveContentPage<ScanViewModel>
    {
        public ScanPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            ViewModel = new ScanViewModel();
            this.WhenActivated(disposables =>
            {
                this.BindCommand(ViewModel, x => x.ScanCommand, x => x.btnScan)
                    .DisposeWith(disposables);
            });
        }
    }
}
