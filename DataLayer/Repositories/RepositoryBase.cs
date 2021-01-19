using DataLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataLayer.Repositories
{
    public interface IRepositoryBase<T> where T : BaseEntity
    {
        Task<IList<T>> GetAllAsync(bool includeDeleted = false);
        Task<IList<T>> GetDeletedRecordsAsync();
        Task<T> GetByIdAsync(Guid id);
        void Insert(T record);
        void Update(T record);
        void Delete(T record);
        void DeleteRange(List<T> records);
        Task DeleteAsync(Guid id);
        void Remove(T record);
        void RemoveRange(List<T> records);
        Task<T> GetLastRecordAsync();
        List<T> GetRecordsByIds(List<Guid> ids);
    }

    public class RepositoryBase<T> : IDisposable, IRepositoryBase<T> where T : BaseEntity
    {
        private readonly Context _db;
        private readonly DbSet<T> _dbSet;

        public RepositoryBase(Context db)
        {
            _db = db;
            _dbSet = db.Set<T>();
        }

        public async Task<IList<T>> GetAllAsync(bool includeDeleted = false)
        {
            return includeDeleted
                ? await _dbSet.IgnoreQueryFilters().ToListAsync()
                : await _dbSet.ToListAsync();
        }

        protected IQueryable<T> GetRecords(bool includeDeleted = false)
        {
            return includeDeleted ? _dbSet.IgnoreQueryFilters() : _dbSet;
        }

        public async Task<IList<T>> GetDeletedRecordsAsync()
        {
            return await _dbSet.IgnoreQueryFilters()
                .Where(d => d.IsDeleted != true)
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public void Insert(T record)
        {
            record.CreatedAt = DateTime.UtcNow;

            var entry = _db.Entry(record);
            if (entry.State != EntityState.Detached) entry.State = EntityState.Added;
            else _dbSet.Add(record);
        }

        public void Update(T record)
        {
            var entry = _db.Entry(record);
            var otherRecord = _db.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity.Id == record.Id);
            if (otherRecord != null) _db.Entry(otherRecord.Entity).State = EntityState.Detached;
            if (entry.State == EntityState.Detached) _dbSet.Attach(record);
            _db.Entry(record).State = EntityState.Modified;
        }

        public void Delete(T record)
        {
            if (record != null)
                record.IsDeleted = true;
        }

        public void DeleteRange(List<T> records)
        {
            foreach (var record in records)
                if (record != null)
                    Delete(record);
        }

        public async Task DeleteAsync(Guid id)
        {
            var record = await GetByIdAsync(id);
            if (record == null) return;

            Delete(record);
        }

        public void Remove(T record)
        {
            if (record != null)
                _dbSet.Remove(record);
        }

        public void RemoveRange(List<T> records)
        {
            if (records.All(x => x != null))
                _dbSet.RemoveRange(records);
        }

        public async Task<T> GetLastRecordAsync()
        {
            return await _dbSet.OrderByDescending(x => x.CreatedAt)
                .LastOrDefaultAsync();
        }

        public virtual void Dispose()
        {
            _db?.Dispose();
        }

        public List<T> GetRecordsByIds(List<Guid> ids)
        {
            var list = GetRecords().Where(x => ids.Contains(x.Id)).ToList();
            return ids.Count == list.Count ? list : null;
        }
    }
}