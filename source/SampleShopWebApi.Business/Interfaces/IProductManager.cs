using System.Collections.Generic;
using SampleShopWebApi.DTO.Common;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Business.Interfaces
{
    /// <summary>
    /// Represents a Product manager interface.
    /// </summary>
    public interface IProductManager
    {
        /// <summary>
        /// Returns a product by a given Id.
        /// </summary>
        /// <param name="productId">Product Id.</param>
        /// <returns>The found product, otherwise null.</returns>
        Product GetProduct(int productId);

        /// <summary>
        /// Returns a collection of all found products.
        /// </summary>
        /// <returns>The collection of products.</returns>
        PageResult<IList<Product>> GetAllProducts();

        /// <summary>
        /// Returns a collection of products filtered by given page parameters.
        /// </summary>
        /// <param name="pageParameters">Page parameters.</param>
        /// <returns>The collection of products.</returns>
        PageResult<IList<Product>> GetProducts(PageParameters pageParameters);

        /// <summary>
        /// Update a product using product update parameters.
        /// </summary>
        /// <param name="productId">Id of a product to be updated.</param>
        /// <param name="productUpdateParameters">Product update parameters.</param>
        /// <returns>Update result.</returns>
        UpdateResult<Product> UpdateProduct(int productId, ProductUpdateParameters productUpdateParameters);
    }
}