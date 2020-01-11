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
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [Route("{id?}")]
        [HttpGet]
        public async Task<IActionResult> Get(int? id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Message = "Customer id is required",
                        Code = 400
                    });
                }

                Customer customer = await _customerService.Get(id);
                ResponseData<Customer> response = new ResponseData<Customer>()
                {
                    Code = 200,
                    Data = customer,
                    Message = "Customer fetched."
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
        public async Task<IActionResult> Post(Customer customer)
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
                Customer newCustomer = await _customerService.Add(customer);
                ResponseData<Customer> response = new ResponseData<Customer>()
                {
                    Code = 200,
                    Data = newCustomer,
                    Message = "Customer created."
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
        public async Task<IActionResult> Put([FromRoute]int? id, Customer customer)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Message = "Customer id is required",
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
                if (id != customer.Id)
                {
                    return BadRequest(new ErrorResponse()
                    {
                        Message = "Customer is invalid.",
                        Code = 400
                    });
                }

                Customer newCustomer = await _customerService.Update(customer);
                ResponseData<Customer> response = new ResponseData<Customer>()
                {
                    Code = 200,
                    Data = newCustomer,
                    Message = "Customer updated."
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
                        Message = "Customer id is required",
                        Code = 400
                    });
                }

                int x = await _customerService.Delete(id);
                ResponseData<Customer> response = new ResponseData<Customer>()
                {
                    Code = 200,
                    Message = "Customer deleted."
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