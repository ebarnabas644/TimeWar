// <copyright file="UnlockWeaponLogic.cs" company="Time War">
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
    /// Unlocks a weapon.
    /// </summary>
    public class UnlockWeaponLogic : PointOfInterestLogic
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnlockWeaponLogic"/> class.
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="poi">Poi.</param>
        /// <param name="numOfUnlocks">Number of unlocked weapons.</param>
        /// <param name="timed">Timed.</param>
        public UnlockWeaponLogic(GameModel model, PointOfInterest poi, int numOfUnlocks = 1, bool timed = false)
            : base(model, poi, timed)
        {
            this.NumOfUnlocks = numOfUnlocks;
        }

        /// <summary>
        /// Gets or sets the number of unlocked weapons.
        /// </summary>
        public int NumOfUnlocks { get; set; }

        /// <inheritdoc/>
        public override void POIEvent()
        {
            if (this.Model.Hero.NumOfWeaponUnlocked + this.NumOfUnlocks < Enum.GetNames(typeof(BulletType)).Length - 1)
            {
                this.Model.Hero.NumOfWeaponUnlocked += this.NumOfUnlocks;
            }
            else
            {
                this.Model.Hero.NumOfWeaponUnlocked = Enum.GetNames(typeof(BulletType)).Length - 1;
            }
        }
    }
}
