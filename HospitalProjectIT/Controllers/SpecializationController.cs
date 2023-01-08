using domain.Logic.Interfaces;
using domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProjectIT.Controllers
{
    [ApiController]
    [Route("specialization")]
    public class SpecializationController : ControllerBase
    {
        private readonly ISpecializationRepository _rep;
        public SpecializationController(ISpecializationRepository rep)
        {
            _rep = rep;
        }

        [Authorize]
        [HttpPost("add")]
        public IActionResult AddSpecialization(string name)
        {
            Specialization specialization = new(0, name);
            var res = specialization.IsValid();
            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            if (_rep.Create(specialization))
            {
                _rep.Save();

                return Ok(_rep.GetByName(name));
            }

            return Problem(statusCode: 404, detail: "Error while creating");
        }

        [Authorize]
        [HttpDelete("delete")]
        public IActionResult DeleteSpecialization(int id)
        {
            if (_rep.Delete(id))
            {
                _rep.Save();

                return Ok();
            }

            return Problem(statusCode: 404, detail: "Error while deleting");

        }

        [HttpGet("get_all")]
        public IActionResult GetAll()
        {
            return Ok(_rep.GetAll());
        }
    }
}
