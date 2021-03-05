using RentACar.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RentACar.Entities.Dtos
{
    public class BrandDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
