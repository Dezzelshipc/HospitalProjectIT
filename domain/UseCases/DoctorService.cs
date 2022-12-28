using domain.Logic;
using domain.Logic.Interfaces;
using domain.Models;

namespace domain.UseCases
{
    public class DoctorService
    {
        private readonly IDoctorRepository _db;
        private readonly IAppointmentRepository _appdb;

        public DoctorService(IDoctorRepository db, IAppointmentRepository appdb)
        {
            _db = db;
            _appdb = appdb;
        }

        public Result<Doctor> CreateDoctor(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.IsFailure)
                return Result.Fail<Doctor>("Invalid doctor: " + result.Error);

            var result1 = FindDoctor(doctor.Id);
            if (result1.Success)
                return Result.Fail<Doctor>("Doctor alredy exists");

            if (_db.Create(doctor) )
            {
                _db.Save();
                return Result.Ok(doctor);
            }
            return Result.Fail<Doctor>("Unable to create doctor");
        }

        public Result<Doctor> DeleteDoctor(int id)
        {
            if (_appdb.GetAppointments(id).Any())
                return Result.Fail<Doctor>("Unable to delete doctor: Doctor has appointments");

            var result = FindDoctor(id);
            if (result.IsFailure)
                return Result.Fail<Doctor>(result.Error);

            if (_db.Delete(id))
            {
                _db.Save();
                return result;
            }
            return Result.Fail<Doctor>("Unable to delete doctor");
        }

        public Result<IEnumerable<Doctor>> GetAllDoctors()
        {
            return Result.Ok(_db.GetAll());
        }

        public Result<Doctor> FindDoctor(int id)
        {
            if (id < 0)
                return Result.Fail<Doctor>("Invalid id");

            var doctor = _db.GetItem(id);

            return doctor != null ? Result.Ok(doctor) : Result.Fail<Doctor>("Doctor not found");
        }
        public Result<IEnumerable<Doctor>> FindDoctors(Specialization specialization)
        {
            var result = specialization.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<Doctor>>("Invalid specialization: " + result.Error);

            var doctors = _db.FindDoctors(specialization);

            return doctors.Any() ? Result.Ok(doctors) : Result.Fail<IEnumerable<Doctor>>("Doctors not found");
        }
    }
}
