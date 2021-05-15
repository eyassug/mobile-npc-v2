namespace MobileNPC.ViewModels
{
    public class ProductViewModel : ViewModelBase
    {
        private readonly Models.Product product;

        public ProductViewModel(Models.Product product)
        {
            this.product = product ?? throw new System.ArgumentNullException(nameof(product));
        }

        public Models.Product Product => product;
    }
}
