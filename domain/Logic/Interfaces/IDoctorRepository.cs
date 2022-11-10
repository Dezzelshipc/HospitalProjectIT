using domain.Models;

namespace domain.Logic.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        bool CreateDoctor(Doctor doctor);
        bool DeleteDoctor(int id);
        IEnumerable<Doctor> GetAllDoctors();
        Doctor? FindDoctor(int id);
        IEnumerable<Doctor> FindDoctor(Specialization specialization);
    }
}
