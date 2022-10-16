namespace UnitTests.ServiceTests
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
            var app = new Appointment(DateTime.MinValue, DateTime.MinValue, -2, -2);
            var sched = new Schedule();
            var res = _appService.SaveAppointment(app, sched);

            Assert.True(res.IsFailure);
            Assert.Contains("Invalid appointment: ", res.Error);
        }

        [Fact]
        public void Save_InvalidSched_F()
        {
            var app = new Appointment(DateTime.MinValue, DateTime.MinValue, 0, 0);
            var sched = new Schedule(-1, DateTime.MinValue, DateTime.MinValue);
            var res = _appService.SaveAppointment(app, sched);

            Assert.True(res.IsFailure);
            Assert.Contains("Invalid schedule: ", res.Error);
        }

        [Fact]
        public void Save_InvalidTime_F()
        {
            var app = new Appointment(DateTime.MaxValue, DateTime.MaxValue, 0, 0);
            var sched = new Schedule(0, DateTime.MinValue, DateTime.MinValue);
            var res = _appService.SaveAppointment(app, sched);

            Assert.True(res.IsFailure);
            Assert.Equal("Appointment out of schedule", res.Error);
        }

        [Fact]
        public void Save_TakenTime_F()
        {
            List<Appointment> apps = new()
            {
                new Appointment(DateTime.MinValue, DateTime.Parse("1000-01-10"), 0, 0),
                new Appointment(DateTime.Parse("1000-01-10"), DateTime.MaxValue, 0, 0)
            };
            _appRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => apps);

            var app = new Appointment(DateTime.Parse("1000-01-01"), DateTime.Parse("1000-01-20"), 0, 0);
            var sched = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
            var res = _appService.SaveAppointment(app, sched);

            Assert.True(res.IsFailure);
            Assert.Equal("Appointment time already taken", res.Error);
        }

        [Fact]
        public void Save_SaveError_F()
        {
            List<Appointment> apps = new();
            _appRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => apps);
            _appRepositoryMock.Setup(x => x.SaveAppointment(It.IsAny<Appointment>())).Returns(() => false);

            var app = new Appointment();
            var sched = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
            var res = _appService.SaveAppointment(app, sched);

            Assert.True(res.IsFailure);
            Assert.Equal("Unable to save appointment", res.Error);
        }

        [Fact]
        public void Save_Valid_P()
        {
            List<Appointment> apps = new();
            _appRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => apps);
            _appRepositoryMock.Setup(x => x.SaveAppointment(It.IsAny<Appointment>())).Returns(() => true);

            var app = new Appointment();
            var sched = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
            var res = _appService.SaveAppointment(app, sched);

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