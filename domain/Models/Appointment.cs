using domain.Logic;

namespace domain.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }

        public Appointment() : this(0, DateTime.MinValue, DateTime.MinValue, 0, 0) { }
        public Appointment(DateTime startTime, DateTime endTime, int patientId, int doctorId) 
            : this(0, startTime, endTime, patientId, doctorId) { }
        public Appointment(int id, DateTime startTime, DateTime endTime, int patientId, int doctorId)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            PatientId = patientId;
            DoctorId = doctorId;
        }

        public Result IsValid()
        {
            if (Id < 0)
                return Result.Fail("Invalid id");

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
