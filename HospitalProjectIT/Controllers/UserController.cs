using domain.UseCases;
using domain.Models;
using HospitalProjectIT.Views;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProjectIT.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet("get_user")]
        public ActionResult<UserSearchView> GetUserByLogin(string login)
        {
            if (login == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан логин");

            var userRes = _service.GetUserByLogin(login);
            if (userRes.IsFailure)
                return Problem(statusCode: 404, detail: userRes.Error);

            return Ok(new UserSearchView
            {
                Id = userRes.Value.Id,
                PhoneNumber = userRes.Value.PhoneNumber,
                Fio = userRes.Value.Fio,
                Role = userRes.Value.Role,

                UserName = userRes.Value.UserName,
                Password = userRes.Value.Password
            });
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(string username, string password, string phone_number, string fio, Role role)
        {
            User user = new(0, phone_number, fio, role, username, password);
            var register = _service.Register(user);

            if (register.IsFailure)
                return Problem(statusCode: 404, detail: register.Error);

            return Ok(register.Value);
        }

        [HttpGet("is_user")]
        public IActionResult IsUserExists(string login)
        {
            var res = _service.IsUserExists(login);

            if (res.IsFailure)
                return Problem(statusCode: 404, detail: res.Error);

            return Ok(res.Value);
        }

        [HttpGet("get_all")]
        public IActionResult GetAll()
        {
            return Ok(_service.GetAll().Value);
        }
    }
}
