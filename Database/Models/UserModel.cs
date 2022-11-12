using domain.Models;

namespace Database.Models
{
    public class UserModel
    {
        public int Id;
        public string PhoneNumber;
        public string Fio;
        public Role Role = Role.Patient;

        public string UserName;
        public string Password;
    }

}
