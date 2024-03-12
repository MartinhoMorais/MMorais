using Dapper;
using System.Data;
using MMorais.Domain;

namespace MMorais.Infrastructure.Repository;

public class UsuarioDapperRepository : IUsuarioDapperRepository
{
    private readonly IDbConnection _dbConnection;

    public UsuarioDapperRepository(IDbConnection dbConnection)
    {
        _dbConnection = dbConnection;
        DefaultTypeMap.MatchNamesWithUnderscores = true;
    }

    public async Task<IEnumerable<Usuario>> ObterTodos()
    {
        string query = "SELECT * FROM public.\"Usuario\";";
        return await _dbConnection.QueryAsync<Usuario>(query);
    }
}