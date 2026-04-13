using Papa.Facturacion.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Papa.Facturacion.Repositories.Interfaces
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<ICollection<TEntity>> ListAsync();
        Task<ICollection<TResult>> ListAsync<TResult, Tkey>
        (
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, Tkey>> orderBy
        );
        Task<(ICollection<TResult> Result, int TotalRows)> ListAsync<TResult, Tkey>
        (
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, Tkey>> orderBy,
            int page = 1, int pageSize = 10
        );
        Task<TEntity?> GetByIdAsync(int id);
        Task<TEntity> AddAsync(TEntity entity);
        Task UpdateAsync();
        Task<int> DeleteAsync(int id);
        Task<ICollection<TResult>> ExecuteSpASync<TResult>(string query, object[] parameters);
    }
}
