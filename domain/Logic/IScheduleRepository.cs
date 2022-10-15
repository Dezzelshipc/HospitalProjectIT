﻿using domain.Models;

namespace domain.Logic
{
    public interface IScheduleRepository : IRepository<Schedule>
    {
        IEnumerable<Schedule> GetSchedule(Doctor doctor);
        bool AddSchedule(Doctor doctor, Schedule schedule);
        bool UpdateSchedule(Doctor doctor, Schedule schedule);
    }
}