namespace MMorais.Core.Data;
public interface IUnitOfWork
{
    Task<bool> Commit();
}