// <copyright file="ProfileRepository.cs" company="Time War">
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
    /// Profile entity class.
    /// </summary>
    public class ProfileRepository : MainRepository<Profile>, IProfileRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProfileRepository"/> class.
        /// </summary>
        /// <param name="ctx">Database context object.</param>
        public ProfileRepository(DbContext ctx)
            : base(ctx)
        {
        }

        /// <inheritdoc/>
        public override Profile GetOne(int id)
        {
            return this.GetAll().SingleOrDefault(x => x.PlayerId == id);
        }

        /// <inheritdoc/>
        public void Update(Profile entity)
        {
            if (entity != null)
            {
                var profile = this.GetOne(entity.PlayerId);
                if (profile == null)
                {
                    throw new InvalidOperationException("Entity not found");
                }

                profile.TotalDeaths = entity.TotalDeaths;
                profile.TotalKills = entity.TotalKills;
                profile.CompletedRuns = entity.CompletedRuns;
                this.Ctx.SaveChanges();
            }
        }
    }
}
