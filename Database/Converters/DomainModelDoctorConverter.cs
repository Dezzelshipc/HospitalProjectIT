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
                Specialization = model.Specialization
            };
        }

        public static Doctor ToDomian(this DoctorModel model)
        {
            return new Doctor
            {
                Id = model.Id,
                Fio = model.Fio,
                Specialization = model.Specialization
            };
        }
    }
}
