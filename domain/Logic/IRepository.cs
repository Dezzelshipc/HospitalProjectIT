﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace domain.Logic
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }

    public interface IUserRepository : IRepository<User> 
    {
        bool IsUserExists(string login);
        User GetUserByLogin(string login);
        bool CreateUser(User user);
    }
}