namespace UnitTests.ServiceTests
{
    public class ScheduleServiceTests
    {
        private readonly ScheduleService _scheduleService;
        private readonly Mock<IScheduleRepository> _scheduleRepositoryMock;

        public ScheduleServiceTests()
        {
            _scheduleRepositoryMock = new Mock<IScheduleRepository>();
            _scheduleService = new ScheduleService(_scheduleRepositoryMock.Object);
        }

        [Fact]
        public void Get_Ivalid_F()
        {
            var doctor = new Doctor();
            var result = _scheduleService.GetSchedule(doctor);

            Assert.True(result.IsFailure);
            Assert.Contains("Invalid doctor: ", result.Error);
        }

        [Fact]
        public void Get_Valid_P()
        {
            List<Schedule> scheds = new()
            {
                new Schedule(),
                new Schedule()
            };
            IEnumerable<Schedule> s = scheds;
            _scheduleRepositoryMock.Setup(rep => rep.GetSchedule(It.IsAny<Doctor>())).Returns(() => s);

            var doctor = new Doctor(0, "a", new Specialization(0, "a"));
            var result = _scheduleService.GetSchedule(doctor);

            Assert.True(result.Success);
        }

        [Fact]
        public void Add_IvalidSchedule_F()
        {
            var schedule = new Schedule(-1, DateTime.MinValue, DateTime.MinValue);
            var result = _scheduleService.AddSchedule(schedule);

            Assert.True(result.IsFailure);
            Assert.Contains("Invalid schedule: ", result.Error);
        }

        [Fact]
        public void Add_Error_F()
        {
            _scheduleRepositoryMock.Setup(rep => rep.Create(It.IsAny<Schedule>())).Returns(() => false);

            var schedule = new Schedule();
            var result = _scheduleService.AddSchedule(schedule);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to add schedule", result.Error);
        }

        [Fact]
        public void Add_Valid_P()
        {
            _scheduleRepositoryMock.Setup(rep => rep.Create(It.IsAny<Schedule>())).Returns(() => true);

            var schedule = new Schedule();
            var result = _scheduleService.AddSchedule(schedule);

            Assert.True(result.Success);
        }

        [Fact]
        public void Update_IvalidSchedule_F()
        {
            var schedule = new Schedule(-1, DateTime.MinValue, DateTime.MinValue);
            var result = _scheduleService.UpdateSchedule(schedule);

            Assert.True(result.IsFailure);
            Assert.Contains("Invalid schedule: ", result.Error);
        }

        [Fact]
        public void Update_Error_F()
        {
            _scheduleRepositoryMock.Setup(rep => rep.Update(It.IsAny<Schedule>())).Returns(() => false);

            var schedule = new Schedule();
            var result = _scheduleService.UpdateSchedule(schedule);

            Assert.True(result.IsFailure);
            Assert.Equal("Unable to update schedule", result.Error);
        }

        [Fact]
        public void Update_Valid_P()
        {
            _scheduleRepositoryMock.Setup(rep => rep.Update(It.IsAny<Schedule>())).Returns(() => true);

            var schedule = new Schedule();
            var result = _scheduleService.UpdateSchedule(schedule);

            Assert.True(result.Success);
        }
    }
}
