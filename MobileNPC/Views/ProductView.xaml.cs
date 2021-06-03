using Xamarin.Forms;
using ReactiveUI.XamForms;
using ReactiveUI;

namespace MobileNPC.Views
{
    public partial class ProductView : ReactiveContentView<ViewModels.ProductViewModel>
    {
        public ProductView()
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
