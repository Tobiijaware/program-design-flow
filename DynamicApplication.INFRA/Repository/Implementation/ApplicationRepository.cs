using DynamicApplication.DOMAIN.Models;
using DynamicApplication.INFRA.Context;
using DynamicApplication.INFRA.Repository.Interface;


namespace DynamicApplication.INFRA.Repository.Implementation
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly CosmosDbContext _context;

        public ApplicationRepository(CosmosDbContext context)
        {
            _context = context;
        }

        public async Task Add<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            await _context.Set<TEntity>().AddAsync(entity);
        }
        public IQueryable<TEntity> GetAll<TEntity>() where TEntity : BaseEntity
        {
            return _context.Set<TEntity>();
        }
        public async Task<TEntity?> FindById<TEntity>(long id) where TEntity : BaseEntity
        {
            return await _context.Set<TEntity>().FindAsync(id) ?? null;
        }
        public void Update<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _context.Set<TEntity>().Update(entity);
        }
        public void Remove<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            _context.Set<TEntity>().Remove(entity);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
