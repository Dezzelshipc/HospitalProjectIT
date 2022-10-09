using domain;

namespace UnitTests
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
        public void LoginIsEmptyOrNull_ShouldFail()
        {
            var res = _userService.GetUserByLogin(string.Empty);

            Assert.True(res.IsFailure);
            Assert.Equal("Login error", res.Error);
        }

        [Fact]
        public void UserNotFound_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.GetUserByLogin(It.IsAny<string>()))
                .Returns(() => null);

            var res = _userService.GetUserByLogin("qwertyuiop");

            Assert.True(res.IsFailure);
            Assert.Equal("User not found", res.Error);
        }

        [Fact]
        public void IsUserExists_ShouldFail()
        {
            var res = _userService.IsUserExists(string.Empty);

            Assert.True(res.IsFailure);
            Assert.Equal("Login error", res.Error);
        }

        [Fact]
        public void IsUserExists_NotFound_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<string>()))
                .Returns(() => false);

            var res = _userService.IsUserExists("qwertyuiop123123");

            Assert.True(res.IsFailure);
            Assert.Equal("User not found", res.Error);
        }

        [Fact]
        public void Register_Empty_ShouldFail()
        {
            var res = _userService.Register(new User());

            Assert.True(res.IsFailure);
            Assert.Equal("Username error", res.Error);
        }

        [Fact]
        public void Register_AlreadyExists_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<string>()))
                .Returns(() => true);

            var res = _userService.Register(new User(1, "a", "a", Role.Patient, "a", "a"));

            Assert.True(res.IsFailure);
            Assert.Equal("Username already exists", res.Error);
        }

        [Fact]
        public void Register_Error_ShouldFail()
        {
            _userRepositoryMock.Setup(repository => repository.IsUserExists(It.IsAny<string>()))
                .Returns(() => false);

            _userRepositoryMock.Setup(repository => repository.CreateUser(It.IsAny<User>()))
                .Returns(() => false);

            var res = _userService.Register(new User(1, "a", "a", Role.Patient, "a", "a"));

            Assert.True(res.IsFailure);
            Assert.Equal("User creating error", res.Error);
        }
    }
}
