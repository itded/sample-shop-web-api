using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SampleShopWebApi.Api.Settings;
using SampleShopWebApi.Business.Interfaces;
using SampleShopWebApi.DTO.Common;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManager productManager;
        private readonly int defaultPageSize;

        public ProductsController(IOptions<ApiControllerSettings> settings, IProductManager productManager)
        {
            this.productManager = productManager;
            this.defaultPageSize = settings.Value.DefaultPageSize;
        }

        [HttpGet(Name = nameof(GetAllProducts))]
        public ActionResult GetAllProducts()
        {
            var result = this.productManager.GetAllProducts();

            int page = 1;
            int pageSize = result.Count;
            var paginationMetadata = new PaginationMetadata()
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = pageSize,
                TotalPages = page,
                PrevPageLink = string.Empty,
                NextPageLink = string.Empty
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(result);
        }

        [HttpGet(Name = nameof(GetAllProductsV2)), MapToApiVersion("2.0")]
        public ActionResult GetAllProductsV2(int? page, int? pageSize)
        {
            // default values
            page ??= 1;
            pageSize ??= defaultPageSize;

            var pageParameters = new PageParameters()
            {
                Page = page.Value,
                PageSize = pageSize.Value
            };

            var result = this.productManager.GetProducts(pageParameters);

            // TODO: implement PaginationMetadata
            var paginationMetadata = new PaginationMetadata()
            {
                CurrentPage = page.Value,
                PageSize = pageSize.Value,
                TotalCount = 0,
                TotalPages = 0,
                PrevPageLink = string.Empty,
                NextPageLink = string.Empty
            };

            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));

            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}", Name = nameof(GetProduct))]
        public ActionResult GetProduct(int id)
        {
            var product = this.productManager.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPatch("{id:int}", Name = nameof(PartialUpdateProduct))]
        public ActionResult PartialUpdateProduct(int id, [FromBody] ProductPatchRequest patchRequest)
        {
            var updateProductResult = this.productManager.UpdateProduct(id, new ProductUpdateParameters()
            {
                Description = patchRequest.Description,
                ReplaceDescription = true
            });

            if (!updateProductResult.Success)
            {
                return BadRequest(updateProductResult.ExceptionMessage);
            }

            return Ok(updateProductResult.Result);
        }
    }
}