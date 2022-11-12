using Database.Converters;
using domain.Logic.Interfaces;
using domain.Models;

namespace Database.Repository
{
    internal class AppointmentsRepository : IAppointmentRepository
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
            var app = GetItem(id);
            if (app == default)
                return false;

            _context.Appointments.Remove(app.ToModel());
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
            var docs = _context.Doctors.Where(d => d.Specialization == specialization.ToModel());
            return _context.Appointments.Where(a => docs.Any(d => d.Id == a.DoctorId)).Select(a => a.ToDomain());
        }

        public IEnumerable<DateTime> GetFreeAppointments(Specialization specialization)
        {
            var docs = _context.Doctors.Where(d => d.Specialization == specialization.ToModel());
            return _context.Appointments.Where(a => a.PatientId == -1 && docs.Any(d => d.Id == a.DoctorId)).Select(a => a.StartTime);
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
