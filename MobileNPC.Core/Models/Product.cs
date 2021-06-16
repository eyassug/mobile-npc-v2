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
            return new Attribute(code: code, label: label, productValue);
        }

        string GetCountryName(string countryCode)
        {
            var regionInfo = CultureInfo
                    .GetCultures(CultureTypes.SpecificCultures)
                    .Select(culture => new RegionInfo(culture.LCID))
                    .FirstOrDefault(region => region.TwoLetterISORegionName == countryCode);
            // TODO: add countryCode dictionary and get values
            return regionInfo?.EnglishName;
        }
    }
}
