using Microsoft.EntityFrameworkCore;
using Papa.Facturacion.DataAccess.Context;
using Papa.Facturacion.Entities;
using Papa.Facturacion.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Papa.Facturacion.Repositories.Implementations
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        protected readonly PapaFacturacionContext _context;

        public BaseRepository(PapaFacturacionContext context)
        {
            _context = context;
        }

        //Listar
        public async Task<ICollection<TEntity>> ListAsync()
        {
            var result = await _context.Set<TEntity>()
                .Where(p => p.BEstado)
                .AsNoTracking()
                .ToListAsync();
            return result;
        }

        //Listado con ordenamiento, filtros y sin paginación
        public async Task<ICollection<TResult>> ListAsync<TResult, Tkey>
        (
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, Tkey>> orderBy
        )
        {
            return await _context.Set<TEntity>()
                  .Where(predicate)
                  .AsNoTracking()
                  .OrderBy(orderBy)
                  .Select(selector)
                  .ToListAsync();
        }

        //Listado con ordenamiento, filtros y paginación
        public async Task<(ICollection<TResult> Result, int TotalRows)> ListAsync<TResult, Tkey>
        (
            Expression<Func<TEntity, bool>> predicate,
            Expression<Func<TEntity, TResult>> selector,
            Expression<Func<TEntity, Tkey>> orderBy,
            int page = 1, int pageSize = 10
        )
        {
            var result = await _context.Set<TEntity>()
                  .Where(predicate)
                  .AsNoTracking()
                  .OrderBy(orderBy)
                  .Skip((page - 1) * pageSize)
                  .Take(pageSize)
                  .Select(selector)
                  .ToListAsync();

            var total = await _context.Set<TEntity>()
                  .Where(predicate)
                  .CountAsync();

            return (result, total);
        }

        //Obtener por Id
        public async Task<TEntity?> GetByIdAsync(int id)
        {
            return await _context.Set<TEntity>().FirstOrDefaultAsync(p => p.BEstado && p.IId == id);
        }

        //Agregar
        public async Task<TEntity> AddAsync(TEntity entity)
        {
            var result = await _context.Set<TEntity>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        //Actualizar
        public async Task UpdateAsync()
        {
            await _context.SaveChangesAsync();
        }

        //Eliminación lógica
        public async Task<int> DeleteAsync(int id)
        {
            return await _context.Set<TEntity>()
                .Where(p => p.IId == id)
                .ExecuteUpdateAsync(p => p.SetProperty(p => p.BEstado, false));
        }

        //Ejecutar SP
        public async Task<ICollection<TResult>> ExecuteSpASync<TResult>(string query, object[] parameters)
        {
            var resultado = _context.Database.SqlQueryRaw<TResult>(query, parameters);
            return resultado.ToListAsync().Result;
        }

    }
}
