using FinalProject.Models;

namespace FinalProject.Repositories
{
    public interface ISessionRepository
    {
        Task<Session> GetByIdAsync(int id);
        Task<IEnumerable<Session>> GetAllAsync();
        Task AddAsync(Session session);
        Task UpdateAsync(Session session);
        Task DeleteAsync(int id);
    }
}
