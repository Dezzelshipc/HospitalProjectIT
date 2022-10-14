using domain.Logic;

namespace domain.Models
{
    public class Specialization
    {
        public int Id;
        public string Name;

        public Specialization() : this(0, "") { }

        public Specialization(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public Result IsValid()
        {
            if (Id < 0)
                return Result.Fail("Invalid id");

            if (string.IsNullOrEmpty(Name))
                return Result.Fail("Invalid name");

            return Result.Ok();
        }
    }

    
}
