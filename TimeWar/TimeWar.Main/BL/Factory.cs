// <copyright file="Factory.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.BL
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Data.Models;
    using TimeWar.Logic.Classes;
    using TimeWar.Repository.Classes;

    /// <summary>
    /// Factory class.
    /// </summary>
    internal class Factory : IDisposable
    {
        private TimeWarContext ctx;
        private SaveRepository saveRepository;
        private MapRecordRepository mapRepository;
        private ProfileRepository profileRepository;
        private ManagerLogic managerLogic;
        private ViewerLogic viewerLogic;

        /// <summary>
        /// Initializes a new instance of the <see cref="Factory"/> class.
        /// </summary>
        public Factory()
        {
            this.ctx = new TimeWarContext();
            this.saveRepository = new SaveRepository(this.ctx);
            this.mapRepository = new MapRecordRepository(this.ctx);
            this.profileRepository = new ProfileRepository(this.ctx);
            this.managerLogic = new ManagerLogic(this.profileRepository, this.saveRepository, this.mapRepository);
            this.viewerLogic = new ViewerLogic(this.profileRepository, this.saveRepository, this.mapRepository);
        }

        /// <summary>
        /// Gets viewer logic instance.
        /// </summary>
        public ViewerLogic ViewerLogic
        {
            get { return this.viewerLogic; }
        }

        /// <summary>
        /// Gets manager logic instance.
        /// </summary>
        public ManagerLogic ManagerLogic
        {
            get { return this.managerLogic; }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.ctx.Dispose();
        }
    }
}
