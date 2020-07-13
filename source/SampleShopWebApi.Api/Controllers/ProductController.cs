using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using SampleShopWebApi.Api.Settings;
using SampleShopWebApi.Business.Interfaces;
using SampleShopWebApi.DTO.Common;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Api.Controllers
{
    /// <summary>
    /// Represents a Product controller.
    /// Configuration of the controller is stored in the <see cref="ApiControllerSettings"/> section.
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/v{version:apiVersion}/products")]
    public class ProductController : ControllerBase
    {
        private readonly IProductManager productManager;
        private readonly int defaultPageSize;

        /// <summary>
        /// .Ctor
        /// </summary>
        /// <param name="settings">Controller settings.</param>
        /// <param name="productManager">Product manager.</param>
        public ProductController(IOptions<ApiControllerSettings> settings, IProductManager productManager)
        {
            this.productManager = productManager;
            this.defaultPageSize = settings.Value.DefaultPageSize;
        }

        /// <summary>
        /// Gets all products.
        /// An X-Pagination header stores a serialized <see cref="PaginationMetadata"/> object.
        /// </summary>
        /// <returns>List of products and the X-Pagination header.</returns>
        [HttpGet(Name = nameof(GetAllProducts)), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IList<Product>), (int)HttpStatusCode.OK)]
        public ActionResult GetAllProducts()
        {
            var productPageResult = this.productManager.GetAllProducts();
            var products = productPageResult.Result;

            int page = 1;
            int pageSize = productPageResult.TotalCount;

            AddPaginationHeaderToResponse(productPageResult, nameof(GetAllProducts), page, pageSize);

            return Ok(products);
        }

        /// <summary>
        /// Gets all products using a page filter.
        /// An X-Pagination header stores a serialized <see cref="PaginationMetadata"/> object.
        /// </summary>
        /// <param name="page">A page index.</param>
        /// <param name="pageSize">A size of the page.</param>
        /// <returns>List of products and the X-Pagination header.</returns>
        [HttpGet(Name = nameof(GetAllProductsV2)), MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(IList<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public ActionResult GetAllProductsV2(int? page, int? pageSize)
        {
            if (page <= 0 || pageSize <= 0)
            {
                return BadRequest("Page and PageSize parameters must be greater than 0.");
            }

            // default values
            page ??= 1;
            pageSize ??= defaultPageSize;

            var pageParameters = new PageParameters()
            {
                Page = page.Value,
                PageSize = pageSize.Value
            };

            var productPageResult = productManager.GetProducts(pageParameters);
            var products = productPageResult.Result;

            AddPaginationHeaderToResponse(productPageResult, nameof(GetAllProductsV2), page.Value, pageSize.Value);

            return Ok(products);
        }

        /// <summary>
        /// Gets a product by a given Id.
        /// </summary>
        /// <param name="id">Product Id.</param>
        /// <returns>The found product, otherwise the NotFound code.</returns>
        [HttpGet]
        [Route("{id:int}", Name = nameof(GetProduct))]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.NotFound)]
        public ActionResult GetProduct(int id)
        {
            var product = productManager.GetProduct(id);
            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        /// <summary>
        /// Updates a product using product update parameters.
        /// </summary>
        /// <param name="id">Id of a product to be updated.</param>
        /// <param name="patchRequest">Product update parameters.</param>
        /// <returns>Update result.</returns>
        [HttpPatch]
        [Route("{id:int}", Name = nameof(GetProduct))]
        [ProducesResponseType(typeof(Product), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.BadRequest)]
        public ActionResult PartialUpdateProduct(int id, [FromBody] ProductPatchRequest patchRequest)
        {
            if (patchRequest == null)
            {
                return BadRequest("Product patch request cannot be null");
            }

            var updateProductResult = productManager.UpdateProduct(id, new ProductUpdateParameters()
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

        private void AddPaginationHeaderToResponse<T>(PageResult<T> pageResult, string routeName, int page, int pageSize) where T : class
        {
            if (Response == null)
            {
                return;
            }

            int totalPages = pageResult.TotalCount % pageSize == 0
                ? pageResult.TotalCount / pageSize
                : pageResult.TotalCount / pageSize + 1;

            var prevLink = page > 1 ? Url.Link(routeName,
                new
                {
                    page = page - 1,
                    pageSize = pageSize
                }) : "";

            var nextLink = page < totalPages ? Url.Link(routeName,
                new
                {
                    page = page + 1,
                    pageSize = pageSize
                }) : "";

            var paginationMetadata = new PaginationMetadata()
            {
                CurrentPage = page,
                PageSize = pageSize,
                TotalCount = pageResult.TotalCount,
                TotalPages = totalPages,
                PrevPageLink = prevLink,
                NextPageLink = nextLink
            };

            JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions();
            jsonSerializerOptions.Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping;

            Response?.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata, jsonSerializerOptions));
        }
    }
}