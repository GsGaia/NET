using Gaia.Domain.Entity;
using Gaia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gaia.Services {
    public class LocationService
    {
        private readonly DbOracle _context;

        public LocationService(DbOracle context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Location>> GetAllAsync()
        {
            return await _context.Locations
                .Include(l => l.Accidents)
                .Include(l => l.Requestions)
                .ToListAsync();
        }

        public async Task<Location?> GetByIdAsync(long id)
        {
            return await _context.Locations
                .Include(l => l.Accidents)
                .Include(l => l.Requestions)
                .FirstOrDefaultAsync(l => l.IdLocation == id);
        }

        public async Task<Location> CreateAsync(Location location)
        {
            try
            {
                _context.Locations.Add(location);
                await _context.SaveChangesAsync();
                return location;
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                Console.WriteLine($"Erro ao salvar no banco: {innerMessage}");
                throw new Exception($"Erro no banco: {innerMessage}", dbEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar localização: {ex.Message}");
                throw;
            }
        }

        public async Task<Location> UpdateAsync(long id, Location updated)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null) throw new Exception("Localização não encontrada.");
            
            location.City = updated.City;
            location.Station  = updated.Station;
            location.StartAccident = updated.StartAccident;
            location.EndAccident = updated.EndAccident;
            location.Status = updated.Status;

            await _context.SaveChangesAsync();
            return location;
        }

        public async Task DeleteAsync(long id)
        {
            var location = await _context.Locations.FindAsync(id);
            if (location == null) throw new Exception("Localização não encontrada.");

            _context.Locations.Remove(location);
            await _context.SaveChangesAsync();
        }
    }
}
