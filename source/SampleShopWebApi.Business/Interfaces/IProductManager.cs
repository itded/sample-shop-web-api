using System;
using System.Collections.Generic;
using SampleShopWebApi.DTO.Common;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Business.Interfaces
{
    public interface IProductManager
    {
        Product GetProduct(int productId);

        IList<Product> GetAllProducts();

        IList<Product> GetProducts(PageParameters pageParameters);

        Product UpdateProduct(int productId, ProductUpdateParameters productUpdateParameters);
    }
}