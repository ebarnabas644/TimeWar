// <copyright file="IManagerLogicUI.cs" company="Time War">
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
    /// Manager logic ui interface.
    /// </summary>
    public interface IManagerLogicUI
    {
        /// <summary>
        /// Add profile entity to database.
        /// </summary>
        /// <param name="profileUIs">Profile entities.</param>
        /// <param name="newProfile">New profile.</param>
        void CreateProfile(IList<PlayerProfileUI> profileUIs, PlayerProfileUI newProfile);

        /// <summary>
        /// Add map entity to database.
        /// </summary>
        /// <param name="newMap">Map entity.</param>
        void CreateMap(MapRecordUI newMap);

        /// <summary>
        /// Add save entity to database.
        /// </summary>
        /// <param name="newSave">Save entity.</param>
        void CreateSave(SaveUI newSave);

        /// <summary>
        /// Modify already existing profile entity.
        /// </summary>
        /// <param name="newProfile">Profile entity.</param>
        void ModifyProfile(PlayerProfileUI newProfile);

        /// <summary>
        /// Modify already existing map entity.
        /// </summary>
        /// <param name="newMap">Map entity.</param>
        void ModifyMap(MapRecordUI newMap);

        /// <summary>
        /// Modify already existing save entity.
        /// </summary>
        /// <param name="newSave">Save entity.</param>
        void ModifySave(SaveUI newSave);

        /// <summary>
        /// Delete already existing profile entity.
        /// </summary>
        /// <param name="profileUIs">Profile ui entites.</param>
        /// <param name="profile">Profile entity.</param>
        void DeleteProfile(IList<PlayerProfileUI> profileUIs, PlayerProfileUI profile);

        /// <summary>
        /// Delete already existing map entity.
        /// </summary>
        /// <param name="mapUIs">Map ui entities.</param>
        /// <param name="map">Map entity.</param>
        void DeleteMap(IList<MapRecordUI> mapUIs, MapRecordUI map);

        /// <summary>
        /// Delete already existing save entity.
        /// </summary>
        /// <param name="saveUIs">Save ui entities.</param>
        /// <param name="save">Save entity.</param>
        void DeleteSave(IList<SaveUI> saveUIs, SaveUI save);
    }
}
