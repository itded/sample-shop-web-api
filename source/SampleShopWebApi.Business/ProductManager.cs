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

        public Product UpdateProduct(int productId, ProductUpdateParameters productUpdateParameters)
        {
            this.productRepository.UpdateProduct(productId, productUpdateParameters);
            return this.productRepository.GetProduct(productId);
        }
    }
}