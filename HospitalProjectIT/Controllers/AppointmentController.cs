using domain.Models;
using domain.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProjectIT.Controllers
{
    [ApiController]
    [Route("appointment")]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _service;
        private readonly ScheduleService _serviceSched;
        private readonly Mutex _mutexSave = new();
        public AppointmentController(AppointmentService service, ScheduleService scheduleService)
        {
            _service = service;
            _serviceSched = scheduleService;
        }

        [HttpPost("save")]
        public IActionResult SaveAppointment(int patient_id, int doctor_id, DateTime start_time, DateTime end_time, int schedule_id)
        {
            Appointment appointment = new(0, start_time, end_time, patient_id, doctor_id);

            _mutexSave.WaitOne();

            var schedule = _serviceSched.GetSchedule(schedule_id);
            if (schedule.IsFailure)
                return Problem(statusCode: 404, detail: schedule.Error);

            var res = _service.SaveAppointment(appointment, schedule.Value);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            _mutexSave.ReleaseMutex();

            return Ok(res.Value);
        }

        [HttpGet("get/existing")]
        public IActionResult GetExistingAppointments(int specialization_id)
        {
            Specialization spec = new(specialization_id, "");
            var res = _service.GetExistingAppointments(spec);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(res.Value);
        }

        [HttpGet("get/free")]
        public IActionResult GetFreeAppointments(int specialization_id, int schedule_id)
        {
            var schedule = _serviceSched.GetSchedule(schedule_id);
            if (schedule.IsFailure)
                return Problem(statusCode: 404, detail: schedule.Error);
            var res = _service.GetFreeAppointments(new Specialization(specialization_id, ""), schedule.Value);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(res.Value);
        }
    }
}
