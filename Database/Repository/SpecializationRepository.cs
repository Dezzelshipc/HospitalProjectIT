using Database.Converters;
using domain.Logic.Interfaces;
using domain.Models;

namespace Database.Repository
{
    public class SpecializationRepository : ISpecializationRepository
    {
        private readonly ApplicationContext _context;

        public SpecializationRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Create(Specialization item)
        {
            _context.Specializations.Add(item.ToModel());
            return true;
        }

        public bool Delete(int id)
        {
            var spec = _context.Specializations.FirstOrDefault(s => s.Id == id);
            if (spec == default)
                return false;

            _context.Specializations.Remove(spec);
            return true;
        }

        public IEnumerable<Specialization> GetAll()
        {
            return _context.Specializations.Select(s => s.ToDomain());
        }

        public Specialization? GetByName(string name)
        {
            return _context.Specializations.FirstOrDefault(s => s.Name == name)?.ToDomain();
        }

        public Specialization? GetItem(int id)
        {
            return _context.Specializations.FirstOrDefault(s => s.Id == id)?.ToDomain();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Specialization item)
        {
            _context.Specializations.Update(item.ToModel());
            return true;
        }
    }
}
