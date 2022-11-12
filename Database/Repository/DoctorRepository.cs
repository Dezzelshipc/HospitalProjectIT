using Database.Converters;
using domain.Logic.Interfaces;
using domain.Models;

namespace Database.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly ApplicationContext _context;

        public DoctorRepository(ApplicationContext context)
        {
            _context = context;
        }

        public bool Create(Doctor item)
        {
            _context.Doctors.Add(item.ToModel());
            return true;
        }

        public bool Delete(int id)
        {
            var doctor = GetItem(id);
            if (doctor == default)
                return false;

            _context.Remove(doctor.ToModel());
            return true;
        }

        public Doctor? FindDoctor(Specialization specialization)
        {
            return _context.Doctors.FirstOrDefault(d => d.Specialization == specialization.ToModel())?.ToDomain();
        }

        public IEnumerable<Doctor> FindDoctors(Specialization specialization)
        {
            return _context.Doctors.Where(d => d.Specialization == specialization.ToModel()).Select(d => d.ToDomain());
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _context.Doctors.Select(d => d.ToDomain());
        }

        public Doctor? GetItem(int id)
        {
            return _context.Doctors.FirstOrDefault(d => d.Id == id)?.ToDomain();
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool Update(Doctor item)
        {
            _context.Doctors.Update(item.ToModel());
            return true;
        }
    }
}
