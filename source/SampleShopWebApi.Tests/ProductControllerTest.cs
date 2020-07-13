using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SampleShopWebApi.Api.Controllers;
using SampleShopWebApi.Api.Settings;
using SampleShopWebApi.Business;
using SampleShopWebApi.Data.Mocks.Repositories;
using SampleShopWebApi.DTO.Products;
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

        /// <summary>
        /// All Products v1.0 test.
        /// </summary>
        [Fact]
        public void GetAllProductsReturnsOkResult_V1_0()
        {
            var productsResult = productController.GetAllProducts();
            Assert.IsType<OkObjectResult>(productsResult);

            var productsValue = ((OkObjectResult)productsResult).Value;
            var products = productsValue as IList<DTO.Products.Product>;

            Assert.True(products != null);
            Assert.Equal(IndexTo - IndexFrom + 1, products.Count);
            Assert.Equal(IndexFrom, products.First().Id);
        }

        /// <summary>
        /// All Products v2.0 test - Returns a  bad request iinvalid page parameters.
        /// </summary>
        /// <param name="page">Test page.</param>
        /// <param name="pageSize">Test page size.</param>
        [Theory]
        [InlineData(0 ,0)]
        [InlineData(0, null)]
        [InlineData(null, 0)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(-5, null)]
        [InlineData(null, -5)]
        public void GetPagedProductsReturnsOkResult_WhenInvalidPaging_V2_0(int? page, int? pageSize)
        {
            var productsResult = productController.GetAllProductsV2(page, pageSize);
            Assert.IsType<BadRequestObjectResult>(productsResult);
            var productsResultValue = ((BadRequestObjectResult)productsResult).Value;
            Assert.IsType<string>(productsResultValue);
        }

        /// <summary>
        /// All Products v2.0 test - various page parameters.
        /// Returns a page contains a different number of entries.
        /// Inline data depends on values of <see cref="PageSize"/>, <see cref="IndexFrom"/> and <see cref="IndexTo"/>.
        /// </summary>
        [Theory]
        [InlineData(1, 1, 1, 1)]
        [InlineData(2, 1, 2, 1)]
        [InlineData(3, null, 2 * PageSize + 1, 5)]
        [InlineData(null, 5, 1, 5)]
        [InlineData(null, null, 1, PageSize)]
        public void GetPagedProductsReturnsOkResult_WhenPaging_V2_0(int? page, int? pageSize, int expectedFirstIndex, int expectedCount)
        {
            var productsResult = productController.GetAllProductsV2(page, pageSize);
            Assert.IsType<OkObjectResult>(productsResult);

            var productsValue = ((OkObjectResult)productsResult).Value;
            var products = productsValue as IList<DTO.Products.Product>;

            Assert.True(products != null);
            Assert.Equal(expectedCount, products.Count);
            Assert.Equal(expectedFirstIndex, products.First().Id);
        }

        /// <summary>
        /// Returns a bad request if updates a not existing product or using empty parameters.
        /// </summary>
        [Fact]
        public void PartialUpdateProductNotFound()
        {
            var request = GetProductPatchRequest();
            var updateResult = productController.PartialUpdateProduct(NotFoundId, request);
            Assert.IsType<BadRequestObjectResult>(updateResult);
            var updateResultValue = ((BadRequestObjectResult) updateResult).Value;
            Assert.IsType<string>(updateResultValue);

            updateResult = productController.PartialUpdateProduct(FoundId, null);
            Assert.IsType<BadRequestObjectResult>(updateResult);
            updateResultValue = ((BadRequestObjectResult)updateResult).Value;
            Assert.IsType<string>(updateResultValue);
        }

        /// <summary>
        /// Updates a product.
        /// </summary>
        [Fact]
        public void PartialUpdateProduct()
        {
            var request = GetProductPatchRequest();
            var updateResult = productController.PartialUpdateProduct(FoundId, request);
            Assert.IsType<OkObjectResult>(updateResult);
            var updateResultValue = ((OkObjectResult)updateResult).Value;
            Assert.IsType<Product>(updateResultValue);

            var product = (Product) updateResultValue;
            Assert.Equal(request.Description, product.Description);
        }

        private ProductPatchRequest GetProductPatchRequest()
        {
            return new ProductPatchRequest()
            {
                Description = "Test"
            };
        }
    }
}
