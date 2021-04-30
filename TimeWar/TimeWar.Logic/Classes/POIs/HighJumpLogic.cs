﻿// <copyright file="HighJumpLogic.cs" company="Time War">
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
    public class HighJumpLogic : PointOfInterestLogic
    {
        private int newJumpHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="HighJumpLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="poi">Poi.</param>
        /// <param name="newJumpHeight">New jump height.</param>
        /// <param name="timed">Timed.</param>
        public HighJumpLogic(GameModel model, PointOfInterest poi, int newJumpHeight = 25, bool timed = false)
            : base(model, poi, timed)
        {
            this.newJumpHeight = newJumpHeight;
        }

        /// <inheritdoc/>
        public override void POIEvent()
        {
            this.TimedPoi = true;
            throw new NotImplementedException();
        }
    }
}
