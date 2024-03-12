using MMorais.Core.DomainObjects;

namespace MMorais.Core.Data;

public interface IRepository<T> : IDisposable where T : IAggregateRoot
{
    IUnitOfWork UnitOfWork { get; }
}