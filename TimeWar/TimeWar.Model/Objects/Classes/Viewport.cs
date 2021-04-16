// <copyright file="Viewport.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Camera viewport class.
    /// </summary>
    public class Viewport
    {
        private int gameWidth;
        private int gameHeight;

        /// <summary>
        /// Initializes a new instance of the <see cref="Viewport"/> class.
        /// </summary>
        /// <param name="windowWidth">Width of the window.</param>
        /// <param name="windowHeight">Height of the window.</param>
        /// <param name="gameWidth">Current game world width.</param>
        /// <param name="gameHeight">Current game world height.</param>
        /// <param name="followed">Followed character.</param>
        public Viewport(int windowWidth, int windowHeight, int gameWidth, int gameHeight, Character followed)
        {
            this.WindowWidth = windowWidth;
            this.WindowHeight = windowHeight;
            this.Followed = followed;
            this.gameWidth = gameWidth;
            this.gameHeight = gameHeight;
        }

        /// <summary>
        /// Gets or sets the currently followed character by viewport.
        /// </summary>
        public Character Followed { get; set; }

        /// <summary>
        /// Gets or sets the current window width.
        /// </summary>
        public int WindowWidth { get; set; }

        /// <summary>
        /// Gets or sets the current window height.
        /// </summary>
        public int WindowHeight { get; set; }

        /// <summary>
        /// Gets the calculated x position of the viewport.
        /// </summary>
        public int GetViewportX
        {
            get
            {
                if (this.Followed.Position.X - (this.WindowWidth / 2) < 0)
                {
                    return 0;
                }
                else if (this.Followed.Position.X + (this.WindowWidth / 2) > this.gameWidth)
                {
                    return -this.gameWidth + this.WindowWidth;
                }

                return -this.Followed.Position.X + (this.WindowWidth / 2);
            }
        }

        /// <summary>
        /// Gets the calculated y position of the viewport.
        /// </summary>
        public int GetViewportY
        {
            get
            {
                if (this.Followed.Position.Y - (this.WindowHeight / 2) < 0)
                {
                    return 0;
                }
                else if (this.Followed.Position.Y + (this.WindowHeight / 2) >= this.gameHeight)
                {
                    return 0 - this.gameHeight + this.WindowHeight + 3;
                }

                return -this.Followed.Position.Y + (this.WindowHeight / 2);
            }
        }

        /// <summary>
        /// Gets the followed character X position relative to the viewport.
        /// </summary>
        public int GetRelativeCharacterPosX
        {
            get
            {
                if (this.Followed.Position.X < this.WindowWidth / 2)
                {
                    return this.Followed.Position.X;
                }
                else if (this.Followed.Position.X > this.gameWidth - (this.WindowWidth / 2))
                {
                    return this.Followed.Position.X + this.GetViewportX;
                }
                else
                {
                    return this.WindowWidth / 2;
                }
            }
        }

        /// <summary>
        /// Gets the followed character Y position relative to the viewport.
        /// </summary>
        public int GetRelativeCharacterPosY
        {
            get
            {
                if (this.Followed.Position.Y < this.WindowHeight / 2)
                {
                    return this.Followed.Position.Y;
                }
                else if (this.Followed.Position.Y > this.gameHeight - (this.WindowHeight / 2))
                {
                    return this.Followed.Position.Y + this.GetViewportY;
                }
                else
                {
                    return this.WindowHeight / 2;
                }
            }
        }

        /// <summary>
        /// Get relative x position from the viewport.
        /// </summary>
        /// <param name="xPos">Object x pos.</param>
        /// <returns>Relative x pos.</returns>
        public int GetRelativeObjectPosX(int xPos)
        {
            return xPos + this.GetViewportX;
        }

        /// <summary>
        /// Get relative y position from the viewport.
        /// </summary>
        /// <param name="yPos">Object y pos.</param>
        /// <returns>Relative y pos.</returns>
        public int GetRelativeObjectPosY(int yPos)
        {
            return yPos + this.GetViewportY;
        }
    }
}
