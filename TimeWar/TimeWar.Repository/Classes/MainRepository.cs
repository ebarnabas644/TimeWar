// <copyright file="MainRepository.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Repository.Classes
{
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using TimeWar.Repository.Interfaces;

    /// <summary>
    /// Main repository class.
    /// </summary>
    /// <typeparam name="T">Entity class.</typeparam>
    public abstract class MainRepository<T> : IMainRepository<T>
        where T : class
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainRepository{T}"/> class.
        /// </summary>
        /// <param name="ctx">Database context object.</param>
        protected MainRepository(DbContext ctx)
        {
            this.Ctx = ctx;
        }

        /// <summary>
        /// Gets database context.
        /// </summary>
        protected DbContext Ctx { get; }

        /// <inheritdoc/>
        public void Create(T entity)
        {
            var local = this.Ctx.Set<T>().FirstOrDefault(entry => entry.Equals(entity));
            if (local != null)
            {
                this.Ctx.Entry(local).State = EntityState.Detached;
            }

            this.Ctx.Entry(entity).State = EntityState.Added;
            this.Ctx.Add(entity);
            this.Ctx.SaveChanges();
        }

        /// <inheritdoc/>
        public void Delete(T entity)
        {
            var local = this.Ctx.Set<T>().FirstOrDefault(entry => entry.Equals(entity));
            if (local != null)
            {
                this.Ctx.Entry(local).State = EntityState.Detached;
            }

            this.Ctx.Entry(entity).State = EntityState.Deleted;
            this.Ctx.SaveChanges();
        }

        /// <inheritdoc/>
        public IQueryable<T> GetAll()
        {
            return this.Ctx.Set<T>();
        }

        /// <inheritdoc/>
        public abstract T GetOne(int id);
    }
}
