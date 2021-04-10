﻿// <copyright file="GameWorld.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Game world details, settings.
    /// </summary>
    public class GameWorld
    {
        private bool[][] ground;
        private Dictionary<string, Point> pointOfInterests;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWorld"/> class.
        /// </summary>
        /// <param name="height">Height in tile.</param>
        /// <param name="width">Width in tile.</param>
        /// <param name="tileSize">Game tile size.</param>
        /// <param name="magnify">Zoom extent of the game world(default value = 3).</param>
        public GameWorld(int height, int width, int tileSize, int magnify = 5)
        {
            this.ground = new bool[height][];
            for (int i = 0; i < this.ground.Length; i++)
            {
                this.ground[i] = new bool[width];
            }

            this.pointOfInterests = new Dictionary<string, Point>();
            this.Magnify = magnify;
            this.TileSize = tileSize;
            this.GameWidth = this.TileSize * width * this.Magnify;
            this.GameHeight = this.TileSize * height * this.Magnify;
        }

        /// <summary>
        /// Gets or sets the name of the game world.
        /// </summary>
        public string WorldName { get; set; }

        /// <summary>
        /// Gets or sets the game world tile size(pixel value).
        /// </summary>
        public int TileSize { get; set; }

        /// <summary>
        /// Gets the game world width in tile value.
        /// </summary>
        public double GetTileWidth
        {
            get { return this.GameWidth / this.Magnify / this.TileSize; }
        }

        /// <summary>
        /// Gets the game world height in tile value.
        /// </summary>
        public double GetTileHeight
        {
            get { return this.GameHeight / this.Magnify / this.TileSize; }
        }

        /// <summary>
        /// Gets or sets the game world width(pixel value).
        /// </summary>
        public double GameWidth { get; set; }

        /// <summary>
        /// Gets or sets the game world height(pixel value).
        /// </summary>
        public double GameHeight { get; set; }

        /// <summary>
        /// Gets or sets the zoom extent of the game world.
        /// </summary>
        public int Magnify { get; set; }

        /// <summary>
        /// Add new point of interest.
        /// </summary>
        /// <param name="name">Name of the point.</param>
        /// <param name="position">Position.</param>
        public void AddPointOfInterest(string name, Point position)
        {
            if (!this.pointOfInterests.ContainsKey(name))
            {
                this.pointOfInterests.Add(name, position);
            }
        }

        /// <summary>
        /// Find existing point of interest by key.
        /// </summary>
        /// <param name="name">Key of the point.</param>
        /// <returns>Position of the point.</returns>
        public Point SearchPointOfInterest(string name)
        {
            if (this.pointOfInterests.ContainsKey(name))
            {
                return this.pointOfInterests[name];
            }

            return new Point(-1, -1);
        }

        /// <summary>
        /// Add new ground tile.
        /// </summary>
        /// <param name="position">Position of the ground(tile pos).</param>
        public void AddGround(Point position)
        {
            try
            {
                this.ground[position.Y][position.X] = true;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.WriteLine("GameWorld.AddGround: Bad value: (X: " + position.X + " Y: " + position.Y + ")");
            }
        }

        /// <summary>
        /// Remove ground tile.
        /// </summary>
        /// <param name="position">Position of the ground(tile pos).</param>
        public void RemoveGround(Point position)
        {
            try
            {
                this.ground[position.Y][position.X] = false;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.WriteLine("GameWorld.RemoveGround: Bad value: (X: " + position.X + " Y: " + position.Y + ")");
            }
        }

        /// <summary>
        /// Search for ground tiles.
        /// </summary>
        /// <param name="position">Position.</param>
        /// <returns>Bool based on the ground value.</returns>
        public bool SearchGround(Point position)
        {
            try
            {
                return this.ground[position.Y][position.X];
            }
            catch (IndexOutOfRangeException)
            {
                Debug.WriteLine("GameWorld.SearchGround: Bad value: (X: " + position.X + " Y: " + position.Y + ")");
            }

            return false;
        }

        /// <summary>
        /// Convert tile value to pixel value.
        /// </summary>
        /// <param name="tilePos">Tile position.</param>
        /// <returns>Pixel position.</returns>
        public int ConvertTileToPixel(int tilePos)
        {
            return tilePos * this.Magnify * this.TileSize;
        }

        /// <summary>
        /// Convert pixel value to tile value.
        /// </summary>
        /// <param name="pixelPos">Tile position.</param>
        /// <returns>Pixel position.</returns>
        public int ConvertPixelToTile(int pixelPos)
        {
            return pixelPos / this.Magnify / this.TileSize;
        }
    }
}
