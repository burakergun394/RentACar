using RentACar.Core.Entities.Abstract;
using System;

namespace RentACar.Entities.Concrete
{
    public class Rental : IEntity
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public DateTime RentDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public Boolean IsCarReturn { get; set; }
    }
}
