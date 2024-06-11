using System.Collections;
using Core.Entities;
using Core.Interfaces;

namespace Infrastructure.Data;

public class UnitOfWork(StoreContext context) : IUnitOfWork
{
    private Hashtable _repositories;

    public void Dispose()
    {
        context.Dispose();
    }

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : BaseEntity
    {
        if (_repositories == null) _repositories = new Hashtable();
        var name = typeof(TEntity).Name;
        if (!_repositories.ContainsKey(name))
        {
            var repositoryType = typeof(GenericRepository<>);
            var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), context);
            _repositories.Add(name, repositoryInstance);
        }

        return (IGenericRepository<TEntity>)_repositories[name];
    }

    public async Task<int> Complete()
    {
        return await context.SaveChangesAsync();
    }
}