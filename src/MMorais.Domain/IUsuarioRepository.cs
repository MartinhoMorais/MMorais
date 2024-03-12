using MMorais.Core.Data;

namespace MMorais.Domain;

public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<IEnumerable<Usuario>> ObterTodos();
    Task<Usuario?> ObterPorId(Guid id);
    Task<Usuario?> ObterPorCpf(string cpf);
    Task CadastrarUsuario(Usuario usuario);
    void AlterarUsuario(Usuario usuario);
    void RemoverUsuario(Usuario usuario);
}