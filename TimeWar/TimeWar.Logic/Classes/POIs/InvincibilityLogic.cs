// <copyright file="InvincibilityLogic.cs" company="Time War">
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
    /// Invincibility logic.
    /// </summary>
    public class InvincibilityLogic : TimedPOILogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvincibilityLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="poi">Poi.</param>
        /// <param name="character">Character.</param>
        /// <param name="timeOfEffect">Time of effect.</param>
        /// <param name="timed">Timed.</param>
        public InvincibilityLogic(GameModel model, PointOfInterest poi, CharacterLogic character, int timeOfEffect = 10000, bool timed = false)
            : base(model, poi, character, timeOfEffect, timed)
        {
        }

        /// <inheritdoc/>
        public override void POIEvent()
        {
            this.Character.Character.IsInvincible = true;
            base.POIEvent();
        }

        /// <inheritdoc/>
        public override void ResetStats()
        {
            this.Character.Character.IsInvincible = false;
            base.ResetStats();
        }
    }
}
