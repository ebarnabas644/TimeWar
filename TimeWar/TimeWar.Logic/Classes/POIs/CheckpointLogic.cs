// <copyright file="CheckpointLogic.cs" company="Time War">
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
    /// Checkpoint logic.
    /// </summary>
    public class CheckpointLogic : PointOfInterestLogic
    {
        private CharacterLogic character;

        /// <summary>
        /// Initializes a new instance of the <see cref="CheckpointLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="poi">Poi.</param>
        /// <param name="character">Charcter logic.</param>
        /// <param name="timed">Timed.</param>
        public CheckpointLogic(GameModel model, PointOfInterest poi, CharacterLogic character, bool timed = false)
            : base(model, poi, timed)
        {
            this.character = character;
        }

        /// <inheritdoc/>
        public override void POIEvent()
        {
            this.Model.Hero.Checkpoint = new System.Drawing.Point(this.Model.CurrentWorld.ConvertTileToPixel(this.Poi.Position.X), this.Model.CurrentWorld.ConvertTileToPixel(this.Poi.Position.Y));
        }
    }
}
