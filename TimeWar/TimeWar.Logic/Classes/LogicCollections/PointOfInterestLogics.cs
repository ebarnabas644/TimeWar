// <copyright file="PointOfInterestLogics.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Classes.LogicCollections
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using TimeWar.Logic.Classes.POIs;
    using TimeWar.Model;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Collection of POIs.
    /// </summary>
    public class PointOfInterestLogics
    {
        private GameModel model;
        private List<PointOfInterestLogic> pois;
        private CharacterLogic character;

        /// <summary>
        /// Initializes a new instance of the <see cref="PointOfInterestLogics"/> class.
        /// </summary>
        /// <param name="model">Game model.</param>
        /// <param name="character">Character.</param>
        public PointOfInterestLogics(GameModel model, CharacterLogic character)
        {
            this.model = model;
            this.character = character;
            this.pois = new List<PointOfInterestLogic>();
        }

        /// <summary>
        /// Gets pois.
        /// </summary>
        public void GetPOIs()
        {
            throw new NotImplementedException();
        }
    }
}
