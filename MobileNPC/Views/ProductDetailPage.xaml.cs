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
                this.Bind(ViewModel, x => x.Identifier, x => x.lblGTIN.Text);

                this.Bind(ViewModel, x => x.Name, x => x.lblFunctionalName.Text);
            });
        }
    }
}
