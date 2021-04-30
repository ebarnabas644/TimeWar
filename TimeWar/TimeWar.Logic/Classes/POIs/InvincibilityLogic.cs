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
    public class InvincibilityLogic : PointOfInterestLogic
    {
        private int invisibilityTime;

        /// <summary>
        /// Initializes a new instance of the <see cref="InvincibilityLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="poi">Poi.</param>
        /// <param name="invisibilityTime">Time of invisibility.</param>
        /// <param name="timed">Timed.</param>
        public InvincibilityLogic(GameModel model, PointOfInterest poi, int invisibilityTime = 10000, bool timed = false)
            : base(model, poi, timed)
        {
            this.invisibilityTime = invisibilityTime;
        }

        /// <inheritdoc/>
        public override void POIEvent()
        {
            this.TimedPoi = true;
            throw new NotImplementedException();
        }
    }
}
