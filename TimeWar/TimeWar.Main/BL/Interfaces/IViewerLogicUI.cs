// <copyright file="IViewerLogicUI.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.BL.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Main.Data;

    /// <summary>
    /// Viewer logic ui interface.
    /// </summary>
    public interface IViewerLogicUI
    {
        /// <summary>
        /// Get all profile entity.
        /// </summary>
        /// <returns>Profile entities.</returns>
        IList<PlayerProfileUI> GetProfiles();

        /// <summary>
        /// Get all map entity.
        /// </summary>
        /// <returns>Map entites.</returns>
        IList<MapRecordUI> GetMaps();

        /// <summary>
        /// Get all save entity.
        /// </summary>
        /// <returns>Save entites.</returns>
        IList<SaveUI> GetSaves();

        /// <summary>
        /// Gets the currently selected profile.
        /// </summary>
        /// <returns>Player profile ui entity.</returns>
        public PlayerProfileUI GetSelectedProfile();

        /// <summary>
        /// Init all map from game folder.
        /// </summary>
        /// <returns>List of map names.</returns>
        public IList<MapFiles> LoadMaps();
    }
}
