using domain.Logic;
using domain.Models;

namespace domain.UseCases
{
    public class UserService {
        private IUserRepository _db;

        public UserService(IUserRepository db)
        { 
            _db = db;
        }

        public Result<User> Register(User user) 
        {
            var check = user.IsValid();
            if (check.IsFailure)
                return Result.Fail<User>(check.Error);

            if (_db.IsUserExists(user.UserName))
                return Result.Fail<User>("Username already exists");


            return _db.CreateUser(user) ? Result.Ok(user) : Result.Fail<User>("User creating error");
        }

        public Result<User> GetUserByLogin(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<User>("Login error");

            return _db.IsUserExists(login) ? Result.Ok(_db.GetUserByLogin(login)) : Result.Fail<User>("User not found");
        }

        public Result<bool> IsUserExists(string login)
        {
            if (string.IsNullOrEmpty(login))
                return Result.Fail<bool>("Login error");

            return _db.IsUserExists(login) ? Result.Ok(true) : Result.Fail<bool>("User not found");
        }
    }
}
