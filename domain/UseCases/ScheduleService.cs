using domain.Logic;
using domain.Logic.Interfaces;
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
        public Result AddSchedule(Doctor doctor, Schedule schedule)
        {
            var result = doctor.IsValid();
            if (result.IsFailure)
                return Result.Fail("Invalid doctor: " + result.Error);

            var result1 = schedule.IsValid();
            if (result1.IsFailure)
                return Result.Fail("Invalid schedule: " + result1.Error);

            return _db.AddSchedule(doctor, schedule) ? Result.Ok() : Result.Fail<Schedule>("Unable to add schedule");
        }
        public Result UpdateSchedule(Doctor doctor, Schedule schedule)
        {
            var result = doctor.IsValid();
            if (result.IsFailure)
                return Result.Fail("Invalid doctor: " + result.Error);

            var result1 = schedule.IsValid();
            if (result1.IsFailure)
                return Result.Fail("Invalid schedule: " + result1.Error);

            return _db.UpdateSchedule(doctor, schedule) ? Result.Ok() : Result.Fail("Unable to update schedule");
        }
    }
}
