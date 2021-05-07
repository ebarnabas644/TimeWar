// <copyright file="FinishLogic.cs" company="Time War">
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
    /// Finish logic.
    /// </summary>
    public class FinishLogic : PointOfInterestLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FinishLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="poi">Poi.</param>
        /// <param name="timed">Timed.</param>
        public FinishLogic(GameModel model, PointOfInterest poi, bool timed = false)
            : base(model, poi, timed)
        {
        }

        /// <inheritdoc/>
        public override void POIEvent()
        {
            this.Model.LevelFinished = true;
        }
    }
}
