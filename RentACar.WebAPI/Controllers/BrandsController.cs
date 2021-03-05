using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RentACar.Business.Abstract;
using RentACar.Entities.Concrete;
using RentACar.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RentACar.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        private IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            var result = _brandService.GetAll();

            if (!result.IsSuccees)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpGet("getbyid/{brandId}")]
        public IActionResult GetById(int brandId)
        {
            var result = _brandService.GetByBrandId(brandId);

            if (!result.IsSuccees)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Add(Brand brand)
        {
            var result = _brandService.Add(brand);

            if (!result.IsSuccees)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Brand brand)
        {
            var result = _brandService.Update(brand);

            if (!result.IsSuccees)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _brandService.Delete(id);

            if (!result.IsSuccees)
                return BadRequest(result);

            return Ok(result);
        }

      
    }
}
