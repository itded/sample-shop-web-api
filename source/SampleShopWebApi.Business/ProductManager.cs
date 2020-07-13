using System;
using System.Collections.Generic;
using SampleShopWebApi.Business.Interfaces;
using SampleShopWebApi.DTO.Common;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Business
{
    /// <summary>
    /// Represents the implementation of the <see cref="IProductManager"/> interface.
    /// </summary>
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository productRepository;

        /// <summary>
        /// .Ctor
        /// </summary>
        /// <param name="productRepository">Product repository.</param>
        public ProductManager(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        /// <inheritdoc />
        public Product GetProduct(int productId)
        {
            return this.productRepository.GetProduct(productId);
        }

        /// <inheritdoc />
        public IList<Product> GetAllProducts()
        {
            return this.productRepository.GetProducts(null);
        }

        /// <inheritdoc />
        public IList<Product> GetProducts(PageParameters pageParameters)
        {
            return this.productRepository.GetProducts(pageParameters);
        }

        /// <inheritdoc />
        public UpdateResult<Product> UpdateProduct(int productId, ProductUpdateParameters productUpdateParameters)
        {
            try
            {
                this.productRepository.UpdateProduct(productId, productUpdateParameters);
                Product product = this.productRepository.GetProduct(productId);

                return new UpdateResult<Product>()
                {
                    Result = product,
                    Success = true
                };
            }
            catch (Exception ex)
            {
                return new UpdateResult<Product>()
                {
                    ExceptionMessage = ex.Message,
                    Success = false
                };
            }
        }
    }
}