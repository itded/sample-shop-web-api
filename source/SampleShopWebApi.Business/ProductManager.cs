using System;
using System.Collections.Generic;
using SampleShopWebApi.Business.Interfaces;
using SampleShopWebApi.Data.Repositories.Interfaces;
using SampleShopWebApi.DTO.Common;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Business
{
    public class ProductManager : IProductManager
    {
        private readonly IProductRepository productRepository;

        public ProductManager(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Product GetProduct(int productId)
        {
            return this.productRepository.GetProduct(productId);
        }

        public IList<Product> GetAllProducts()
        {
            return this.productRepository.GetProducts(null);
        }

        public IList<Product> GetProducts(PageParameters pageParameters)
        {
            return this.productRepository.GetProducts(pageParameters);
        }

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