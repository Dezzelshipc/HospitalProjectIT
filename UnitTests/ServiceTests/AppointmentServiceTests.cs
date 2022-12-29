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
        public void Save_TakenTimeBetween_F()
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
        public void Save_TakenTimeInner_F()
        {
            List<Appointment> apps = new()
            {
                new Appointment(DateTime.MinValue, DateTime.Parse("1000-01-01"), 0, 0),
                new Appointment(DateTime.Parse("1000-01-01"), DateTime.Parse("1000-01-20"), 0, 0),
                new Appointment(DateTime.Parse("1000-01-20"), DateTime.MaxValue, 0, 0)
            };
            _appRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => apps);

            var app = new Appointment(DateTime.Parse("1000-01-05"), DateTime.Parse("1000-01-15"), 0, 0);
            var sched = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
            var res = _appService.SaveAppointment(app, sched);

            Assert.True(res.IsFailure);
            Assert.Equal("Appointment time already taken", res.Error);
        }

        [Fact]
        public void Save_TakenTimeEqualBoundaries_P()
        {
            List<Appointment> apps = new()
            {
                new Appointment(DateTime.MinValue, DateTime.Parse("1000-01-01"), 0, 0),
                new Appointment(DateTime.Parse("1000-01-20"), DateTime.MaxValue, 0, 0)
            };
            _appRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => apps);
            _appRepositoryMock.Setup(x => x.Create(It.IsAny<Appointment>())).Returns(() => true);

            var app = new Appointment(DateTime.Parse("1000-01-01"), DateTime.Parse("1000-01-20"), 0, 0);
            var sched = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
            var res = _appService.SaveAppointment(app, sched);

            Assert.True(res.Success);
        }

        [Fact]
        public void Save_SaveError_F()
        {
            List<Appointment> apps = new();
            _appRepositoryMock.Setup(x => x.GetAppointments(It.IsAny<int>())).Returns(() => apps);
            _appRepositoryMock.Setup(x => x.Create(It.IsAny<Appointment>())).Returns(() => false);

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
            _appRepositoryMock.Setup(x => x.Create(It.IsAny<Appointment>())).Returns(() => true);

            var app = new Appointment();
            var sched = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
            var res = _appService.SaveAppointment(app, sched);

            Assert.True(res.Success);
        }

        [Fact]
        public void Get_InvalidSpec_F()
        {
            var spec = new Specialization();
            var sched = new Schedule();

            var res = _appService.GetFreeAppointments(spec, sched);

            Assert.True(res.IsFailure);
            Assert.Contains("Invalid specialization: ", res.Error);

            var res2 = _appService.GetExistingAppointments(spec);

            Assert.True(res2.IsFailure);
            Assert.Contains("Invalid specialization: ", res2.Error);
        }

        [Fact]
        public void GetExisting_Valid_P()
        {
            List<Appointment> apps = new()
            {
                new Appointment(),
                new Appointment()
            };

            _appRepositoryMock.Setup(repository => repository.GetExistingAppointments(It.IsAny<Specialization>())).Returns(() => apps);

            var spec = new Specialization(0, "a");
            var res = _appService.GetExistingAppointments(spec);

            Assert.True(res.Success);
        }

        [Fact]
        public void GetFree_Valid_P()
        {
            List<DateTime> dates = new()
            {
                new DateTime(),
                new DateTime()
            };

            _appRepositoryMock.Setup(repository => repository.GetFreeAppointments(It.IsAny<Specialization>(), It.IsAny<Schedule>())).Returns(() => dates);

            var spec = new Specialization(0, "a");
            var sched = new Schedule(0, DateTime.MinValue, DateTime.MaxValue);
            var res = _appService.GetFreeAppointments(spec, sched);

            Assert.True(res.Success);
        }
    }
}