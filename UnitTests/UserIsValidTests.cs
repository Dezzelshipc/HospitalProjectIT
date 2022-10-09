namespace UnitTests
{
    public class UserIsValidTests
    {
        [Fact]
        public void EmptyUser()
        {
            var user = new User();
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Username error", check.Error);
        }

        [Fact]
        public void PassError( )
        {
            var user = new User(1, "a", "a", Role.Administrator, "a", "");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Password error", check.Error);
        }

        [Fact]
        public void PhoneError()
        {
            var user = new User(2, "", "a", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Phone number error", check.Error);
        }

        [Fact]
        public void FioError()
        {
            var user = new User(3, "a", "", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Fio error", check.Error);
        }
    }
}