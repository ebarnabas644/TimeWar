// <copyright file="RapidFireLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.POIs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Logic.Interfaces;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Rapid fire logic.
    /// </summary>
    public class RapidFireLogic : TimedPOILogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RapidFireLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="poi">Poi.</param>
        /// <param name="character">Character.</param>
        /// <param name="timeOfEffect">Time of effect.</param>
        /// <param name="timed">Timed.</param>
        public RapidFireLogic(GameModel model, PointOfInterest poi, CharacterLogic character, int timeOfEffect = 10000, bool timed = false)
            : base(model, poi, character, timeOfEffect, timed)
        {
            this.DefaultAttackTime = this.Character.AttackTime;
        }

        /// <summary>
        /// Gets or sets the original attack time of the player.
        /// </summary>
        public int DefaultAttackTime { get; set; }

        /// <inheritdoc/>
        public override void POIEvent()
        {
            base.POIEvent();
            this.Character.AttackTime = 1;
        }

        /// <inheritdoc/>
        public override void ResetStats()
        {
            base.ResetStats();
            this.Character.AttackTime = this.DefaultAttackTime;
        }
    }
}