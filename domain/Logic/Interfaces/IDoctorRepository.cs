using domain.Models;

namespace domain.Logic.Interfaces
{
    public interface IDoctorRepository : IRepository<Doctor>
    {
        //Doctor? FindDoctor(Specialization specialization);
        IEnumerable<Doctor> FindDoctors(Specialization specialization);
    }
}
