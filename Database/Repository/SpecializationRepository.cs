using Database.Converters;
using domain.Logic.Interfaces;
using domain.Models;

namespace Database.Repository
{
    public class SpecializationRepository : IRepository<Specialization>
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
            var spec = GetItem(id);
            if (spec == default)
                return false;

            _context.Specializations.Remove(spec.ToModel());
            return true;
        }

        public IEnumerable<Specialization> GetAll()
        {
            return _context.Specializations.Select(s => s.ToDomian());
        }

        public Specialization? GetItem(int id)
        {
            return _context.Specializations.FirstOrDefault(s => s.Id == id)?.ToDomian();
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
