namespace ServiceTests
{
    public class AppointmentServiceTests
    {
        private readonly AppointmentService _appService;
        private readonly Mock<IAppointmentRepository> _appRepositoryMock;

        public AppointmentServiceTests()
        {
            _appRepositoryMock = new Mock<IAppointmentRepository>();
            _appService = new AppointmentService(_appRepositoryMock.Object);
        }

        [Fact]
        public void Save_InvalidApp_F()
        {
            var app = new Appointment(DateTime.MinValue, DateTime.MinValue, -2,-2);
            var res = _appService.SaveAppointment(app);

            Assert.True(res.IsFailure);
            Assert.Contains("Invalid appointment: ", res.Error);
        }

        [Fact]
        public void Save_SaveError_F()
        {
            _appRepositoryMock.Setup(repository => repository.SaveAppointment(It.IsAny<Appointment>())).Returns(() => false);

            var app = new Appointment();
            var res = _appService.SaveAppointment(app);

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to save appointment", res.Error);
        }

        [Fact]
        public void Save_Valid_P()
        {
            _appRepositoryMock.Setup(repository => repository.SaveAppointment(It.IsAny<Appointment>())).Returns(() => true);

            var app = new Appointment();
            var res = _appService.SaveAppointment(app);

            Assert.True(res.Success);
        }

        [Fact]
        public void Get_InvalidSpec_F()
        {
            var spec = new Specialization();
            var res = _appService.GetAppointments(spec);

            Assert.True(res.IsFailure);
            Assert.Contains("Invalid specialization: ", res.Error);
        }

        [Fact]
        public void Get_Valid_P()
        {
            List<Appointment> apps = new()
            {
                new Appointment(),
                new Appointment()
            };
            IEnumerable<Appointment> a = apps;
            _appRepositoryMock.Setup(repository => repository.GetAppointments(It.IsAny<Specialization>())).Returns(() => a);

            var spec = new Specialization(0, "a");
            var res = _appService.GetAppointments(spec);

            Assert.True(res.Success);
        }
    }
}