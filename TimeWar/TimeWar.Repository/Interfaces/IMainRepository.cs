// <copyright file="IMainRepository.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Repository.Interfaces
{
    using System.Linq;

    /// <summary>
    /// Generic repository interface for common operations.
    /// </summary>
    /// <typeparam name="T">Entity class.</typeparam>
    public interface IMainRepository<T>
        where T : class
    {
        /// <summary>
        /// Get one entity via id.
        /// </summary>
        /// <param name="id">Id of the entity.</param>
        /// <returns>Return the entity object based on id.</returns>
        T GetOne(int id);

        /// <summary>
        /// Get all element from table.
        /// </summary>
        /// <returns>Entites.</returns>
        IQueryable<T> GetAll();

        /// <summary>
        /// Add entity to the table.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        void Create(T entity);

        /// <summary>
        /// Delete entity from the table.
        /// </summary>
        /// <param name="entity">Entity object.</param>
        void Delete(T entity);
    }
}
