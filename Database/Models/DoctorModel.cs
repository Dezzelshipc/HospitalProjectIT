namespace Database.Models
{
    public class DoctorModel
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public SpecializationModel Specialization { get; set; }
    }
}
