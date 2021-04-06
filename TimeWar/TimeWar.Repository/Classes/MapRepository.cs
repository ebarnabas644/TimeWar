// <copyright file="MapRepository.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Repository.Classes
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using TimeWar.Data.Models;
    using TimeWar.Repository.Interfaces;

    /// <summary>
    /// Map entity class.
    /// </summary>
    public class MapRepository : MainRepository<Map>, IMapRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapRepository"/> class.
        /// </summary>
        /// <param name="ctx">Database context object.</param>
        public MapRepository(DbContext ctx)
            : base(ctx)
        {
        }

        /// <inheritdoc/>
        public override Map GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.MapId == id);
        }

        /// <inheritdoc/>
        public void Update(Map entity)
        {
            if (entity != null)
            {
                var map = this.GetOne(entity.MapId);
                if (map == null)
                {
                    throw new InvalidOperationException("Entity not found");
                }

                map.RunTime = entity.RunTime;
                this.Ctx.SaveChanges();
            }
        }
    }
}
