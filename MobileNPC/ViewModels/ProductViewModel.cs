namespace MobileNPC.ViewModels
{
    using MobileNPC.Core.Models;
    using ReactiveUI.Fody.Helpers;
    using Sextant;

    public class ProductViewModel : BaseViewModel, IViewModel
    {
        public override string Id => "Amoxicilin";

        public ProductViewModel(IViewStackService viewStackService) : base(viewStackService)
        {
            Identifier = "1".PadLeft(14);
            Name = Id;
        }

        [Reactive]
        public string Identifier { get; set; }
        [Reactive]
        public string Name { get; set; }
    }
}
