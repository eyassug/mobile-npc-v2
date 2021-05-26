namespace MobileNPC.ViewModels
{
    using ReactiveUI;
    using ReactiveUI.Fody.Helpers;
    using Sextant;

    public class ProductDetailViewModel : ViewModelBase
    {
        public ProductDetailViewModel(IScreen hostScreen = null, IViewStackService viewStackService = null) : base(viewStackService)
        {
            string gtin = "XXX";
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
