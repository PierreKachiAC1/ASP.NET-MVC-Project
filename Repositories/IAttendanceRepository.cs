using FinalProject.Models;

namespace FinalProject.Repositories
{
    public interface IAttendanceRepository
    {
        Task<Attendance> GetByIdAsync(int id);
        Task<IEnumerable<Attendance>> GetAllAsync();
        Task AddAsync(Attendance attendance);
        Task UpdateAsync(Attendance attendance);
        Task DeleteAsync(int id);
        Task<Attendance> FindBySessionAndUserId(int sessionId, int userId);

    }
}
