using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.XamForms;
namespace MobileNPC.Views
{
    public partial class ProductDetailPage : ReactiveContentPage<ViewModels.ProductDetailViewModel>
    {
        public ProductDetailPage()
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
