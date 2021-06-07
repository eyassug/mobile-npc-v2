namespace MobileNPC.Core.Models
{
    using System.Collections.Generic;
    public class ProductConfiguration
    {
        public string DefaultLocale { get; set; } = "enUS";
        public string ImageAttribute { get; set; }
        public string CountryAttribute { get; set; }
        public List<AttributeConfiguration> AttributeConfigurations { get; set; } = new List<AttributeConfiguration>();

        public static ProductConfiguration Default => new ProductConfiguration
        {
            ImageAttribute = "GS1_PRODUCT_IMAGE",
            CountryAttribute = "COUNTRY_OF_ORIGIN",
            DefaultLocale = "en_US",
            AttributeConfigurations = new List<AttributeConfiguration>
            {
                new AttributeConfiguration {Code = "COUNTRY_OF_ORIGIN", Label = "Country Of Origin"},
                new AttributeConfiguration {Code = "GS1_BRANDNAME", Label = "Brand Name"},
                new AttributeConfiguration {Code = "GS1_FUNCTIONAL_NAME", Label = "Functional Name"},
                new AttributeConfiguration {Code = "MANUFACTURER_NAME", Label = "Manufacturer Name"}
            }
        };
    }

    public class AttributeConfiguration
    {
        public string Code { get; set; }
        public string Label { get; set; }
    }
}
