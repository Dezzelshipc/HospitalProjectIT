using domain.Logic;

namespace domain.Models
{
    public class Schedule
    {
        public int DoctorId;
        public DateTime StartTime;
        public DateTime EndTime;

        public Schedule() : this(0, DateTime.MinValue, DateTime.MinValue) { }

        public Schedule(int doctorId, DateTime startTime, DateTime endTime)
        {
            DoctorId = doctorId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Result IsValid()
        {
            if (DoctorId < 0)
                return Result.Fail("Invalid doctor id");

            if (StartTime > EndTime)
                return Result.Fail("Invalid time");

            return Result.Ok();
        }
    }
}
