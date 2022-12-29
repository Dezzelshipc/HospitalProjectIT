using domain.Logic;

namespace domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; }
        public string Fio { get; set; }
        public Role Role { get; set; } = Role.Patient;

        public string UserName { get; set; }
        public string Password { get; set; }

        public User() : this(0, "", "", Role.Patient, "", "") { }
        public User(int id, string phoneNumber, string fio, Role role, string userName, string password)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            Fio = fio;
            Role = role;
            UserName = userName;
            Password = password;
        }

        public Result IsValid()
        {
            if (Id < 0)
                return Result.Fail("Invalid id");

            if (string.IsNullOrEmpty(UserName))
                return Result.Fail("Invalid username");

            if (string.IsNullOrEmpty(Password))
                return Result.Fail("Invalid password");

            if (string.IsNullOrEmpty(PhoneNumber))
                return Result.Fail("Invalid phone number");

            if (string.IsNullOrEmpty(Fio))
                return Result.Fail("Invalid fio");

            return Result.Ok();
        }
    }

}
