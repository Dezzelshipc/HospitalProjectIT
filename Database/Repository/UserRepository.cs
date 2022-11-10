using Database.Converters;
using domain.Logic.Interfaces;
using domain.Models;

namespace Database.Repository
{
    internal class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public void Create(User item)
        {
            _context.Users.Add(item.ToModel());
        }

        public bool CreateUser(User user)
        {
            // false ?
            Create(user);
            return true;
        }

        public void Delete(int id)
        {
            var user = GetItem(id);
            if (user != default)
                _context.Users.Remove(user.ToModel());

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

        public void Update(User item)
        {
            _context.Update(item.ToModel());
        }
    }
}
