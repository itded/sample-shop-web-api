using System.Collections.Generic;
using SampleShopWebApi.DTO.Common;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Business.Interfaces
{
    /// <summary>
    /// Represents a Product repository interface.
    /// </summary>
    public interface IProductRepository
    {
        /// <summary>
        /// Returns a product by a given Id.
        /// </summary>
        /// <param name="productId">Product Id.</param>
        /// <returns>The found product, otherwise null.</returns>
        Product GetProduct(int productId);

        /// <summary>
        /// Returns a collection of products filtered by given page parameters.
        /// </summary>
        /// <param name="pageParameters">Page parameters.</param>
        /// <returns>The collection of products.</returns>
        IList<Product> GetProducts(PageParameters pageParameters = null);

        /// <summary>
        /// Updates a product using product update parameters.
        /// </summary>
        /// <param name="productId">Id of a product to be updated.</param>
        /// <param name="productUpdateParameters">Product update parameters.</param>
        void UpdateProduct(int productId, ProductUpdateParameters productUpdateParameters);
    }
}
