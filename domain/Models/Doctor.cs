using domain.Logic;

namespace domain.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Fio { get; set; }
        public int SpecializationId { get; set; }

        public Doctor() : this(0, "", 0) { }
        public Doctor(int id, string fio, int specialization)
        {
            Id = id;
            Fio = fio;
            SpecializationId = specialization;
        }
        public Result IsValid()
        {
            if (Id < 0)
                return Result.Fail("Invalid id");

            if (string.IsNullOrEmpty(Fio))
                return Result.Fail("Invalid fio");

            if (SpecializationId < 0)
                return Result.Fail("Invalid specialization id");

            return Result.Ok();
        }
    }
}
