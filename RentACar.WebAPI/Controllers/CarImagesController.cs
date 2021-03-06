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
    public class CarImagesController : ControllerBase
    {
        private ICarImageService _carImageService;

        public CarImagesController(ICarImageService carImageService)
        {
            _carImageService = carImageService;
        }

        [HttpPost]
        public IActionResult Add([FromForm] CarImage carImage, [FromForm] IFormFile image)
        {
            var result = _carImageService.Add(carImage, image);

            if (result.IsSuccees)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete("{carImageId}")]
        public IActionResult Delete(int carImageId)
        {
            var result = _carImageService.Delete(carImageId);

            if (result.IsSuccees)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPut]
        public IActionResult Update([FromForm] CarImage carImage, [FromForm] IFormFile image)
        {
            var result = _carImageService.Update(carImage, image);

            if (result.IsSuccees)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
