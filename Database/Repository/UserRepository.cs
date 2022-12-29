using Database.Converters;
using domain.Logic.Interfaces;
using domain.Models;

namespace Database.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Create(User item)
        {
            _context.Users.Add(item.ToModel());
            return true;
        }

        public bool Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            if (user == default)
                return false;

            _context.Users.Remove(user);
            return true;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users.Select(u => u.ToDomain());
        }

        public User? GetItem(int id)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == id);
            return user?.ToDomain();
        }

        public User? GetUserByLogin(string login)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == login);
            return user?.ToDomain();
        }

        public bool IsUserExists(string login)
        {
            return _context.Users.Any(u => u.UserName == login);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(User item)
        {
            _context.Users.Update(item.ToModel());
            return true;
        }
    }
}
