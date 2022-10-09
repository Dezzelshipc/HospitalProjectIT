namespace domain
{
    public class User
    {
        public int Id;
        public string PhoneNumber;
        public string Fio;
        public Role Role = Role.Patient;

        public string UserName;
        public string Password;

        public User() { }
        public User(int id, string phoneNumber, string fio, Role role, string userName, string password)
        {
            Id = id;
            PhoneNumber = phoneNumber;
            Fio = fio;
            Role = role;
            UserName = userName;
            Password = password;
        }

        public Result<bool> IsValid()
        {
            if (string.IsNullOrEmpty(UserName))
                return Result.Fail<bool>("Username error");

            if (string.IsNullOrEmpty(Password))
                return Result.Fail<bool>("Password error");

            if (string.IsNullOrEmpty(PhoneNumber))
                return Result.Fail<bool>("Phone number error");

            if (string.IsNullOrEmpty(Fio))
                return Result.Fail<bool>("Fio error");

            return Result.Ok(true);
        }
    }

}
