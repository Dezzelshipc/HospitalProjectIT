
namespace UnitTests.IsValidTest
{
    public class ScheduleIsValid
    {
        [Fact]
        public void EmptySchedule_P()
        {
            var schedule = new Schedule();
            var check = schedule.IsValid();

            Assert.True(check.Success);
        }

        [Fact]
        public void InvalidDoctorID_F()
        {
            var schedule = new Schedule(-1, DateTime.MinValue, DateTime.MinValue);
            var check = schedule.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid doctor id", check.Error);
        }

        [Fact]
        public void InvalidTime_F()
        {
            var schedule = new Schedule(0, DateTime.MaxValue, DateTime.MinValue);
            var check = schedule.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid time", check.Error);
        }

        [Fact]
        public void ValidID_P()
        {
            var schedule = new Schedule(0, DateTime.Now, DateTime.Now);
            var check = schedule.IsValid();

            Assert.True(check.Success);
        }
    }
}