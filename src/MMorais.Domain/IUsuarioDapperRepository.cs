namespace MMorais.Domain;

public interface IUsuarioDapperRepository
{
    Task<IEnumerable<Usuario>> ObterTodos();
}