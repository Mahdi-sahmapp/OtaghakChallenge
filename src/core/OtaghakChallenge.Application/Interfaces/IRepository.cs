using OtaghakChallenge.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OtaghakChallenge.Application.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        public Task<T> AddAsync(T Entity, CancellationToken cancellationToken);
        public Task<T> UpdateAsync(T Entity);
        public Task DeleteAsync(T Entity);
        public Task<T?> FindAsync(Int64 Id, CancellationToken cancellationToken);
        public IQueryable<T> GetAll();

        public Task ContextSaveChangesAsync(CancellationToken cancellationToken);

    }
}
