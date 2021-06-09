using Xamarin.Forms;
using ReactiveUI.XamForms;
using ReactiveUI;
using System.Reactive.Disposables;

namespace MobileNPC.Views
{
    public partial class ProductView : ReactiveContentView<ViewModels.ProductViewModel>
    {
        public ProductView()
        {
            InitializeComponent();
            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, vm => vm.ImageUri, v => v.ImageProduct.Source).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Attributes, v => v.ListAttributes.ItemsSource).DisposeWith(disposables);
            });
        }
    }
}
