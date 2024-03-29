﻿using domain.Models;

namespace domain.Logic.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserExists(string login);
        User? GetUserByLogin(string login);
    }
}
