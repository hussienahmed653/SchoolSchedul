using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Domain;
using SchoolSchedule.Infrastructure.DbConext;

namespace SchoolSchedule.Infrastructure.Departements.Persistence
{
    internal class DepartementRepository : IDepartementRepository
    {
        private readonly ApplicationDbContext _context;

        public DepartementRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Departement>> GetByIdAsync(int classId)
        {
            return await _context.Departements
                .Where(d => d.GradeId == classId)
                .ToListAsync();
        }
        public async Task<List<Departement>> GetAllAsync()
        {
            return await _context.Departements
                .GroupBy(d => d.DepartementName)
                .Select(g => g.First())
                .ToListAsync();
        }
    }
}
