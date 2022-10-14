using domain.Logic;
using domain.Models;

namespace domain.UseCases
{
    public class AppointmentService
    {
        private readonly IAppointmentRepository _db;

        public AppointmentService(IAppointmentRepository db)
        {
            _db = db;
        }

        public Result<Appointment> SaveAppointment(Appointment appointment)
        {
            var result = appointment.IsValid();
            if (result.IsFailure)
                return Result.Fail<Appointment>("Invalid appointment: " + result.Error);

            return _db.SaveAppointment(appointment) ? Result.Ok(appointment) : Result.Fail<Appointment>("Unable to save appointment");
        }

        public Result<IEnumerable<Appointment>> GetAppointments(Specialization specialization)
        {
            var result = specialization.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<Appointment>>("Invalid specialization: " + result.Error);

            return Result.Ok(_db.GetAppointments(specialization));
        }
    }
}
