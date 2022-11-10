using domain.Logic;
using domain.Logic.Interfaces;
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

        public Result<Appointment> SaveAppointment(Appointment appointment, Schedule schedule)
        {
            var result = appointment.IsValid();
            if (result.IsFailure)
                return Result.Fail<Appointment>("Invalid appointment: " + result.Error);

            var result1 = schedule.IsValid();
            if (result1.IsFailure)
                return Result.Fail<Appointment>("Invalid schedule: " + result1.Error);

            if (schedule.StartTime > appointment.StartTime || schedule.EndTime < appointment.EndTime)
                return Result.Fail<Appointment>("Appointment out of schedule");

            var apps = _db.GetAppointments(appointment.DoctorId);
            if (apps.Any(a => appointment.StartTime < a.EndTime && a.StartTime < appointment.EndTime))
                return Result.Fail<Appointment>("Appointment time already taken");

            return _db.SaveAppointment(appointment) ? Result.Ok(appointment) : Result.Fail<Appointment>("Unable to save appointment");
        }

        public Result<IEnumerable<Appointment>> GetExistingAppointments(Specialization specialization)
        {
            var result = specialization.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<Appointment>>("Invalid specialization: " + result.Error);

            return Result.Ok(_db.GetExistingAppointments(specialization));
        }

        public Result<IEnumerable<DateTime>> GetFreeAppointments(Specialization specialization)
        {
            var result = specialization.IsValid();
            if (result.IsFailure)
                return Result.Fail<IEnumerable<DateTime>>("Invalid specialization: " + result.Error);

            return Result.Ok(_db.GetFreeAppointments(specialization));
        }
    }
}
