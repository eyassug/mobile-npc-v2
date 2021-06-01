namespace MobileNPC.Core.Services
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Akeneo;
    using Akeneo.Client;
    using Akeneo.Model;
    using Akeneo.Search;

    public class ProductService : IProductService
    {
        AkeneoClient AkeneoClient { get; }
        public ProductService(AkeneoOptions akeneoOptions)
        {
            AkeneoClient = new AkeneoClient(akeneoOptions);
        }

        async public Task<Product> GetProductAsync(string code)
        {
            return await AkeneoClient.GetAsync<Product>(code);
        }

        async public Task<IEnumerable<Product>> GetProductsAsync(string queryString, int page = 1, int limit = 25, string locale = "en_US")
        {
            queryString = $"{queryString}&page={page}&limit={limit}&with_count={true.ToString().ToLower()}&locale={locale}";
            var products = await AkeneoClient.FilterAsync<Product>(queryString);
            return products.GetItems();
        }

        public Task<IEnumerable<Product>> GetProductsAsync(IEnumerable<Criteria> criteria, int page = 1, int limit = 25, string locale = "en_US")
        {
            var queryString = SearchQueryBuilder.Instance.GetQueryString(criteria);
            return GetProductsAsync(queryString, page, limit, locale);
        }
    }
}
