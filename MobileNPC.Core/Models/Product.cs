namespace MobileNPC.Core.Models
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class Product
    {
        readonly Akeneo.Model.Product product;
        readonly ProductConfiguration configuration;
        public Product(Akeneo.Model.Product product, ProductConfiguration configuration)
        {
            this.product = product ?? throw new System.ArgumentNullException(nameof(configuration));
            this.configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
            Identifier = product.Identifier;
            ImageUri = GetAttribute(configuration.ImageAttribute)?.Value;
            CountryOfOrigin = GetAttribute(configuration.CountryAttribute)?.Value;
            Attributes = configuration.AttributeConfigurations?
            .Select(a => GetAttribute(a.Code, a.Label)).ToList() ?? new List<Attribute>();
        }

        public Product(Akeneo.Model.Product product, ProductConfiguration configuration, Services.GS1Properties properties) : this(product, configuration)
        {
            if(!string.IsNullOrEmpty(properties?.ExpirationDate))
                Attributes.Add(new Attribute("EXP", "Expiration Date", properties.ExpirationDate));
        }

        public string Identifier { get; }
        public string ImageUri { get; }
        public string CountryOfOrigin { get; }
        public List<Attribute> Attributes { get; }

        Attribute GetAttribute(string code, string label = null)
        {
            label = label ?? configuration.AttributeConfigurations.FirstOrDefault(a => a.Code == code)?.Label;
            _ = product.Values.TryGetValue(code, out var values);
            var productValue = values?.FirstOrDefault(v => v.Locale == configuration.DefaultLocale) ?? values?.FirstOrDefault();
            if (string.Compare(code, configuration.CountryAttribute, System.StringComparison.InvariantCultureIgnoreCase) == 0)
                return new Attribute(code, label, GetCountryName(productValue?.Data?.ToString()));
            else if (string.Compare(code, configuration.GTINAttribute, System.StringComparison.InvariantCultureIgnoreCase) == 0)
                return new Attribute(code, label, product.Identifier);
            return new Attribute(code: code, label: label, productValue);
        }

        string GetCountryName(string countryCode)
        {
            var country = ISO3166.Country.List.Where(c => c.TwoLetterCode.Equals(countryCode, System.StringComparison.OrdinalIgnoreCase)
                                                        || c.ThreeLetterCode.Equals(countryCode, System.StringComparison.OrdinalIgnoreCase)
                                                        || c.NumericCode.Equals(countryCode, System.StringComparison.OrdinalIgnoreCase)).FirstOrDefault();
            return country?.Name;
        }
    }
}
