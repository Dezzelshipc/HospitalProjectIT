using domain.Models;
using domain.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProjectIT.Controllers
{
    [ApiController]
    [Route("doctor")]
    public class DoctorController : ControllerBase
    {
        private readonly DoctorService _service;
        public DoctorController(DoctorService service)
        {
            _service = service;
        }

        [HttpPost("create")]
        public IActionResult CreateDoctor(string fio, int specialization_id)
        {
            Doctor doctor = new(0, fio, specialization_id);
            var res = _service.CreateDoctor(doctor);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(res.Value);
        }

        [HttpDelete("delete")]
        public IActionResult DeleteDoctor(int id)
        {
            var res = _service.DeleteDoctor(id);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(res.Value);
        }

        [HttpGet("get_all")]
        public IActionResult GetAllDoctors()
        {
            var res = _service.GetAllDoctors();

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(res.Value);
        }

        [HttpGet("find")]
        public IActionResult FindDoctor(int id)
        {
            var res = _service.FindDoctor(id);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(res.Value);
        }

        [HttpGet("get")]
        public IActionResult FindDoctors(int specialization)
        {
            Specialization spec = new(specialization, "a");
            var res = _service.FindDoctors(spec);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(res.Value);
        }
    }
}
