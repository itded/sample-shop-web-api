using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SampleShopWebApi.Data.Repositories.Interfaces;
using SampleShopWebApi.DTO.Common;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Data.Repositories
{
    public class ProductRepository: BaseRepository, IProductRepository
    {
        public ProductRepository(ShopDbContext shopDbContext) : base(shopDbContext) {
            // nothing
        }

        public Product GetProduct(int productId){
            var product = this.ShopDbContext.Products.Find(productId);
            return MapEntityToDto(product);
        }

        public IList<Product> GetProducts(PageParameters pageParameters) {
            if (pageParameters == null)
            {
                return this.ShopDbContext.Products.AsNoTracking()
                    .Select(x => MapEntityToDto(x)).ToList();
            }
            else
            {
                return this.ShopDbContext.Products.AsNoTracking()
                    .Skip((pageParameters.Page - 1) * pageParameters.PageSize)
                    .Take(pageParameters.PageSize)
                    .Select(x => MapEntityToDto(x)).ToList();
            }
        }

        public void UpdateProduct(int productId, ProductUpdateParameters productUpdateParameters) {
            var product = this.ShopDbContext.Products.Find(productId);

            if (product == null){
                throw new ArgumentNullException(nameof(product));
            }

            if (!productUpdateParameters.ReplaceDescription){
                return;
            }
            
            product.Description = productUpdateParameters.Description;
            
            this.SaveChanges();
        }

        private static Product MapEntityToDto(Entities.Product product) => (product != null) ? new Product()
        {
            Id = product.Id,
            ImgUri = product.ImgUri,
            Description = product.Description,
            Name = product.Name,
            Price = product.Price
        } : null;
    }
}
