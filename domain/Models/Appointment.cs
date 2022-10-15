using domain.Logic;

namespace domain.Models
{
    public class Appointment
    {
        public DateTime StartTime;
        public DateTime EndTime;
        public int PatientId;
        public int DoctorId;

        public Appointment() : this(DateTime.MinValue, DateTime.MinValue, 0, 0) { }
        public Appointment(DateTime startTime, DateTime endTime, int patientId, int doctorId)
        {
            StartTime = startTime;
            EndTime = endTime;
            PatientId = patientId;
            DoctorId = doctorId;
        }

        public Result IsValid()
        {
            if (PatientId < -1)
                return Result.Fail("Invalid patient id");

            if (DoctorId < 0)
                return Result.Fail("Invalid doctor id");

            if (StartTime > EndTime)
                return Result.Fail("Invalid time");

            return Result.Ok();
        }
    }
}
