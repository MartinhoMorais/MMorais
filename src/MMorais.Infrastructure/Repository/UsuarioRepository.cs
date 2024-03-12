using MMorais.Domain;
using MMorais.Core.Data;
using Microsoft.EntityFrameworkCore;

namespace MMorais.Infrastructure.Repository;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly UsuarioContext _context;
    public UsuarioRepository(UsuarioContext context)
    {
        _context = context;
    }
    public IUnitOfWork UnitOfWork => _context;

    public async Task<IEnumerable<Usuario>> ObterTodos()
    {
        return await _context.Usuarios.
            AsNoTrackingWithIdentityResolution().
            ToListAsync();
    }
    public async Task<Usuario?> ObterPorId(Guid id)
    {
        return await _context.Usuarios.FindAsync(id);
    }

    public async Task<Usuario?> ObterPorCpf(string cpf)
    {
        return await _context.Usuarios.
            Where(x => x.Cpf == cpf).
            FirstOrDefaultAsync();
    }

    public async Task CadastrarUsuario(Usuario usuario)
    {
        await _context.Usuarios.AddAsync(usuario);
    }
    public void AlterarUsuario(Usuario usuario)
    {
        _context.Usuarios.Update(usuario);
    }
    public void RemoverUsuario(Usuario usuario)
    {
        _context.Remove(usuario);
    }
    public void Dispose()
    {
        _context?.Dispose();
    }
}