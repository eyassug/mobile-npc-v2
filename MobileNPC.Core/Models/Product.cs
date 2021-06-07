namespace MobileNPC.Core.Models
{
    using System.Collections.Generic;
    using System.Linq;

    public class Product
    {
        readonly Akeneo.Model.Product product;
        readonly ProductConfiguration configuration;
        public Product(Akeneo.Model.Product product, ProductConfiguration configuration)
        {
            this.product = product ?? throw new System.ArgumentNullException(nameof(configuration));
            this.configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
        }
        public string Identifier => product.Identifier;
        public string ImageUri => GetAttribute(configuration.ImageAttribute)?.Value;
        public string CountryOfOrigin => GetCountryName(GetAttribute(configuration.CountryAttribute)?.Value);
        public List<Attribute> Attributes => configuration.AttributeConfigurations?
            .Select(a => GetAttribute(a.Code, a.Label)).ToList() ?? new List<Attribute>();

        Attribute GetAttribute(string code, string label = null)
        {
            label = label ?? configuration.AttributeConfigurations.FirstOrDefault(a => a.Code == code)?.Label;
            _ = product.Values.TryGetValue(code, out var values);
            var productValue = values?.FirstOrDefault(v => v.Locale == configuration.DefaultLocale) ?? values?.FirstOrDefault();
            return new Attribute(code: code, label: label, productValue);
        }

        string GetCountryName(string countryCode)
        {
            // TODO: add countryCode dictionary and get values
            return countryCode;
        }
    }
}
