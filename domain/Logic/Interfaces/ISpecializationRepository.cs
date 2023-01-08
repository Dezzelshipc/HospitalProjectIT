using domain.Models;

namespace domain.Logic.Interfaces
{
    public interface ISpecializationRepository : IRepository<Specialization>
    {
        public Specialization? GetByName(string name);
    }
}