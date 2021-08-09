namespace MobileNPC.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Reactive;
    using System.Reactive.Disposables;
    using MobileNPC.Core.Services;
    using ReactiveUI;
    using ReactiveUI.Fody.Helpers;
    using Sextant;
    using Splat;
    using Xamarin.Forms;

    public class ProductDetailViewModel : BaseViewModel
    {

        const string AkeneoMediaPrefix = "media/cache/preview";
        const string ImageNotAvailableUri = "https://i.ibb.co/42zVPjq/unavailable-image.jpg";
        public const string ParameterName = "parameter";
        public override string Id => Identifier;

        //private readonly IProductService productService;
        public ProductDetailViewModel(IViewStackService viewStackService) : base(viewStackService)
        {
            Identifier = "Loading";
            Attributes = new List<Core.Models.Attribute>();
            CountryOfOrigin = "N/A";
        }

        [Reactive]
        public Core.Models.Product Product { get; set; }
        [Reactive]
        public string Identifier { get; set; } = "x";

        [Reactive]
        public ImageSource Image { get; set; }
        [Reactive]
        public string CountryOfOrigin { get; set; }
        [Reactive]
        public List<Core.Models.Attribute> Attributes { get; set; }

        public override IObservable<Unit> WhenNavigatingTo(INavigationParameter parameter)
        {
            if (parameter.ContainsKey(ParameterName))
            {
                var product = parameter.GetValue<Core.Models.Product>(ParameterName);
                if (!string.IsNullOrEmpty(product.ImageUri) && string.Compare(product.ImageUri, "N/A", StringComparison.OrdinalIgnoreCase) != 0)
                {
                    var builder = new UriBuilder(Configuration.AppConstants.AkeneoUrl);
                    builder.Path = $"{AkeneoMediaPrefix}/{product.ImageUri}";
                    Image = ImageSource.FromUri(builder.Uri);
                }
                else
                    Image = ImageSource.FromUri(new Uri(ImageNotAvailableUri));
                Identifier = product.Identifier;
                Product = product;
                
                Attributes.AddRange(Product.Attributes);
;            }
            return base.WhenNavigatedTo(parameter);
        }

    }
}
