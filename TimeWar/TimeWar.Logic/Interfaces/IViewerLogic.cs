// <copyright file="IViewerLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Interfaces
{
    using System.Collections.Generic;
    using TimeWar.Data.Models;

    /// <summary>
    /// Database viewer logic interface.
    /// </summary>
    public interface IViewerLogic
    {
        /// <summary>
        /// Get profile entity based on id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Profile entity.</returns>
        PlayerProfile GetProfile(int id);

        /// <summary>
        /// Get map entity based on id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Map entity.</returns>
        MapRecord GetMap(int id);

        /// <summary>
        /// Get save entity based on id.
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Save entity.</returns>
        Save GetSave(int id);

        /// <summary>
        /// Get all profile entity.
        /// </summary>
        /// <returns>Profile entities.</returns>
        IList<PlayerProfile> GetProfiles();

        /// <summary>
        /// Get all map entity.
        /// </summary>
        /// <returns>Map entites.</returns>
        IList<MapRecord> GetMaps();

        /// <summary>
        /// Get all save entity.
        /// </summary>
        /// <returns>Save entites.</returns>
        IList<Save> GetSaves();
    }
}
