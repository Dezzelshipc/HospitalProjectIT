﻿using domain.Models;
using domain.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace HospitalProjectIT.Controllers
{
    [ApiController]
    [Route("user")]
    public class UserController : ControllerBase
    {
        private readonly UserService _service;
        private readonly Mutex _mutexRegister = new();
        public UserController(UserService service)
        {
            _service = service;
        }

        [HttpGet("get_user")]
        public IActionResult GetUserByLogin(string login)
        {
            if (login == string.Empty)
                return Problem(statusCode: 404, detail: "Не указан логин");

            var userRes = _service.GetUserByLogin(login);
            if (userRes.IsFailure)
                return Problem(statusCode: 404, detail: userRes.Error);

            return Ok(userRes.Value);
        }

        [HttpPost("register")]
        public IActionResult RegisterUser(string username, string password, string phone_number, string fio, Role role)
        {
            User user = new(0, phone_number, fio, role, username, password);

            _mutexRegister.WaitOne();
            var register = _service.Register(user);

            if (register.IsFailure)
                return Problem(statusCode: 404, detail: register.Error);

            _mutexRegister.ReleaseMutex();
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
