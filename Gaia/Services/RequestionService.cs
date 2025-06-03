using Gaia.Domain.Entity;
using Gaia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gaia.Services{
    public class RequestionService
    {
        private readonly DbOracle _context;

        public RequestionService(DbOracle context)
        {
            _context = context;
        }

        public async Task<bool> UserExistsAsync(long idUser)
        {
            var count = await _context.Users.CountAsync(u => u.IdUser == idUser);
            Console.WriteLine($"Verificando usuário {idUser}, count={count}");
            return count > 0;
        }



        public async Task<bool> LocationExistsAsync(long locationId)
        {
            var count = await _context.Locations.CountAsync(l => l.IdLocation == locationId);
            Console.WriteLine($"Verificando usuário {locationId}, count={count}");
            return count > 0;
        }

        
        
        public async Task<IEnumerable<Requestion>> GetAllAsync()
        {
            return await _context.Requestions
                .Include(r => r.User)
                .Include(r => r.Location)
                .ToListAsync();
        }

        public async Task<Requestion?> GetByIdAsync(long id)
        {
            return await _context.Requestions
                .Include(r => r.User)
                .Include(r => r.Location)
                .FirstOrDefaultAsync(r => r.IdRequestion == id);
        }

        public async Task<Requestion> CreateAsync(Requestion requestion)
        {
            try
            {
                _context.Requestions.Add(requestion);
                await _context.SaveChangesAsync();
                return requestion;
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                Console.WriteLine($"Erro ao salvar dados no banco: {innerMessage}");
                throw new Exception($"Erro no banco: {innerMessage}", dbEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar solicitação : {ex.Message}");
                throw;
            }
        }

        public async Task<Requestion> UpdateAsync(Requestion requestion, long id)
        {
            var req = await _context.Requestions.FindAsync(id);
            if (req == null) throw new Exception("Solicitação não encontrado.");
            
            req.Title = requestion.Title;
            req.Description = requestion.Description;
            req.Unit = requestion.Unit;
            req.RequestDate = requestion.RequestDate;
            req.IdLocation = requestion.IdLocation;
            req.IdUser = requestion.IdUser;

            
            await _context.SaveChangesAsync();
            return req;
        }

        public async Task DeleteAsync(long id)
        {
            var requestion = await _context.Requestions.FindAsync(id);
            if (requestion == null) throw new Exception("Solicitação não encontrado.");

            _context.Requestions.Remove(requestion);
            await _context.SaveChangesAsync();
        }
    }
}