// <copyright file="IManagerLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Interfaces
{
    using TimeWar.Data.Models;

    /// <summary>
    /// Database manager logic interface.
    /// </summary>
    public interface IManagerLogic
    {
        /// <summary>
        /// Add profile entity to database.
        /// </summary>
        /// <param name="newProfile">Profile entity.</param>
        void CreateProfile(PlayerProfile newProfile);

        /// <summary>
        /// Add map entity to database.
        /// </summary>
        /// <param name="newMap">Map entity.</param>
        void CreateMap(MapRecord newMap);

        /// <summary>
        /// Add save entity to database.
        /// </summary>
        /// <param name="newSave">Save entity.</param>
        void CreateSave(Save newSave);

        /// <summary>
        /// Modify already existing profile entity.
        /// </summary>
        /// <param name="newProfile">Profile entity.</param>
        void ModifyProfile(PlayerProfile newProfile);

        /// <summary>
        /// Modify already existing map entity.
        /// </summary>
        /// <param name="newMap">Map entity.</param>
        void ModifyMap(MapRecord newMap);

        /// <summary>
        /// Modify already existing save entity.
        /// </summary>
        /// <param name="newSave">Save entity.</param>
        void ModifySave(Save newSave);

        /// <summary>
        /// Delete already existing profile entity.
        /// </summary>
        /// <param name="profile">Profile entity.</param>
        void DeleteProfile(PlayerProfile profile);

        /// <summary>
        /// Delete already existing map entity.
        /// </summary>
        /// <param name="map">Map entity.</param>
        void DeleteMap(MapRecord map);

        /// <summary>
        /// Delete already existing save entity.
        /// </summary>
        /// <param name="save">Save entity.</param>
        void DeleteSave(Save save);
    }
}
