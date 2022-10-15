using domain.Models;

namespace domain.Logic
{
    public interface IUserRepository : IRepository<User>
    {
        bool IsUserExists(string login);
        User? GetUserByLogin(string login);
        bool CreateUser(User user);
    }
}
