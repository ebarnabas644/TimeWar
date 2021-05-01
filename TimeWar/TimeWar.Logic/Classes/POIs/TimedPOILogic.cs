// <copyright file="TimedPOILogic.cs" company="Time War">
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
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Base class for timed POIs.
    /// </summary>
    public class TimedPOILogic : PointOfInterestLogic, ITimedEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TimedPOILogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="poi">Poi.</param>
        /// <param name="character">Character.</param>
        /// <param name="timeOfEffect">Time of effect.</param>
        /// <param name="timed">Timed.</param>
        public TimedPOILogic(GameModel model, PointOfInterest poi, CharacterLogic character, int timeOfEffect = 10000, bool timed = false)
            : base(model, poi, timed)
        {
            this.Character = character;
            this.Timer = (int)this.Character.EffectStopwatch.ElapsedMilliseconds + timeOfEffect;
        }

        /// <summary>
        /// Gets or sets character for the effect.
        /// </summary>
        public CharacterLogic Character { get; set; }

        /// <inheritdoc/>
        public bool CheckTimer()
        {
            if (this.Character.EffectStopwatch.ElapsedMilliseconds > this.Timer)
            {
                this.ResetStats();
                return true;
            }

            return false;
        }

        /// <inheritdoc/>
        public override void POIEvent()
        {
            this.TimedPoi = true;
            this.Character.EffectCounter++;
        }

        /// <inheritdoc/>
        public virtual void ResetStats()
        {
            this.Character.EffectCounter--;
        }
    }
}
