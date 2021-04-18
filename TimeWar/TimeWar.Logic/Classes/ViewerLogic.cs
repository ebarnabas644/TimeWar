// <copyright file="ViewerLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes
{
    using System.Collections.Generic;
    using System.Linq;
    using TimeWar.Data.Models;
    using TimeWar.Logic.Interfaces;
    using TimeWar.Repository.Interfaces;

    /// <summary>
    /// Database viewer class.
    /// </summary>
    public class ViewerLogic : IViewerLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerLogic"/> class.
        /// </summary>
        /// <param name="profileRepo">Profile repository.</param>
        /// <param name="saveRepo">Save repository.</param>
        /// <param name="mapRepo">Map repository.</param>
        public ViewerLogic(IProfileRepository profileRepo, ISaveRepository saveRepo, IMapRecordRepository mapRepo)
        {
            this.ProfileRepo = profileRepo;
            this.SaveRepo = saveRepo;
            this.MapRepo = mapRepo;
        }

        private IProfileRepository ProfileRepo { get; }

        private ISaveRepository SaveRepo { get; }

        private IMapRecordRepository MapRepo { get; }

        /// <inheritdoc/>
        public MapRecord GetMap(int id)
        {
            return this.MapRepo.GetOne(id);
        }

        /// <inheritdoc/>
        public IList<MapRecord> GetMaps()
        {
            return this.MapRepo.GetAll().ToList();
        }

        /// <inheritdoc/>
        public PlayerProfile GetProfile(int id)
        {
            return this.ProfileRepo.GetOne(id);
        }

        /// <inheritdoc/>
        public IList<PlayerProfile> GetProfiles()
        {
            return this.ProfileRepo.GetAll().ToList();
        }

        /// <inheritdoc/>
        public Save GetSave(int id)
        {
            return this.SaveRepo.GetOne(id);
        }

        /// <inheritdoc/>
        public IList<Save> GetSaves()
        {
            return this.SaveRepo.GetAll().ToList();
        }
    }
}
