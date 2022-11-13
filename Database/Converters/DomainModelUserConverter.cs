using Database.Models;
using domain.Models;

namespace Database.Converters
{
    public static class DomainModelUserConverter
    {
        public static UserModel ToModel(this User model)
        {
            return new UserModel
            {
                Id = model.Id,
                PhoneNumber = model.PhoneNumber,
                Fio = model.Fio,
                Role = model.Role,
                UserName = model.UserName,
                Password = model.Password,
            };
        }
        public static User ToDomain(this UserModel model)
        {
            return new User
            {
                Id = model.Id,
                PhoneNumber = model.PhoneNumber,
                Fio = model.Fio,
                Role = model.Role,
                UserName = model.UserName,
                Password = model.Password,
            };
        }
    }
}
