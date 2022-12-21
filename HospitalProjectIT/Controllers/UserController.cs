using domain.UseCases;
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

        [HttpGet("user")]
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
    }
}
