namespace MobileNPC.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reactive;
    using MobileNPC.Core.Models;
    using ReactiveUI;
    using ReactiveUI.Fody.Helpers;
    using Sextant;

    public class ProductViewModel : ReactiveObject, IActivatableViewModel
    {
        Akeneo.Model.Product Product { get; }
        public ProductViewModel(Akeneo.Model.Product product) 
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Identifier = Product.Identifier;
            Attributes = new List<Core.Models.Attribute>();
        }

        [Reactive]
        public string Identifier { get; set; }
        [Reactive]
        public string Name { get; set; }
        [Reactive]
        public Uri ImageUri { get; set; }
        [Reactive]
        public List<Core.Models.Attribute> Attributes { get; set; }
        

        public ViewModelActivator Activator => new ViewModelActivator();
    }

    public class AttributeViewModel : ReactiveObject
    {
        public AttributeViewModel(string attributeCode, IEnumerable<Akeneo.Model.ProductValue> productValue)
        {
            Code = attributeCode;
        }

        public string Code { get; }
        [Reactive]
        public string Label { get; set; }
        [Reactive]
        public string Value { get; set; }
    }
}
