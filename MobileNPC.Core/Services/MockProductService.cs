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

        public Task<Product> GetProductAsync(string code)
        {
            return Task.FromResult(new Product
            {
                Identifier = code,
                Values = new Dictionary<string, List<Akeneo.Model.ProductValue>>
                {
                    //TODO: Add product attributes here
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
