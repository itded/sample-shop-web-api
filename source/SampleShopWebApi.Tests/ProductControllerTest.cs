using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SampleShopWebApi.Api.Controllers;
using SampleShopWebApi.Api.Settings;
using SampleShopWebApi.Business;
using SampleShopWebApi.Data.Mocks.Repositories;
using Xunit;

namespace SampleShopWebApi.Tests
{
    public class ProductControllerTest
    {
        private readonly ProductController productController;

        private const int NotFoundId = 0;
        private const int FoundId = 1;
        private const int IndexFrom = 1;
        private const int IndexTo = 25;
        private const int PageSize = 10;

        public ProductControllerTest()
        {
            var productRepository = new ProductRangeRepository(IndexFrom, IndexTo);
            var productManager = new ProductManager(productRepository);
            var settings = new ApiControllerSettings()
            {
                DefaultPageSize = PageSize
            };

            var options = Options.Create(settings);

            productController = new ProductController(options, productManager);
        }

        /// <summary>
        /// Product Not Found test.
        /// </summary>
        [Fact]
        public void GetProductReturnsNotFoundResult()
        {
            var notFoundResult = productController.GetProduct(NotFoundId);
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        /// <summary>
        /// Product Found test.
        /// </summary>
        [Fact]
        public void GetProductReturnsOkResult()
        {
            var productResult = productController.GetProduct(FoundId);
            Assert.IsType<OkObjectResult>(productResult);

            var productValue = ((OkObjectResult)productResult).Value;
            Assert.IsType<DTO.Products.Product>(productValue);

            var product = (DTO.Products.Product) productValue;
            Assert.Equal(FoundId, product.Id);
        }
    }
}
