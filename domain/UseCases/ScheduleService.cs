using domain.Logic;
using domain.Models;

namespace domain.UseCases
{
    public class ScheduleService
    {
        private readonly IScheduleRepository _db;

        public ScheduleService(IScheduleRepository db)
        {
            _db = db;
        }

        public Result<IEnumerable<Schedule>> GetSchedule(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<Schedule>>("Invalid doctor: " + result.Error);

            return Result.Ok(_db.GetSchedule(doctor));
        }
        public Result AddSchedule(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.IsFailure)
                return Result.Fail("Invalid doctor: " + result.Error);

            return _db.AddSchedule(doctor) ? Result.Ok() : Result.Fail<Schedule>("Unable to create doctor");
        }
        public Result UpdateSchedule(Doctor doctor)
        {
            var result = doctor.IsValid();
            if (result.IsFailure)
                return Result.Fail<Schedule>("Invalid doctor: " + result.Error);

            return _db.UpdateSchedule(doctor) ? Result.Ok() : Result.Fail("Unable to create doctor");
        }
    }
}
