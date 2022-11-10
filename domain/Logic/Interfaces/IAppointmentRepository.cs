using domain.Models;

namespace domain.Logic.Interfaces
{
    public interface IAppointmentRepository : IRepository<Appointment>
    {
        bool SaveAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAppointments(int doctorId);
        IEnumerable<Appointment> GetExistingAppointments(Specialization specialization);
        IEnumerable<DateTime> GetFreeAppointments(Specialization specialization);
    }
}
