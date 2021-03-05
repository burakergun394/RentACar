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
    public class ModelYearsController : ControllerBase
    {
        private IModelYearService _modelYearService;

        public ModelYearsController(IModelYearService modelYearService)
        {
            _modelYearService = modelYearService;
        }

        [HttpPost]
        public IActionResult Add(ModelYear modelYear)
        {
            var result = _modelYearService.Add(modelYear);

            if (!result.IsSuccees)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
