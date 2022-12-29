using Database.Converters;
using domain.Logic.Interfaces;
using domain.Models;

namespace Database.Repository
{
    public class AppointmentsRepository : IAppointmentRepository
    {
        private readonly ApplicationContext _context;

        public AppointmentsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Create(Appointment item)
        {
            _context.Appointments.Add(item.ToModel());
            return true;
        }

        public bool Delete(int id)
        {
            var app = _context.Appointments.FirstOrDefault(a => a.Id == id);
            if (app == default)
                return false;

            _context.Appointments.Remove(app);
            return true;
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.Appointments.Select(a => a.ToDomain());
        }

        public IEnumerable<Appointment> GetAppointments(int doctorId)
        {
            return _context.Appointments.Where(a => a.DoctorId == doctorId).Select(a => a.ToDomain());
        }

        public IEnumerable<Appointment> GetExistingAppointments(Specialization specialization)
        {
            var docs = _context.Doctors.Where(d => d.SpecializationId == specialization.Id);
            return _context.Appointments.Where(a => docs.Any(d => d.Id == a.DoctorId)).Select(a => a.ToDomain());
        }

        public IEnumerable<DateTime> GetFreeAppointments(Specialization specialization, Schedule schedule)
        {
            var docs = _context.Doctors.Where(d => d.SpecializationId == specialization.Id && d.Id == schedule.DoctorId);
            var existing = _context.Appointments.Where(a => docs.Any(d => d.Id == a.DoctorId)).Select(a => a.StartTime);
            List<DateTime> free = new();
            for (DateTime dt = schedule.StartTime; dt < schedule.EndTime; dt.AddMinutes(30))
            {
                if (existing.All(a => a != dt))
                    free.Append(dt);
            }
            return free;
        }

        public Appointment? GetItem(int id)
        {
            return _context.Appointments.FirstOrDefault(a => a.Id == id)?.ToDomain();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Appointment item)
        {
            _context.Appointments.Update(item.ToModel());
            return true;
        }
    }
}
