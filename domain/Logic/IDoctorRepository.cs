using domain.Models;

namespace domain.Logic
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        bool CreateDoctor(Doctor doctor);
        bool DeleteDoctor(int id);
        IEnumerable<Doctor> GelAllDoctors();
        Doctor? FindDoctor(int id);
        Doctor? FindDoctor(Specialization specialization);
    }
}
