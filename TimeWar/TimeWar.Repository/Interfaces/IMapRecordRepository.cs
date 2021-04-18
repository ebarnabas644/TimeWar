// <copyright file="IMapRecordRepository.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Repository.Interfaces
{
    using TimeWar.Data.Models;

    /// <summary>
    /// Extra operations for Map record class entities.
    /// </summary>
    public interface IMapRecordRepository : IMainRepository<MapRecord>
    {
        /// <summary>
        /// Update entity content.
        /// </summary>
        /// <param name="entity">Map record entity object.</param>
        void Update(MapRecord entity);
    }
}
