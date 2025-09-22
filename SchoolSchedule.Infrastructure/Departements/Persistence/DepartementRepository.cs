using Microsoft.EntityFrameworkCore;
using SchoolSchedule.Application.Common.Interfaces;
using SchoolSchedule.Application.DTOs;
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

        public async Task<Departement> FindByGradeIdAndDepartementNameAsync(DepartementDto createDepartement)
        {
            return await _context.Departements
                .FirstOrDefaultAsync(d => d.GradeId == createDepartement.GradeId && d.DepartementName == createDepartement.DepartementName);
        }

        public async Task AddAsync(Departement departement)
        {
            departement.DepartementId = await _context.Departements.AnyAsync() ? await _context.Departements.MaxAsync(d => d.DepartementId) + 1 : 1;
            await _context.Departements.AddAsync(departement);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(Departement departement)
        {
            _context.Departements.Remove(departement);
            await _context.SaveChangesAsync();
        }
    }
}
