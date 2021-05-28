namespace MobileNPC.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Reactive;
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

        public override IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter)
        {
            if (parameter.ContainsKey("parameter"))
            {
                var received = parameter["parameter"];
                Debug.WriteLine($"Received {received}");
                //ReceivedParameter = received.ToString();
            }

            return base.WhenNavigatedTo(parameter);
        }
    }
}
