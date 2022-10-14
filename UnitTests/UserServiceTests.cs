namespace ServiceTests
{
    public class UserServiceTests
    {
        private readonly UserService _userService;
        private readonly Mock<IUserRepository> _userRepositoryMock;

        public UserServiceTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public void Get_InvalidLogin_F()
        {
            var res = _userService.GetUserByLogin(string.Empty);

            Assert.True(res.IsFailure);
            Assert.Equal("Invalid login", res.Error);
        }

        [Fact]
        public void Get_NotFound_F()
        {
            _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => null);

            var res = _userService.GetUserByLogin("qwertyuiop");

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to find user", res.Error);
        }

        [Fact]
        public void Get_Found_P()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExists("qwertyuiop"))
                .Returns(() => true);
            _userRepositoryMock.Setup(repository => repository.GetUserByLogin("qwertyuiop"))
                .Returns(() => new User(0, "a", "a", Role.Patient, "qwertyuiop", "a"));

            var res = _userService.GetUserByLogin("qwertyuiop");

            Assert.True(res.Success);
        }

        [Fact]
        public void Exists_InvalidLogin_F()
        {
            var res = _userService.IsUserExists(string.Empty);

            Assert.True(res.IsFailure);
            Assert.Equal("Invalid login", res.Error);
        }

        [Fact]
        public void Exists_NotFound_P()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<string>()))
                .Returns(() => false);

            var res = _userService.IsUserExists("qwertyuiop123123");

            Assert.True(res.Success);
            Assert.False(res.Value);
        }

        [Fact]
        public void Exists_Found_P()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<string>()))
                .Returns(() => true);

            var res = _userService.IsUserExists("qwertyuiop123123");

            Assert.True(res.Success);
            Assert.True(res.Value);
        }

        [Fact]
        public void Register_EmptyUser_F()
        {
            var res = _userService.Register(new User());

            Assert.True(res.IsFailure);
            Assert.Contains("Invalid user: ", res.Error);
        }

        [Fact]
        public void Register_UserExists_F()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<string>()))
                .Returns(() => true);

            var res = _userService.Register(new User(1, "a", "a", Role.Patient, "a", "a"));

            Assert.True(res.IsFailure);
            Assert.Equal("Username already exists", res.Error);
        }

        [Fact]
        public void Register_CreateError_F()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<string>()))
                .Returns(() => false);

            _userRepositoryMock.Setup(repository => repository.CreateUser(It.IsAny<User>()))
                .Returns(() => false);

            var res = _userService.Register(new User(1, "a", "a", Role.Patient, "a", "a"));

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to create user", res.Error);
        }

        [Fact]
        public void Register_Success_P()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<string>()))
                .Returns(() => false);

            _userRepositoryMock.Setup(repository => repository.CreateUser(It.IsAny<User>()))
                .Returns(() => true);

            var res = _userService.Register(new User(1, "a", "a", Role.Patient, "a", "a"));

            Assert.True(res.Success);
        }
    }
}
