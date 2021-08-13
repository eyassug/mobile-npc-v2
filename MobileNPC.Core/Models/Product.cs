namespace MobileNPC.Core.Models
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class Product
    {
        readonly Akeneo.Model.Product product;
        readonly ProductConfiguration configuration;
        readonly Services.GS1Properties gS1Properties;
        const string GS1DateTimeFormat = "yyMMdd";
        const string DateTimeFormat = "dd-MMM-yyyy";
        public Product(Akeneo.Model.Product product, ProductConfiguration configuration) : this(product, configuration, null)
        {
            
        }

        public Product(Akeneo.Model.Product product, ProductConfiguration configuration, Services.GS1Properties properties)
        {
            this.product = product ?? throw new System.ArgumentNullException(nameof(configuration));
            this.configuration = configuration ?? throw new System.ArgumentNullException(nameof(configuration));
            gS1Properties = properties;
            Identifier = product.Identifier;
            ImageUri = GetAttribute(configuration.ImageAttribute)?.Value;
            CountryOfOrigin = GetAttribute(configuration.CountryAttribute)?.Value;
            Attributes = configuration.AttributeConfigurations?
            .Select(a => GetAttribute(a.Code, a.Label)).Where(a => a != null).ToList() ?? new List<Attribute>();
        }

        public string Identifier { get; }
        public string ImageUri { get; }
        public string CountryOfOrigin { get; }
        public List<Attribute> Attributes { get; }

        Attribute GetAttribute(string code, string label = null)
        {
            var commonAttributes = new[] { Services.GS1Properties.Identifiers.BatchNumber, Services.GS1Properties.Identifiers.ProductionDate, Services.GS1Properties.Identifiers.ExpirationDate};
            label = label ?? configuration.AttributeConfigurations.FirstOrDefault(a => a.Code == code)?.Label;

            if (commonAttributes.Any(a => a == code))
                return GetCommonAttribute(code, label);

            _ = product.Values.TryGetValue(code, out var values);
            var productValue = values?.FirstOrDefault(v => v.Locale == configuration.DefaultLocale) ?? values?.FirstOrDefault();
            if (string.Compare(code, configuration.CountryAttribute, System.StringComparison.InvariantCultureIgnoreCase) == 0)
                return new Attribute(code, label, GetCountryName(productValue?.Data?.ToString()));
            else if (string.Compare(code, configuration.GTINAttribute, System.StringComparison.InvariantCultureIgnoreCase) == 0)
                return new Attribute(code, label, product.Identifier);
            return new Attribute(code: code, label: label, productValue);
        }

        Attribute GetCommonAttribute(string code, string label)
        {
            if (gS1Properties == null) return null;
            switch (code)
            {
                case Services.GS1Properties.Identifiers.BatchNumber:
                    return new Attribute(code, label, gS1Properties?.BatchOrLotNumber ?? "N/A");
                case Services.GS1Properties.Identifiers.ProductionDate:
                    return new Attribute(code, label, gS1Properties?.ProductionDate ?? "N/A");
                case Services.GS1Properties.Identifiers.ExpirationDate:
                    string expirationDate = null;
                    if (!string.IsNullOrEmpty(gS1Properties?.ExpirationDate) && System.DateTime.TryParseExact(gS1Properties.ExpirationDate, GS1DateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out var expiryDate))
                        expirationDate = expiryDate.ToString(DateTimeFormat);
                    return new Attribute(code, label, expirationDate ?? "N/A");
                default:
                    return null;
            }
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
