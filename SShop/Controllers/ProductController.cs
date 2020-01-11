using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SShop.Models;
using SShop.Services;

namespace SShop.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [Route("{id?}")]
        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            try
            {
                if(id == null)
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Message = "Product id is required",
                        Code = 400
                    });
                }
                Product product = await _productService.Get(id);
                ResponseData<Product> response = new ResponseData<Product>()
                {
                    Code = 200,
                    Data = product,
                    Message = "Product fetched."
                };
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse()
                {
                    Message = ex.Message,
                    Code = 404
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorResponse()
                {
                    Message = "Something went wrong.",
                    Code = 500
                });
            }
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Post(Product product)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseData<ModelStateDictionary>()
                    {
                        Message = "Validation failed.",
                        Data = ModelState,
                        Code = 400
                    });
                }
                Product newProduct = await _productService.Add(product);
                ResponseData<Product> response = new ResponseData<Product>()
                {
                    Code = 200,
                    Data = newProduct,
                    Message = "Product created."
                };
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorResponse()
                {
                    Message = "Something went wrong.",
                    Code = 500
                });
            }
        }

        [HttpPut]
        [Route("{id?}")]
        public async Task<IActionResult> Put([FromRoute]int? id, Product product)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Message = "Product id is required",
                        Code = 400
                    });
                }

                if (!ModelState.IsValid)
                {
                    return BadRequest(new ResponseData<ModelStateDictionary>()
                    {
                        Message = "Validation failed.",
                        Data = ModelState,
                        Code = 400
                    });
                }
                if (id != product.Id)
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Message = "Product is invalid.",
                        Code = 400
                    });
                }
                
                Product newProduct = await _productService.Update(product);
                ResponseData<Product> response = new ResponseData<Product>()
                {
                    Code = 200,
                    Data = newProduct,
                    Message = "Product updated."
                };
                return Ok(response);
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorResponse()
                {
                    Message = "Something went wrong.",
                    Code = 500
                });
            }
        }

        [Route("{id?}")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute]int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Message = "Product id is required",
                        Code = 400
                    });
                }

                int x = await _productService.Delete(id);
                ResponseData<Product> response = new ResponseData<Product>()
                {
                    Code = 200,
                    Message = "Product deleted."
                };
                return Ok(response);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new ErrorResponse()
                {
                    Message = ex.Message,
                    Code = 404
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new ErrorResponse()
                {
                    Message = "Something went wrong.",
                    Code = 500
                });
            }
        }
    }
}