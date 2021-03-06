using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Business.Abstract;
using RentACar.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private IRentalService _rentalService;

        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpPost]
        public IActionResult Add(Rental rental)
        {
            var result = _rentalService.Add(rental);

            if (result.IsSuccees)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("getrentaildetail")]
        public IActionResult GetRentailDetail()
        {
            var result = _rentalService.GetRentalCarsDetail();

            if (result.IsSuccees)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
