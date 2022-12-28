
namespace UnitTests.IsValidTest
{
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
            var doctor = new Doctor(-1, "a", 0);
            var check = doctor.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid id", check.Error);
        }

        [Fact]
        public void EmptyFio_F()
        {
            var doctor = new Doctor(1, "", 0);
            var check = doctor.IsValid();

            Assert.True(check.IsFailure);
            Assert.Equal("Invalid fio", check.Error);
        }

        [Fact]
        public void InvalidSpecialization_F()
        {
            var doctor = new Doctor(0, "a", -1);
            var check = doctor.IsValid();

            Assert.True(check.IsFailure);
            Assert.Contains("Invalid specialization id", check.Error);
        }

        [Fact]
        public void ValidDoctor_P()
        {
            var doctor = new Doctor(0, "a", 0);
            var check = doctor.IsValid();

            Assert.True(check.Success);
        }
    }
}