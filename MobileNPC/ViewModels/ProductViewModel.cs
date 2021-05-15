namespace MobileNPC.ViewModels
{
    using MobileNPC.Core.Models;
    public class ProductViewModel : ViewModelBase
    {
        private readonly Product product;

        public ProductViewModel(Product product)
        {
            this.product = product ?? throw new System.ArgumentNullException(nameof(product));
        }

        public Product Product => product;
    }
}
