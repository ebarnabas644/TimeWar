// <copyright file="IProfileRepository.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Repository.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Data.Models;

    /// <summary>
    /// Extra operations for Profile class entities.
    /// </summary>
    public interface IProfileRepository : IMainRepository<Profile>
    {
        /// <summary>
        /// Update entity content.
        /// </summary>
        /// <param name="entity">Profile entity object.</param>
        void Update(Profile entity);
    }
}
