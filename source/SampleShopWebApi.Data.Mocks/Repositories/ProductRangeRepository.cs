using System;
using System.Collections.Generic;
using System.Linq;
using SampleShopWebApi.Business.Interfaces;
using SampleShopWebApi.DTO.Common;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Data.Mocks.Repositories
{
    /// <summary>
    /// Represents the mock implementation of the <see cref="IProductRepository"/>.
    /// Ranged data are stored in an internal non-static collection.
    /// </summary>
    public class ProductRangeRepository: IProductRepository
    {
        private readonly int indexFrom;
        private readonly int indexTo;

        private IList<Product> products = null;

        /// <summary>
        /// Sets up internal data collection inclusive index range.
        /// </summary>
        /// <param name="indexFrom">The first range index.</param>
        /// <param name="indexTo">The last range index.</param>
        public ProductRangeRepository(int indexFrom, int indexTo)
        {
            this.indexFrom = indexFrom;
            this.indexTo = indexTo;
        }

        /// <inheritdoc/>
        public Product GetProduct(int productId){
            return this.GetProducts().FirstOrDefault(x => x.Id == productId);
        }

        /// <inheritdoc />
        public IList<Product> GetProducts(PageParameters pageParameters) {
            if (pageParameters == null)
            {
                return this.GetProducts();
            }
            else
            {
                return this.GetProducts()
                    .Skip((pageParameters.Page - 1) * pageParameters.PageSize)
                    .Take(pageParameters.PageSize).ToList();
            }
        }

        /// <inheritdoc />
        public void UpdateProduct(int productId, ProductUpdateParameters productUpdateParameters) {
            var product = this.GetProducts().FirstOrDefault(x => x.Id == productId);

            if (product == null){
                throw new ArgumentNullException(nameof(product));
            }

            if (!productUpdateParameters.ReplaceDescription){
                return;
            }
            
            product.Description = productUpdateParameters.Description;
        }

        private IList<Product> GetProducts()
        {
            return products ??= Enumerable.Range(indexFrom, indexTo).Select(x => new Product()
            {
                Id = x,
                Name = $"Name{x}",
                Description = x % 2 == 0 ? $"Description{x}" : null,
                ImgUri = $"Img{x}.png",
                Price = x * 100
            }).ToList();
        }
    }
}
