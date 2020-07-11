using System;
using System.Collections.Generic;
using SampleShopWebApi.Data.Repositories.Interfaces;
using SampleShopWebApi.DTO.Common;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Data.Repositories
{
    public class ProductRepository: IProductRepository
    {
        public ProductRepository() {
            // TODO:
            // this.shopDbContext = shopDbContext;
        }

        public Product GetProduct(int productId){
            // TODO:
            //shopDbContext.Products.FirstOrDefault(x => x.Id == productId);

            throw new NotImplementedException();
        }

        public IList<Product> GetProducts(PageParameters pageParameters) {
            throw new NotImplementedException();
        }

        public void UpdateProduct(int productId, ProductUpdateParameters productUpdateParameters) {
            throw new NotImplementedException();
        }
    }
}
