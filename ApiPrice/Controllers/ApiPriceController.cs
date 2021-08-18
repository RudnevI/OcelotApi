using ApiPrice.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiPrice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApiPriceController : ControllerBase
    {
        private readonly ILogger<ApiPriceController> _logger;

        public ApiPriceController(ILogger<ApiPriceController> logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAll")]
        public IActionResult GetPrices()

        {
            IEnumerable<Price> prices = InMemoryContext.GetPrices();
            return (prices.Any()) ? (Ok(prices)) : (NotFound("No prices are found"));
        }

        [HttpGet("GetbyCode")]
        public IActionResult GetPrice(string code)
        {
            if (code == "")
            {
                return BadRequest("Empty code");
            }

            Price price = InMemoryContext.GetPriceByCode(code);
            return (price is null) ? (NotFound("Price is not found")) : (Ok(price));
        }

        [HttpPost("Add")]
        public IActionResult AddPrice(string[] args)
        {
            if(args is null)
            {
                return BadRequest("Invalid arg type");
            }
            else if(args.Length < 2)
            {
                return BadRequest("Not enough parameters");
            }

            InMemoryContext.AddPrice(new Price { Code = args[0], Value = Decimal.Parse(args[1]) });
            return Ok();

        }

        [HttpDelete("Delete")]
        public IActionResult DeletePrice(string[] args)
        {
            
                if (args is null)
                {
                    return BadRequest("Invalid arg type");
                }
                else if (args.Length == 0)
                {
                    return BadRequest("Not enough parameters");
                }
            InMemoryContext.DeletePrice(new Price { Code = args[0] });
            return Ok();
        }

        [HttpPut("Update")]
        public IActionResult UpdatePrice(string code, string value)
        {
            if (string.IsNullOrWhiteSpace(code) || string.IsNullOrWhiteSpace(value))
            {
                return BadRequest("Invalid parameters");
            }

            InMemoryContext.UpdatePrice(new Price { Code = code, Value = Decimal.Parse(value) });
            return Ok();
        }

        

    }
}
