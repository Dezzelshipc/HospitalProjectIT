
namespace UnitTests.IsValidTest
{
    public class AppointmentIsValid
    {
        [Fact]
        public void EmptyAppointment_P()
        {
            var app = new Appointment();
            var check = app.IsValid();

            Assert.True(check.Success);
        }

        [Fact]
        public void IvalidPatientID_F()
        {
            var app = new Appointment(DateTime.MinValue, DateTime.MinValue, -2, 0);
            var check = app.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid patient id", check.Error);
        }

        [Fact]
        public void IvalidDoctorID_F()
        {
            var app = new Appointment(DateTime.MinValue, DateTime.MinValue, 0, -1);
            var check = app.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid doctor id", check.Error);
        }

        [Fact]
        public void IvalidTime_F()
        {
            var app = new Appointment(DateTime.MaxValue, DateTime.MinValue, 0, 0);
            var check = app.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid time", check.Error);
        }

        [Fact]
        public void ValidAppintment_P()
        {
            var app = new Appointment(DateTime.Now, DateTime.Now, -1, 0);
            var check = app.IsValid();

            Assert.True(check.Success);
        }
    }
}