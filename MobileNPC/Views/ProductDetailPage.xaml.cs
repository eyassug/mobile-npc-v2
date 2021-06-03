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
                this.Bind(ViewModel, x => x.Product, x => x.Product.ViewModel);
            });
        }
    }
}
