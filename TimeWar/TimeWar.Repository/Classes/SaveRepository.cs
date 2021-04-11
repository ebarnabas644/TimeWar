// <copyright file="SaveRepository.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Repository.Classes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using TimeWar.Data.Models;
    using TimeWar.Repository.Interfaces;

    /// <summary>
    /// Save entity class.
    /// </summary>
    public class SaveRepository : MainRepository<Save>, ISaveRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SaveRepository"/> class.
        /// </summary>
        /// <param name="ctx">Database context object.</param>
        public SaveRepository(DbContext ctx)
            : base(ctx)
        {
        }

        /// <inheritdoc/>
        public override Save GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.Id == id);
        }

        /// <inheritdoc/>
        public void Update(Save entity)
        {
            if (entity != null)
            {
                var save = this.GetOne(entity.Id);
                if (save == null)
                {
                    throw new InvalidOperationException("Entity not found");
                }

                save.Point = entity.Point;
                save.Checkpoint = entity.Checkpoint;
                this.Ctx.SaveChanges();
            }
        }
    }
}
