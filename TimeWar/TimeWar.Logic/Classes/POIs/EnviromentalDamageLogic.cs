// <copyright file="EnviromentalDamageLogic.cs" company="Time War">
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
    /// Enviromental damage.
    /// </summary>
    public class EnviromentalDamageLogic : PointOfInterestLogic
    {
        private int damage;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnviromentalDamageLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="poi">POI.</param>
        /// <param name="damage">damage of the poi.</param>
        /// <param name="timed">Timed.</param>
        public EnviromentalDamageLogic(GameModel model, PointOfInterest poi, int damage = 1, bool timed = false)
            : base(model, poi, timed)
        {
            this.damage = damage;
        }

        /// <inheritdoc/>
        public override void POIEvent()
        {
            if (!this.Model.Hero.IsInvincible)
            {
                if (this.Model.Hero.CurrentShield - this.damage <= 0)
                {
                    this.Model.Hero.CurrentHealth -= this.damage;
                }
                else
                {
                    this.Model.Hero.CurrentShield -= this.damage;
                }
            }

            this.IsPlayerContacted = false;
            this.Model.Hero.ShieldRegenTimer.Reset();
        }
    }
}
