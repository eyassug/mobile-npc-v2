namespace MobileNPC.Core.Services
{
    using System.Linq;
    using System.Threading.Tasks;
    using Akeneo;
    using Akeneo.Client;
    using Akeneo.Model;

    public class AttributeProductService : ProductService
    {
        string GTINAttribute { get; }

        public AttributeProductService(AkeneoOptions akeneoOptions, Models.ProductConfiguration configuration)
            : base(akeneoOptions, configuration)
        {
            if (string.IsNullOrEmpty(configuration.GTINAttribute)) throw new System.ArgumentException(nameof(configuration.GTINAttribute));
            GTINAttribute = configuration.GTINAttribute;
        }

        async public override Task<Product> GetProductAsync(string code)
        {
            try
            {
                var searchParams = @"?search={""" + GTINAttribute + @""":[{""operator"":""="",""value"":""" + code + @"""}]}";
                var response = await AkeneoClient.FilterAsync<Product>(searchParams);
                return response.GetItems().FirstOrDefault();
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}
