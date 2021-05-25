namespace MobileNPC.ViewModels
{
    using ReactiveUI;
    using ReactiveUI.Fody.Helpers;

    public class ProductDetailViewModel : ViewModelBase
    {
        public ProductDetailViewModel(string gtin, IScreen hostScreen = null) : base(hostScreen)
        {
            UrlPathSegment = gtin;
            Identifier = gtin;
            Name = "Amoxicillin";
        }

        [Reactive]
        public string Identifier { get; set; }
        [Reactive]
        public string Name { get; set; }


    }
}
