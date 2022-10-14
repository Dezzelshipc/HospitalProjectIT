using domain.Logic;

namespace domain.Models
{
    public class Doctor
    {
        public int Id;
        public string Fio;
        public Specialization Specialization;

        public Doctor() : this(0, "", new Specialization()) { }
        public Doctor(int id, string fio, Specialization specialization)
        {
            Id = id;
            Fio = fio;
            Specialization = specialization;
        }
        public Result IsValid()
        {
            if (Id < 0)
                return Result.Fail("Invalid id");

            if (string.IsNullOrEmpty(Fio))
                return Result.Fail("Invalid fio");

            var result = Specialization.IsValid();
            if (result.IsFailure)
                return Result.Fail("Invalid specialization: " + result.Error);

            return Result.Ok();
        }
    }
}
