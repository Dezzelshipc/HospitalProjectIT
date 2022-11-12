using domain.Logic;

namespace domain.Models
{
    public class Schedule
    {
        public int Id;
        public int DoctorId;
        public DateTime StartTime;
        public DateTime EndTime;

        public Schedule() : this(0, 0, DateTime.MinValue, DateTime.MinValue) { }

        public Schedule(int doctorId, DateTime startTime, DateTime endTime) : this(0, doctorId, startTime, endTime) { }

        public Schedule(int id, int doctorId, DateTime startTime, DateTime endTime)
        {
            Id = id;
            DoctorId = doctorId;
            StartTime = startTime;
            EndTime = endTime;
        }

        public Result IsValid()
        {
            if (Id < 0)
                return Result.Fail("Invalid id");

            if (DoctorId < 0)
                return Result.Fail("Invalid doctor id");

            if (StartTime > EndTime)
                return Result.Fail("Invalid time");

            return Result.Ok();
        }
    }
}
