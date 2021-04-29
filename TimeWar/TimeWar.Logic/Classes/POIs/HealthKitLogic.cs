// <copyright file="HealthKitLogic.cs" company="Time War">
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
    /// Health kit logic.
    /// </summary>
    public class HealthKitLogic : PointOfInterestLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthKitLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="poi">Poi.</param>
        /// <param name="restoredHealth">Number of restored health.</param>
        /// <param name="timed">Timed.</param>
        public HealthKitLogic(GameModel model, PointOfInterest poi, int restoredHealth = 25, bool timed = false)
            : base(model, poi, timed)
        {
            this.NumOfRestoredHealth = restoredHealth;
        }

        /// <summary>
        /// Gets or sets the number of unlocked weapons.
        /// </summary>
        public int NumOfRestoredHealth { get; set; }

        /// <inheritdoc/>
        public override void POIEvent()
        {
            this.Model.Hero.Health += this.NumOfRestoredHealth;
        }
    }
}
