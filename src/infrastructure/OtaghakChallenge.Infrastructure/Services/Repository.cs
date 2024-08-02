using Microsoft.EntityFrameworkCore;
using OtaghakChallenge.Application.Interfaces;
using OtaghakChallenge.Domain.Entities;
using OtaghakChallenge.Persistence.ApplicationDbContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Infrastructure.Services
{
    public class Repository<T>: IRepository<T> where T : BaseEntity
    {
        private readonly ICurrentUser _currentUser;
        private readonly OtaghakDbContext _context;
        private readonly DbSet<T> _dbSet;
        public Repository(OtaghakDbContext context, ICurrentUser currentUser)
        {
            _context = context;
            _dbSet = _context.Set<T>();
            _currentUser = currentUser;
        }

        public async Task<T> AddAsync(T Entity, CancellationToken cancellationToken)
        {
            await _dbSet.AddAsync(Entity, cancellationToken);
            return await Task.FromResult(Entity);
        }

        public async Task<T> UpdateAsync(T Entity)
        {
            _context.Entry(Entity).State = EntityState.Modified;
            return await Task.FromResult(Entity);
        }

        public async Task<T?> FindAsync(Int64 Id, CancellationToken cancellationToken)
        {
            return await _dbSet.FindAsync(Id, cancellationToken);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable().Where(a => a.IsDeleted != true);
        }

        public async Task DeleteAsync(T Entity)
        {
            Entity.IsDeleted = true;
            await UpdateAsync(Entity);
        }

        public async Task ContextSaveChangesAsync(CancellationToken cancellationToken)
        {
            var NewEntryies = _context.ChangeTracker.Entries();
            foreach (var entry in NewEntryies)
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        if (_currentUser.UserId > 0)
                        {
                            entry.Property("CreatedBy").CurrentValue = _currentUser.UserId;
                        }
                        break;
                    case EntityState.Modified:
                        if (_currentUser.UserId > 0)
                            entry.Property("UpdatedBy").CurrentValue = _currentUser.UserId;
                        entry.Property("UpdatedOn").CurrentValue = DateTime.Now;
                        break;
                }

            }

            await _context.SaveChangesAsync();
        }
    }
}
