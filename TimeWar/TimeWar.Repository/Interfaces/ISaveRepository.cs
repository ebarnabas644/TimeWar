// <copyright file="ISaveRepository.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Repository.Interfaces
{
    using TimeWar.Data.Models;

    /// <summary>
    /// Extra operations for Save class entities.
    /// </summary>
    public interface ISaveRepository : IMainRepository<Save>
    {
        /// <summary>
        /// Update entity content.
        /// </summary>
        /// <param name="entity">Save entity object.</param>
        void Update(Save entity);
    }
}
