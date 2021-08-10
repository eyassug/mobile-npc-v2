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
        protected AkeneoClient AkeneoClient { get; }
        Models.ProductConfiguration Configuration { get; }
        public ProductService(AkeneoOptions akeneoOptions)
        {
            AkeneoClient = new AkeneoClient(akeneoOptions);
        }

        public ProductService(AkeneoOptions akeneoOptions, Models.ProductConfiguration configuration)
            : this(akeneoOptions)
        {
            Configuration = configuration;
        }

        async public virtual Task<Product> GetProductAsync(string code)
        {
            try
            {
                return await AkeneoClient.GetAsync<Product>(code);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error fetching product: '{code}'\n{ex.Message}");
                return null;
            }
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

        async public Task<Models.Product> GetAsync(string code)
        {
            //TODO: Add PollyRetry
            var product = await GetProductAsync(code);
            if (product == null) return null;
            return new Models.Product(product, Configuration);
        }

        async public Task<Models.Product> GetAsync(string code, GS1Properties properties)
        {
            //TODO: Add PollyRetry
            var product = await GetProductAsync(code);
            if (product == null) return null;
            return new Models.Product(product, Configuration, properties);
        }
    }
}
