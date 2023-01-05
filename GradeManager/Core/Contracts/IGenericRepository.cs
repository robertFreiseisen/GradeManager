using Shared.Entities;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class, IEntity, new()
    {
        /// <summary>
        /// Eindeutige Entität oder null zurückliefern
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<TEntity> GetByIdAsync(int id);


        /// <summary>
        /// Get all entities in the repository
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<bool> ExistsAsync(int id);


        /// <summary>
        /// Fügt neue Identität zum Datenbestand hinzu
        /// </summary>
        /// <param name="entity"></param>
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);

        /// <summary>
        ///     Liste von Entities einfügen
        /// </summary>
        /// <param name="entities"></param>
        Task AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        ///     Entität per primary key löschen
        /// </summary>
        /// <param name="id"></param>
        bool Remove(int id);

        /// <summary>
        ///     Übergebene Entität löschen.
        /// </summary>
        /// <param name="entityToDelete"></param>
        void Remove(TEntity entityToDelete);

        ///// <summary>
        /////     Entität aktualisieren
        ///// </summary>
        ///// <param name="entityToUpdate"></param>
        //void Update(TEntity entityToUpdate);

        /// <summary>
        ///     Generisches CountAsync mit Filtermöglichkeit. Sind vom Filter
        ///     Navigationproperties betroffen, können diese per eager-loading
        ///     geladen werden.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="includeProperties"></param>
        /// <returns></returns>
        Task<int> CountAsync(Expression<Func<TEntity, bool>>? filter = null,
            params string[] includeProperties);

    }
}