using Database.Models;
using domain.Models;

namespace Database.Converters
{
    public static class UserDomainToUserModelConverter
    {
        public static UserModel ToModel(this User model)
        {
            return new UserModel
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
