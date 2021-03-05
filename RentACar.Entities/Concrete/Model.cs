using RentACar.Core.Entities.Abstract;

namespace RentACar.Entities.Concrete
{
    public class Model : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NormalizedName { get; set; }

    }
}
