// <copyright file="HighJumpLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.POIs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Model;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// High jump logic.
    /// </summary>
    public class HighJumpLogic : TimedPOILogic
    {
        private int defaultJumpHeight;
        private int newJumpHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighJumpLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="poi">Poi.</param>
        /// <param name="character">Character.</param>
        /// <param name="timeOfEffect">Time of effect.</param>
        /// <param name="newMaxJumpHeight">New jump height.</param>
        /// <param name="timed">Timed.</param>
        public HighJumpLogic(GameModel model, PointOfInterest poi, CharacterLogic character, int timeOfEffect = 10000, int newMaxJumpHeight = 30, bool timed = false)
            : base(model, poi, character, timeOfEffect, timed)
        {
            this.defaultJumpHeight = this.Character.MaxJumpHeight;
            this.newJumpHeight = newMaxJumpHeight;
        }

        /// <inheritdoc/>
        public override void POIEvent()
        {
            base.POIEvent();
            this.Character.MaxJumpHeight = this.newJumpHeight;
        }

        /// <inheritdoc/>
        public override void ResetStats()
        {
            base.ResetStats();
            this.Character.MaxJumpHeight = this.defaultJumpHeight;
        }
    }
}
