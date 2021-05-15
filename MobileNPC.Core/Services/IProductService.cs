namespace MobileNPC.Core.Services
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IProductService
    {
        /// <summary>
        /// Gets a product with the specified identifier
        /// </summary>
        /// <param name="code">GTIN or Product Identification Code</param>
        /// <returns></returns>
        Task<Models.Product> GetProductAsync(string code);
        /// <summary>
        /// Gets a paginated list of products based on the specified filters
        /// </summary>
        /// <param name="queryString">initial querystring</param>
        /// <param name="page">Page number</param>
        /// <param name="limit">Number of items per page</param>
        /// <param name="locale">Akeneo Locale</param>
        /// <returns></returns>
        Task<IEnumerable<Models.Product>> GetProductsAsync(string queryString, int page = 1, int limit = 25, string locale="en_US");
    }
}
