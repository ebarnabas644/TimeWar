// <copyright file="ManagerLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes
{
    using TimeWar.Data.Models;
    using TimeWar.Logic.Interfaces;
    using TimeWar.Repository.Interfaces;

    /// <summary>
    /// Database manager class.
    /// </summary>
    public class ManagerLogic : IManagerLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerLogic"/> class.
        /// </summary>
        /// <param name="profileRepo">Profile repository.</param>
        /// <param name="saveRepo">Save repository.</param>
        /// <param name="mapRepo">Map repository.</param>
        public ManagerLogic(IProfileRepository profileRepo, ISaveRepository saveRepo, IMapRecordRepository mapRepo)
        {
            this.ProfileRepo = profileRepo;
            this.SaveRepo = saveRepo;
            this.MapRepo = mapRepo;
        }

        private IProfileRepository ProfileRepo { get; }

        private ISaveRepository SaveRepo { get; }

        private IMapRecordRepository MapRepo { get; }

        /// <inheritdoc/>
        public void CreateMap(MapRecord newMap)
        {
            this.MapRepo.Create(newMap);
        }

        /// <inheritdoc/>
        public void CreateProfile(PlayerProfile newProfile)
        {
            this.ProfileRepo.Create(newProfile);
        }

        /// <inheritdoc/>
        public void CreateSave(Save newSave)
        {
            this.SaveRepo.Create(newSave);
        }

        /// <inheritdoc/>
        public void DeleteMap(MapRecord map)
        {
            this.MapRepo.Delete(map);
        }

        /// <inheritdoc/>
        public void DeleteProfile(PlayerProfile profile)
        {
            this.ProfileRepo.Delete(profile);
        }

        /// <inheritdoc/>
        public void DeleteSave(Save save)
        {
            this.SaveRepo.Delete(save);
        }

        /// <inheritdoc/>
        public void ModifyMap(MapRecord newMap)
        {
            this.MapRepo.Update(newMap);
        }

        /// <inheritdoc/>
        public void ModifyProfile(PlayerProfile newProfile)
        {
            this.ProfileRepo.Update(newProfile);
        }

        /// <inheritdoc/>
        public void ModifySave(Save newSave)
        {
            this.SaveRepo.Update(newSave);
        }
    }
}
