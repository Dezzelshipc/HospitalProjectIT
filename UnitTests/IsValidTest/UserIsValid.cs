namespace UnitTests.IsValidTest
{
    public class UserIsValid
    {
        [Fact]
        public void EmptyUser_F()
        {
            var user = new User();
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid username", check.Error);
        }

        [Fact]
        public void EmptyUsername_F()
        {
            var user = new User(1, "a", "a", Role.Administrator, "", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid username", check.Error);
        }

        [Fact]
        public void NegativeID_F()
        {
            var user = new User(-1, "a", "a", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid id", check.Error);
        }

        [Fact]
        public void EmptyPass_F()
        {
            var user = new User(1, "a", "a", Role.Administrator, "a", "");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid password", check.Error);
        }

        [Fact]
        public void EmptyPhone_F()
        {
            var user = new User(2, "", "a", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid phone number", check.Error);
        }

        [Fact]
        public void EmptyFio_F()
        {
            var user = new User(3, "a", "", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid fio", check.Error);
        }

        [Fact]
        public void ValidUser_P()
        {
            var user = new User(3, "a", "a", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.Success);
        }
    }
}
