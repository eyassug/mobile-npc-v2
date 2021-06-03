namespace MobileNPC.ViewModels
{
    using System;
    using System.Diagnostics;
    using System.Reactive;
    using System.Reactive.Disposables;
    using MobileNPC.Core.Services;
    using ReactiveUI;
    using ReactiveUI.Fody.Helpers;
    using Sextant;
    using Splat;

    public class ProductDetailViewModel : BaseViewModel
    {
        public const string ParameterName = "parameter";
        public override string Id => Identifier;

        private readonly IProductService productService;
        public ProductDetailViewModel(IViewStackService viewStackService) : base(viewStackService)
        {
            productService = Locator.Current.GetService<IProductService>();
            FetchProductCommand = ReactiveCommand.CreateFromTask<string, Unit>(async (string code) =>
            {
                var product = await productService.GetProductAsync(code);
                return Unit.Default;
            });
        }

        [Reactive]
        public ProductViewModel Product { get; set; }
        [Reactive]
        public string Identifier { get; set; }
        [Reactive]
        public string Name { get; set; }


        public override IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter)
        {
            if (parameter.ContainsKey(ParameterName))
            {
                var gtin = parameter.GetValue<string>(ParameterName);
                Debug.WriteLine($"Received {gtin}");
                Identifier = $"{gtin}";
                var selectedProduct = productService.GetProductAsync(gtin).Result;
                if(selectedProduct == null)
                {
                    // Handle Product Not Found Interaction
                    NavigationService.PopPage().Subscribe().DisposeWith(Disposables);
                }
                //FetchProductCommand.Execute(gtin).Subscribe();
                //ReceivedParameter = received.ToString();
            }

            return base.WhenNavigatedTo(parameter);
        }

        public ReactiveCommand<string, Unit> FetchProductCommand { get; private set; }

    }
}
