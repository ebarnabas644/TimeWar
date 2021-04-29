// <copyright file="PointOfInterestLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.POIs
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Model;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Base class for POIs.
    /// </summary>
    public abstract class PointOfInterestLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PointOfInterestLogic"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="poi">Poi.</param>
        /// <param name="timed">If a poi is timed or not.</param>
        protected PointOfInterestLogic(GameModel model, PointOfInterest poi, bool timed = false)
        {
            this.TimedPoi = timed;
            this.Model = model;
            this.Poi = poi;
            this.IsPlayerContacted = false;
            this.Timer = 0;
        }

        /// <summary>
        /// Gets or sets game Model.
        /// </summary>
        public GameModel Model { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether a poi is timed or not.
        /// </summary>
        public bool TimedPoi { get; set; }

        /// <summary>
        /// Gets or sets the max time of a poi.
        /// </summary>
        public int Timer { get; set; }

        /// <summary>
        /// Gets or sets point of interest.
        /// </summary>
        public PointOfInterest Poi { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether player is contacted or not.
        /// </summary>
        public bool IsPlayerContacted { get; set; }

        /// <summary>
        /// One tick.
        /// </summary>
        public void OneTick()
        {
            this.PlayerContacted();
            if (this.IsPlayerContacted)
            {
                this.POIEvent();
            }
        }

        /// <summary>
        /// Action that happens when a player contacts a POI.
        /// </summary>
        public abstract void POIEvent();

        private void PlayerContacted()
        {
            Rectangle player = new Rectangle(this.Model.Hero.Position, new Size(this.Model.Hero.Width, this.Model.Hero.Height));
            Rectangle poi = new Rectangle(this.Poi.Position, new Size(this.Poi.Width, this.Poi.Height));

            if (player.IntersectsWith(poi))
            {
                this.IsPlayerContacted = true;
            }
        }
    }
}
