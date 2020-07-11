using System;
using System.Collections.Generic;
using SampleShopWebApi.DTO.Common;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Data.Repositories.Interfaces
{
    public interface IProductRepository
    {
        Product GetProduct(int productId);

        IList<Product> GetProducts(PageParameters pageParameters);

        void UpdateProduct(int productId, ProductUpdateParameters productUpdateParameters);
    }
}
