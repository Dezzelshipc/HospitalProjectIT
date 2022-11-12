using Database.Models;
using domain.Models;

namespace Database.Converters
{
    public static class DomainModelSpecializationConverter
    {
        public static SpecializationModel ToModel(this Specialization model)
        {
            return new SpecializationModel
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public static Specialization ToDomain(this SpecializationModel model)
        {
            return new Specialization
            {
                Id = model.Id,
                Name = model.Name
            };
        }
    }
}
