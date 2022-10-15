namespace ServiceTests
{
    public class DoctorServiceTests
    {
        private readonly DoctorService _doctorService;
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

        public DoctorServiceTests()
        {
            _doctorRepositoryMock = new Mock<IDoctorRepository>();
            _doctorService = new DoctorService(_doctorRepositoryMock.Object);
        }

        [Fact]
        public void Create_Invalid_F()
        {
            var doctor = new Doctor();
            var result = _doctorService.CreateDoctor(doctor);

            Assert.True(result.IsFailure);
            Assert.Contains("Invalid doctor: ", result.Error);
        }

        [Fact]
        public void Create_CreateError_F()
        {
            _doctorRepositoryMock.Setup(repository => repository.CreateDoctor(It.IsAny<Doctor>())).Returns(() => false);
            var doctor = new Doctor(0, "a", new Specialization(1, "a"));
            var result = _doctorService.CreateDoctor(doctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to create doctor", result.Error);
        }

        [Fact]
        public void Create_Valid_P()
        {
            _doctorRepositoryMock.Setup(repository => repository.CreateDoctor(It.IsAny<Doctor>())).Returns(() => true);
            var doctor = new Doctor(0, "a", new Specialization(1, "a"));
            var result = _doctorService.CreateDoctor(doctor);

            Assert.True(result.Success);
        }

        [Fact]
        public void Delete_IdNotFound_F()
        {
            _doctorRepositoryMock.Setup(repository => repository.DeleteDoctor(It.IsAny<int>())).Returns(() => false);

            var result = _doctorService.DeleteDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Doctor not found", result.Error);
        }

        [Fact]
        public void Delete_DeleteError_F()
        {
            _doctorRepositoryMock.Setup(repository => repository.FindDoctor(It.IsAny<int>())).Returns(() => new Doctor(0, "a", new Specialization(0, "a")));
            _doctorRepositoryMock.Setup(repository => repository.DeleteDoctor(It.IsAny<int>())).Returns(() => false);

            var result = _doctorService.DeleteDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to delete doctor", result.Error);
        }

        [Fact]
        public void Delete_Valid_P()
        {
            _doctorRepositoryMock.Setup(repository => repository.FindDoctor(It.IsAny<int>())).Returns(() => new Doctor(0, "a", new Specialization(0, "a")));
            _doctorRepositoryMock.Setup(repository => repository.DeleteDoctor(It.IsAny<int>())).Returns(() => true);

            var result = _doctorService.DeleteDoctor(0);

            Assert.True(result.Success);
        }

        [Fact]
        public void GetAll_P()
        {
            List<Doctor> doctors = new()
            {
                new Doctor(0, "a", new Specialization(0, "a")),
                new Doctor(1, "as", new Specialization(0, "a"))
            };
            IEnumerable<Doctor> d = doctors;
            _doctorRepositoryMock.Setup(repository => repository.GelAllDoctors()).Returns(() => d);

            var result = _doctorService.GelAllDoctors();

            Assert.True(result.Success);
        }

        [Fact]
        public void FindID_Invalid_F()
        {
            var result = _doctorService.FindDoctor(-1);

            Assert.True(result.IsFailure);
            Assert.Equal("Invalid id", result.Error);
        }

        [Fact]
        public void FindID_NotFound_F()
        {
            _doctorRepositoryMock.Setup(repository => repository.FindDoctor(It.IsAny<int>())).Returns(() => null);

            var result = _doctorService.FindDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Doctor not found", result.Error);
        }

        [Fact]
        public void FindID_Valid_P()
        {
            _doctorRepositoryMock.Setup(repository => repository.FindDoctor(It.IsAny<int>())).Returns(() => new Doctor(0, "a", new Specialization(0, "a")));

            var result = _doctorService.FindDoctor(0);

            Assert.True(result.Success);
        }

        [Fact]
        public void FindSpec_Invalid_F()
        {
            var result = _doctorService.FindDoctor(new Specialization());

            Assert.True(result.IsFailure);
            Assert.Contains("Invalid specialization: ", result.Error);
        }

        [Fact]
        public void FindSpec_NotFound_F()
        {
            _doctorRepositoryMock.Setup(repository => repository.FindDoctor(It.IsAny<Specialization>())).Returns(() => null);

            var result = _doctorService.FindDoctor(new Specialization(0, "a"));

            Assert.True(result.IsFailure);
            Assert.Equal("Doctor not found", result.Error);
        }

        [Fact]
        public void FindSpec_Valid_P()
        {
            _doctorRepositoryMock.Setup(repository => repository.FindDoctor(It.IsAny<Specialization>())).Returns(() => new Doctor(0, "a", new Specialization(0, "a")));

            var result = _doctorService.FindDoctor(new Specialization(0, "a"));

            Assert.True(result.Success);
        }

    }
}
