using domain.Models;

namespace domain.Logic
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetItem(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
        void Save();
    }

    public interface IUserRepository : IRepository<User>
    {
        bool IsUserExists(string login);
        User GetUserByLogin(string login);
        bool CreateUser(User user);
    }

    public interface IDoctorRepository : IRepository<Doctor>
    {
        bool CreateDoctor(Doctor doctor);
        bool DeleteDoctor(int id);
        IEnumerable<Doctor> GelAllDoctors();
        Doctor FindDoctor(int id);
        Doctor FindDoctor(Specialization specialization);
    }

    public interface IAppointmentRepository : IRepository<Appointment>
    {
        bool SaveAppointment(Appointment appointment);
        IEnumerable<Appointment> GetAppointments(Specialization specialization);
    }

    public interface IScheduleRepository : IRepository<Schedule>
    {
        IEnumerable<Schedule> GetSchedule(Doctor doctor);
        bool AddSchedule(Doctor doctor);
        bool UpdateSchedule(Doctor doctor);
    }
}
