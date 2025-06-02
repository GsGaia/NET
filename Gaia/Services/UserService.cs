using Gaia.Domain.Entity;
using Gaia.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace Gaia.Services;

public class UserService
{
    private readonly DbPostgresql _context;

    public UserService(DbPostgresql context)
    {
        _context = context;
    }

    public async Task<IEnumerable<User>> GetAllAsync()
    {
        return await _context.Users.ToListAsync();
    }

    public Task<User?> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users.FindAsync(id);
    }

    public async Task<User> CreateAsync(User user)
    {
        try
        {
            if (!user.ValidCpf()) throw new Exception("CPF inválido.");
            if (!user.ValidEmail()) throw new Exception("Email inválido.");
            if (!user.ValidPassword()) throw new Exception("Senha inválida.");

            user.creationDate = DateTime.UtcNow;
            user.active = true;

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            // Aqui você pode logar o erro real para facilitar o diagnóstico.
            Console.WriteLine($"Erro ao criar usuário: {ex.ToString()}");
            throw; // Re-throw a exceção após logar
        }
    }


    public async Task<User> UpdateAsync(int id, User updated)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new Exception("Usuário não encontrado.");

        user.name = updated.name;
        user.email = updated.email;
        user.password = updated.password;

        await _context.SaveChangesAsync();
        return user;
    }

    public async Task DeleteAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) throw new Exception("Usuário não encontrado.");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }
}