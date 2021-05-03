// <copyright file="IProfileRepository.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Repository.Interfaces
{
    using TimeWar.Data.Models;

    /// <summary>
    /// Extra operations for Profile class entities.
    /// </summary>
    public interface IProfileRepository : IMainRepository<PlayerProfile>
    {
        /// <summary>
        /// Update entity content.
        /// </summary>
        /// <param name="entity">Profile entity object.</param>
        void Update(PlayerProfile entity);
    }
}
