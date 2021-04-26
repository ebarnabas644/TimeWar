// <copyright file="IMapRepository.cs" company="Time War">
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
    /// Extra operations for Map class entities.
    /// </summary>
    public interface IMapRepository : IMainRepository<Map>
    {
        /// <summary>
        /// Update entity content.
        /// </summary>
        /// <param name="entity">Map entity object.</param>
        void Update(Map entity);
    }
}
