using System.Threading.Tasks;
using FinalProject.Models;
using FinalProject.Repositories;
using Microsoft.EntityFrameworkCore;
namespace FinalProject.Repositories
{
    public class AttendanceRepository:IAttendanceRepository
    {
        private readonly ApplicationDbContext _context;

        public AttendanceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Attendance> GetByIdAsync(int id)
        {
            return await _context.Attendances.FindAsync(id);
        }

        public async Task<IEnumerable<Attendance>> GetAllAsync()
        {
            return await _context.Attendances.Include(a => a.User).Include(a => a.Session).ToListAsync();
        }

        public async Task AddAsync(Attendance attendance)
        {
            _context.Attendances.Add(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Attendance attendance)
        {
            _context.Attendances.Update(attendance);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
                await _context.SaveChangesAsync();
            }
        }
        public async Task<Attendance> FindBySessionAndUserId(int sessionId, int userId)
        {
            return await _context.Attendances.FirstOrDefaultAsync(a => a.SessionId == sessionId && a.UserId == userId);
        }
    }
}
