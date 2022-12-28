using Database.Models;
using domain.Models;

namespace Database.Converters
{
    public static class DomainModelDoctorConverter
    {
        public static DoctorModel ToModel(this Doctor model)
        {
            return new DoctorModel
            {
                Id = model.Id,
                Fio = model.Fio,
                SpecializationId = model.SpecializationId
            };
        }

        public static Doctor ToDomain(this DoctorModel model)
        {
            return new Doctor
            {
                Id = model.Id,
                Fio = model.Fio,
                SpecializationId = model.SpecializationId
            };
        }
    }
}
