using Gaia.Domain.Entity;
using Gaia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gaia.Services {

    public class UserService
    {
        private readonly DbOracle _context;

        public UserService(DbOracle context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(u => u.Requestions)
                .ToListAsync();

        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await _context.Users
                .Include(u => u.Requestions)
                .FirstOrDefaultAsync(u => u.IdUser == id);
        }

        public async Task<User> CreateAsync(User user)
        {
            try
            {
                if (!user.ValidCpf()) throw new Exception("CPF inválido.");
                if (!user.ValidEmail()) throw new Exception("Email inválido.");
                if (!user.ValidPassword()) throw new Exception("Senha inválida.");

                user.CreationDate = DateTime.UtcNow;

                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                return user;
            }
            catch (DbUpdateException dbEx)
            {
                var innerMessage = dbEx.InnerException?.Message ?? dbEx.Message;
                Console.WriteLine($"Erro ao salvar dados no banco: {innerMessage}");
                throw new Exception($"Erro no banco: {innerMessage}", dbEx);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar usuário: {ex.Message}");
                throw;
            }
        }



        public async Task<User> UpdateAsync(long id, User updated)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new Exception("Usuário não encontrado.");

            user.Name = updated.Name;
            user.Email = updated.Email;
            user.Password = updated.Password;
            user.TypeUser = updated.TypeUser;
            //user.Cpf = updated.Cpf;
            //user.CreationDate = updated.CreationDate; 

            if (!user.ValidEmail()) throw new Exception("Email inválido.");
            if (!user.ValidPassword()) throw new Exception("Senha inválida.");

            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(long id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new Exception("Usuário não encontrado.");

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
}