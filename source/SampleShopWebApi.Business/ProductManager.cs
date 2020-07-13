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
        public PageResult<IList<Product>> GetAllProducts()
        {
            var products = this.productRepository.GetProducts(null);
            return new PageResult<IList<Product>>()
            {
                Result = products,
                TotalCount = products.Count
            };
        }

        /// <inheritdoc />
        public PageResult<IList<Product>> GetProducts(PageParameters pageParameters)
        {
            int count = productRepository.GetProductCount();

            // if no result or page is out of range
            if (count == 0 || 
                count <= (pageParameters.Page - 1) * pageParameters.PageSize)
            {
                return new PageResult<IList<Product>>()
                {
                    Result = new List<Product>(),
                    TotalCount = count
                };
            }

            var products = this.productRepository.GetProducts(pageParameters);
            return new PageResult<IList<Product>>()
            {
                Result = products,
                TotalCount = count
            };
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