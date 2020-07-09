using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using SampleShopWebApi.DTO.Products;

namespace SampleShopWebApi.Api.Controllers
{
    [ApiController]
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        [HttpGet(Name = nameof(GetAllProducts))]
        public ActionResult GetAllProducts()
        {
            var result = new List<Product>();
            for (int i = 10; i < 20; i++)
            {
                result.Add(
                new Product()
                {
                    Id = i,
                    Name = i.ToString(),
                    Description = i.ToString()
                });
            }

            return Ok(result);
        }

        [HttpGet(Name = nameof(GetAllProductsV2)), MapToApiVersion("2.0")]
        public ActionResult GetAllProductsV2()
        {
            var result = new List<Product>();
            for (int i = 20; i < 25; i++)
            {
                result.Add(
                    new Product()
                    {
                        Id = i,
                        Name = i.ToString(),
                        Description = i.ToString()
                    });
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{id:int}", Name = nameof(GetProduct))]
        public ActionResult GetProduct(int id)
        {
            return Ok(new Product()
                {
                    Id = id,
                    Name = id.ToString(),
                    Description = id.ToString()
                });
        }

        [HttpPatch("{id:int}", Name = nameof(PartialUpdateProduct))]
        public ActionResult PartialUpdateProduct(int id, [FromBody] ProductPatchRequest patchRequest)
        {
            return Ok(new Product()
            {
                Id = id,
                Name = id.ToString(),
                Description = patchRequest.Description
            });
        }
    }
}