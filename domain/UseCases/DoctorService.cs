﻿using domain.Logic;
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

        public Result<Doctor> DeleteDoctor(int id)
        {
            var result = FindDoctor(id);
            if (result.IsFailure)
                return Result.Fail<Doctor>(result.Error);

            return _db.DeleteDoctor(id) ? result : Result.Fail<Doctor>("Unable to delete doctor");
        }

        public Result<IEnumerable<Doctor>> GelAllDoctors()
        {
            return Result.Ok(_db.GelAllDoctors());
        }

        public Result<Doctor> FindDoctor(int id)
        {
            if (id < 0)
                return Result.Fail<Doctor>("Invalid id");

            return Result.Ok(_db.FindDoctor(id));
        }
        public Result<Doctor> FindDoctor(Specialization specialization)
        {
            var result = specialization.IsValid();
            if (result.IsFailure)
                return Result.Fail<Doctor>("Unable to find doctor: " + result.Error);

            return Result.Ok(_db.FindDoctor(specialization));
        }
    }
}