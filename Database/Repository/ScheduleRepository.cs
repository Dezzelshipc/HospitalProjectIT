using Database.Converters;
using domain.Logic.Interfaces;
using domain.Models;

namespace Database.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly ApplicationContext _context;

        public ScheduleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Create(Schedule item)
        {
            _context.Schedlues.Add(item.ToModel());
            return true;
        }

        public bool Delete(int id)
        {
            var sched = GetItem(id);
            if (sched == default)
                return false;

            _context.Schedlues.Remove(sched.ToModel());
            return true;
        }

        public IEnumerable<Schedule> GetAll()
        {
            return _context.Schedlues.Select(s => s.ToDomain());
        }

        public Schedule? GetItem(int id)
        {
            return _context.Schedlues.FirstOrDefault(s => s.Id == id)?.ToDomain();
        }

        public IEnumerable<Schedule> GetSchedule(Doctor doctor)
        {
            return _context.Schedlues.Where(s => s.DoctorId == doctor.Id).Select(s => s.ToDomain());
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Schedule item)
        {
            _context.Schedlues.Update(item.ToModel());
            return true;
        }
    }
}
