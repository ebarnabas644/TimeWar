// <copyright file="MapRecordRepository.cs" company="Time War">
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
    public class MapRecordRepository : MainRepository<MapRecord>, IMapRecordRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MapRecordRepository"/> class.
        /// </summary>
        /// <param name="ctx">Database context object.</param>
        public MapRecordRepository(DbContext ctx)
            : base(ctx)
        {
        }

        /// <inheritdoc/>
        public override MapRecord GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.MapRecordId == id);
        }

        /// <inheritdoc/>
        public void Update(MapRecord entity)
        {
            if (entity != null)
            {
                var map = this.GetOne(entity.MapRecordId);
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
