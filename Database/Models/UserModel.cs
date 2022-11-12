using domain.Models;

namespace Database.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Fio { get; set; }
        public Role Role { get; set; } = Role.Patient;

        public string UserName { get; set; }
        public string Password { get; set; }
    }

}
