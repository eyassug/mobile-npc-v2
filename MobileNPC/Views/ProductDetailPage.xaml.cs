using System.Reactive.Disposables;
using ReactiveUI;
using ReactiveUI.XamForms;
using Xamarin.Forms;

namespace MobileNPC.Views
{
    public partial class ProductDetailPage : ReactiveContentPage<ViewModels.ProductDetailViewModel>
    {
        public ProductDetailPage()
        {
            InitializeComponent();
            ImageVerified.Source = ImageSource.FromResource("MobileNPC.Resources.verified_icon.png");
            this.WhenActivated(disposables =>
            {
                this.Bind(ViewModel, vm => vm.Image, v => v.ImageProduct.Source).DisposeWith(disposables);
                this.OneWayBind(ViewModel, vm => vm.Attributes, v => v.ListAttributes.ItemsSource).DisposeWith(disposables);
                //ImageProduct.LoadingPlaceholder = new FontImageSource().SetIcon("fas fa-spinner");
            });
        }
    }
}
