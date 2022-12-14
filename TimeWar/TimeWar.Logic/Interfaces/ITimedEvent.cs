// <copyright file="ITimedEvent.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Timed event interface.
    /// </summary>
    public interface ITimedEvent
    {
        /// <summary>
        /// Checks if a timed event can despawn.
        /// </summary>
        /// <returns>True if timed event is finished.</returns>
        bool CheckTimer();

        /// <summary>
        /// Resets the player's stats.
        /// </summary>
        void ResetStats();
    }
}
