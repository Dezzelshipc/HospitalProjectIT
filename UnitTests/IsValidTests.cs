namespace IsValidTests
{
    public class UserIsValid
    {
        [Fact]
        public void EmptyUser_F()
        {
            var user = new User();
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid username", check.Error);
        }

        [Fact]
        public void EmptyUsername_F()
        {
            var user = new User(1, "a", "a", Role.Administrator, "", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid username", check.Error);
        }

        [Fact]
        public void NegativeID_F()
        {
            var user = new User(-1, "a", "a", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid id", check.Error);
        }

        [Fact]
        public void EmptyPass_F()
        {
            var user = new User(1, "a", "a", Role.Administrator, "a", "");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid password", check.Error);
        }

        [Fact]
        public void EmptyPhone_F()
        {
            var user = new User(2, "", "a", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid phone number", check.Error);
        }

        [Fact]
        public void EmptyFio_F()
        {
            var user = new User(3, "a", "", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid fio", check.Error);
        }

        [Fact]
        public void ValidUser_P()
        {
            var user = new User(3, "a", "a", Role.Administrator, "a", "a");
            var check = user.IsValid();

            Assert.True(check.Success);
        }
    }
    public class DoctorIsValid
    {
        [Fact]
        public void EmptyDoctor_F()
        {
            var doctor = new Doctor();
            var check = doctor.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid fio", check.Error);
        }

        [Fact]
        public void NegativeID_F()
        {
            var doctor = new Doctor(-1, "a", new Specialization(0, "a"));
            var check = doctor.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid id", check.Error);
        }

        [Fact]
        public void EmptyFio_F()
        {
            var doctor = new Doctor(1, "", new Specialization(0, "a"));
            var check = doctor.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid fio", check.Error);
        }

        [Fact]
        public void InvalidSpecialization_F()
        {
            var doctor = new Doctor(0, "a", new Specialization());
            var check = doctor.IsValid();

            Assert.True(check.IsFailure);
            Assert.Contains("Invalid specialization: ", check.Error);
        }

        [Fact]
        public void ValidDoctor_P()
        {
            var doctor = new Doctor(0, "a", new Specialization(0, "a"));
            var check = doctor.IsValid();

            Assert.True(check.Success);
        }
    }
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
        public void ValidAppintment_P()
        {
            var app = new Appointment(DateTime.Now, DateTime.Now, -1, 0);
            var check = app.IsValid();

            Assert.True(check.Success);
        }
    }
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
        public void ValidID_P()
        {
            var schedule = new Schedule(0, DateTime.Now, DateTime.Now);
            var check = schedule.IsValid();

            Assert.True(check.Success);
        }
    }
    public class SpecializationIsValid
    {
        [Fact]
        public void EmptySpecialization_F()
        {
            var spec = new Specialization();
            var check = spec.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid name", check.Error);
        }

        [Fact]
        public void InvalidID_F()
        {
            var schedule = new Specialization(-1, "a");
            var check = schedule.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid id", check.Error);
        }

        [Fact]
        public void InvalidName_F()
        {
            var schedule = new Specialization(0, "");
            var check = schedule.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid name", check.Error);
        }

        [Fact]
        public void ValidID_P()
        {
            var schedule = new Specialization(0, "a");
            var check = schedule.IsValid();

            Assert.True(check.Success);
        }
    }
}