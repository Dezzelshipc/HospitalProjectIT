using Database.Models;
using domain.Models;

namespace Database.Converters
{
    public static class UserModelToDomainConverter
    {
        public static User ToDomain(this UserModel model)
        {
            return new User
            {
                Id = model.Id,
                PhoneNumber = model.PhoneNumber,
                Fio = model.Fio,
                //Role = model.Role,
                UserName = model.UserName
                //Password = model.Password,
            };
        }
    }
}
