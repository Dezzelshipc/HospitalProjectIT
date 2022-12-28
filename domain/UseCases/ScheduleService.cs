﻿using domain.Logic;
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

        public Result<Schedule> GetSchedule(int doctorId)
        {
            var res = _db.GetItem(doctorId);
            if (res == default)
                return Result.Fail<Schedule>("Schedule not found");
            return Result.Ok(res);
        }

        public Result AddSchedule(Schedule schedule)
        {
            var result = schedule.IsValid();
            if (result.IsFailure)
                return Result.Fail("Invalid schedule: " + result.Error);
            
            if (_db.Create(schedule))
            {
                _db.Save();
                return Result.Ok();
            }
            return Result.Fail<Schedule>("Unable to add schedule");
        }
        public Result UpdateSchedule(Schedule schedule)
        {
            var result = schedule.IsValid();
            if (result.IsFailure)
                return Result.Fail("Invalid schedule: " + result.Error);

            if (_db.Update(schedule))
            {
                _db.Save();
                return Result.Ok();
            }
            return Result.Fail("Unable to update schedule");
        }
    }
}
