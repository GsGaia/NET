using Gaia.Domain.Entity;
using Gaia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gaia.Services
{
    public class AccidentService
    {
        private readonly DbOracle _context;

        public AccidentService(DbOracle context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Accident>> GetAllAsync()
        {
            return await _context.Accidents
                .Include(a => a.Location)
                .ToListAsync();
        }

        public async Task<Accident?> GetByIdAsync(long id)
        {
            return await _context.Accidents
                .Include(a => a.Location)
                .FirstOrDefaultAsync(a => a.IdAccident == id);
        }

        public async Task<Accident> CreateAsync(Accident accident)
        {
            try
            {
                _context.Accidents.Add(accident);
                await _context.SaveChangesAsync();
                return accident;
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                Console.WriteLine($"Erro ao salvar dados no banco: {innerMessage}");
                throw new Exception($"Erro no banco: {innerMessage}", dbEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar acidente: {ex.Message}");
                throw;
            }
        }

        public async Task<Accident> UpdateAsync(long id, Accident updated)
        {
            var accident = await _context.Accidents.FindAsync(id);
            if (accident == null) throw new Exception("Acidente não encontrado.");

            accident.DateAccidentStart = updated.DateAccidentStart;
            accident.DateAccidentEnd = updated.DateAccidentEnd;
            accident.TypeAccident = updated.TypeAccident;
            accident.TypeSeverity = updated.TypeSeverity;
            accident.Location = updated.Location;

            await _context.SaveChangesAsync();
            return accident;
        }

        public async Task DeleteAsync(long id)
        {
            var accident = await _context.Accidents.FindAsync(id);
            if (accident == null) throw new Exception("Acidente não encontrado.");

            _context.Accidents.Remove(accident);
            await _context.SaveChangesAsync();
        }
    }
}
