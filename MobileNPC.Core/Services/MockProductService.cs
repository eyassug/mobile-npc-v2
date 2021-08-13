namespace MobileNPC.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Akeneo.Model;
    using Akeneo.Search;

    public class MockProductService : IProductService
    {
        public MockProductService()
        {
        }

        async public Task<Models.Product> GetAsync(string code)
        {
            var product = await GetProductAsync(code);
            return new Models.Product(product, Models.ProductConfiguration.Default);
        }

        async public Task<Models.Product> GetAsync(string code, GS1Properties properties)
        {
            var product = await GetProductAsync(code);
            return new Models.Product(product, Models.ProductConfiguration.Default, new GS1Properties
            {
                GTIN = code,
                BatchOrLotNumber = "B123",
                ProductionDate = "210101",
                ExpirationDate = "220101"
            });
        }

        public Task<Product> GetProductAsync(string code)
        {
            return Task.FromResult(new Product
            {
                Identifier = code,
                Values = new Dictionary<string, List<Akeneo.Model.ProductValue>>
                {
                    {"COUNTRY_OF_ORIGIN", new List<Akeneo.Model.ProductValue>{ new Akeneo.Model.ProductValue{Data = "Demo Country"} } },
                    {"GS1_BRANDNAME", new List<Akeneo.Model.ProductValue>{ new Akeneo.Model.ProductValue{Data = "Demo Brand"} } },
                    {"GS1_FUNCTIONAL_NAME", new List<Akeneo.Model.ProductValue>{ new Akeneo.Model.ProductValue{Data = "Demo Functional Name"} } },
                    {"MANUFACTURER_NAME", new List<Akeneo.Model.ProductValue>{ new Akeneo.Model.ProductValue{Data = "Demo Manufacturer"} } }
                }
            });
        }

        public Task<IEnumerable<Product>> GetProductsAsync(string queryString, int page = 1, int limit = 25, string locale = "en_US")
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<Criteria> criteria, int page = 1, int limit = 25, string locale = "en_US")
        {
            throw new NotImplementedException();
        }
    }
}
