using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace TaskManager.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        DbSet<T> Set<T>() where T : class;
        EntityEntry<T> Entry<T>(T entity) where T : class;
        int SaveChanges();
        void UpdateRange(IEnumerable<object> entities);
        DatabaseFacade Database { get; }
    }
}
