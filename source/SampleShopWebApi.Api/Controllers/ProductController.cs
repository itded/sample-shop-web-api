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
        /// </summary>
        /// <returns>List of products and X-Pagination header.</returns>
        [HttpGet(Name = nameof(GetAllProducts)), MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(Product[]), (int)HttpStatusCode.OK)]
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

        /// <summary>
        /// Gets all products using a page filter.
        /// X-Pagination header stores serialized a <see cref="PaginationMetadata"/> object.
        /// </summary>
        /// <param name="page">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>List of products and X-Pagination header.</returns>
        [HttpGet(Name = nameof(GetAllProductsV2)), MapToApiVersion("2.0")]
        [ProducesResponseType(typeof(Product[]), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(void), (int)HttpStatusCode.BadRequest)]
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
            var product = this.productManager.GetProduct(id);
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
        [ProducesResponseType(typeof(UpdateResult<Product>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(UpdateResult<Product>), (int)HttpStatusCode.BadRequest)]
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