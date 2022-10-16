namespace UnitTests.IsValidTest
{
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