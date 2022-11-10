using domain.Logic;
using domain.Logic.Interfaces;
using domain.Models;

namespace domain.UseCases
{
    public class DoctorService
    {
        private readonly IDoctorRepository _db;

        public DoctorService(IDoctorRepository db)
        {
            _db = db;
        }

        public Result<Doctor> CreateDoctor(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.IsFailure)
                return Result.Fail<Doctor>("Invalid doctor: " + result.Error);

            return _db.CreateDoctor(doctor) ? Result.Ok(doctor) : Result.Fail<Doctor>("Unable to create doctor");
        }

        public Result<Doctor> DeleteDoctor(int id, IEnumerable<Appointment> appointments)
        {
            if (appointments.Any())
                return Result.Fail<Doctor>("Unable to delete doctor: Doctor has appointments");

            var result = FindDoctor(id);
            if (result.IsFailure)
                return Result.Fail<Doctor>(result.Error);

            return _db.DeleteDoctor(id) ? result : Result.Fail<Doctor>("Unable to delete doctor");
        }

        public Result<IEnumerable<Doctor>> GetAllDoctors()
        {
            return Result.Ok(_db.GetAllDoctors());
        }

        public Result<Doctor> FindDoctor(int id)
        {
            if (id < 0)
                return Result.Fail<Doctor>("Invalid id");

            var doctor = _db.FindDoctor(id);

            return doctor != null ? Result.Ok(doctor) : Result.Fail<Doctor>("Doctor not found");
        }
        public Result<IEnumerable<Doctor>> FindDoctor(Specialization specialization)
        {
            var result = specialization.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<Doctor>>("Invalid specialization: " + result.Error);

            var doctors = _db.FindDoctor(specialization);

            return doctors.Any() ? Result.Ok(doctors) : Result.Fail<IEnumerable<Doctor>>("Doctors not found");
        }
    }
}
