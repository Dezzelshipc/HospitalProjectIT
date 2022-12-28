using Castle.Core.Smtp;
using System.Collections.Generic;

namespace UnitTests.ServiceTests
{
    public class DoctorServiceTests
    {
        private readonly DoctorService _doctorService;
        private readonly Mock<IDoctorRepository> _doctorRepositoryMock;
        private readonly Mock<IAppointmentRepository> _appRepositoryMock;

        public DoctorServiceTests()
        {
            _doctorRepositoryMock = new Mock<IDoctorRepository>();
            _appRepositoryMock = new Mock<IAppointmentRepository>();
            _doctorService = new DoctorService(_doctorRepositoryMock.Object, _appRepositoryMock.Object);
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
        public void Create_IdError_F()
        {
            _doctorRepositoryMock.Setup(r => r.GetItem(It.IsAny<int>())).Returns(() => new Doctor(0, "a", 1));
            var doctor = new Doctor(0, "a", 1);
            var result = _doctorService.CreateDoctor(doctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Doctor alredy exists", result.Error);
        }

        [Fact]
        public void Create_CreateError_F()
        {
            _doctorRepositoryMock.Setup(repository => repository.Create(It.IsAny<Doctor>())).Returns(() => false);
            var doctor = new Doctor(0, "a",1);
            var result = _doctorService.CreateDoctor(doctor);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to create doctor", result.Error);
        }

        [Fact]
        public void Create_Valid_P()
        {
            _doctorRepositoryMock.Setup(repository => repository.Create(It.IsAny<Doctor>())).Returns(() => true);
            var doctor = new Doctor(0, "a", 1);
            var result = _doctorService.CreateDoctor(doctor);

            Assert.True(result.Success);
        }

        [Fact]
        public void Delete_IdNotFound_F()
        {
            List<Appointment> apps = new();

            var result = _doctorService.DeleteDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Doctor not found", result.Error);
        }

        [Fact]
        public void Delete_AppsNotEmpty_F()
        {
            List<Appointment> apps = new()
            {
                new Appointment()
            };
            _appRepositoryMock.Setup(r => r.GetAppointments(It.IsAny<int>())).Returns(() => apps);

            var result = _doctorService.DeleteDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to delete doctor: Doctor has appointments", result.Error);
        }

        [Fact]
        public void Delete_DoctorNotFound_F()
        {
            List<Appointment> apps = new()
            {
                new Appointment()
            }; 
            _appRepositoryMock.Setup(r => r.GetAppointments(It.IsAny<int>())).Returns(() => apps);
            _doctorRepositoryMock.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => null);

            var result = _doctorService.DeleteDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to delete doctor: Doctor has appointments", result.Error);
        }

        [Fact]
        public void Delete_DeleteError_F()
        {
            List<Appointment> apps = new();
            _doctorRepositoryMock.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => new Doctor(0, "a", 0));
            _doctorRepositoryMock.Setup(repository => repository.Delete(It.IsAny<int>())).Returns(() => false);

            var result = _doctorService.DeleteDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to delete doctor", result.Error);
        }

        [Fact]
        public void Delete_Valid_P()
        {
            List<Appointment> apps = new();
            _doctorRepositoryMock.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => new Doctor(0, "a", 0));
            _doctorRepositoryMock.Setup(repository => repository.Delete(It.IsAny<int>())).Returns(() => true);

            var result = _doctorService.DeleteDoctor(0);

            Assert.True(result.Success);
        }

        [Fact]
        public void GetAll_P()
        {
            List<Doctor> doctors = new()
            {
                new Doctor(0, "a", 0),
                new Doctor(1, "as", 0)
            };
            IEnumerable<Doctor> d = doctors;
            _doctorRepositoryMock.Setup(repository => repository.GetAll()).Returns(() => d);

            var result = _doctorService.GetAllDoctors();

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
            _doctorRepositoryMock.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => null);

            var result = _doctorService.FindDoctor(0);

            Assert.True(result.IsFailure);
            Assert.Equal("Doctor not found", result.Error);
        }

        [Fact]
        public void FindID_Valid_P()
        {
            _doctorRepositoryMock.Setup(repository => repository.GetItem(It.IsAny<int>())).Returns(() => new Doctor(0, "a", 0));

            var result = _doctorService.FindDoctor(0);

            Assert.True(result.Success);
        }

        [Fact]
        public void FindSpec_Invalid_F()
        {
            var result = _doctorService.FindDoctors(new Specialization());

            Assert.True(result.IsFailure);
            Assert.Contains("Invalid specialization: ", result.Error);
        }

        [Fact]
        public void FindSpec_NotFound_F()
        {
            _doctorRepositoryMock.Setup(repository => repository.FindDoctors(It.IsAny<Specialization>())).Returns(() => new List<Doctor>());

            var result = _doctorService.FindDoctors(new Specialization(0, "a"));

            Assert.True(result.IsFailure);
            Assert.Equal("Doctors not found", result.Error);
        }

        [Fact]
        public void FindSpec_Valid_P()
        {
            List<Doctor> list = new()
            {
                new Doctor(0, "a", 0)
            };
            _doctorRepositoryMock.Setup(repository => repository.FindDoctors(It.IsAny<Specialization>())).Returns(() => list);

            var result = _doctorService.FindDoctors(new Specialization(0, "a"));

            Assert.True(result.Success);
        }

    }
}
