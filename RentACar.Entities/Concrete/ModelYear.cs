using RentACar.Core.Entities.Abstract;

namespace RentACar.Entities.Concrete
{
    public class ModelYear : IEntity
    {
        public int Id { get; set; }
        public int Year { get; set; }
    }
}
