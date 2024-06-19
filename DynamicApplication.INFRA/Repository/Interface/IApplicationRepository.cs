using DynamicApplication.DOMAIN.Models;

namespace DynamicApplication.INFRA.Repository.Interface
{
    public interface IApplicationRepository
    {
        public Task Add<TEntity>(TEntity entity) where TEntity : BaseEntity;
        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : BaseEntity;
        public Task<TEntity?> FindById<TEntity>(long id) where TEntity : BaseEntity;
        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity;
        public void Remove<TEntity>(TEntity entity) where TEntity : BaseEntity;
        public Task<int> SaveChangesAsync();
    }
}
