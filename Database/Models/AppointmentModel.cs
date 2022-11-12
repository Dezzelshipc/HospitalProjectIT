namespace Database.Models
{
    public class AppointmentModel
    {
        public int Id;
        public DateTime StartTime;
        public DateTime EndTime;
        public int PatientId;
        public int DoctorId;
    }
}
