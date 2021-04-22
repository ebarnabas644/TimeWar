// <copyright file="GameWorld.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Game world details, settings.
    /// </summary>
    public class GameWorld
    {
        private bool[][] ground;
        private int[][] decorations;
        private Dictionary<string, Point> pointOfInterests;
        private List<Bullet> bullets;

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
            this.decorations = new int[height][];
            for (int i = 0; i < this.ground.Length; i++)
            {
                this.ground[i] = new bool[width];
                this.decorations[i] = new int[width];
            }

            this.pointOfInterests = new Dictionary<string, Point>();
            this.bullets = new List<Bullet>();
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
        /// Gets number of bullets.
        /// </summary>
        public int BulletCount
        {
            get { return this.bullets.Count; }
        }

        /// <summary>
        /// Gets bullets.
        /// </summary>
        /// <returns>Return currently spawned bullets collection.</returns>
        public IReadOnlyList<Bullet> GetBullets
        {
            get
            {
                IReadOnlyList<Bullet> output = this.bullets;
                return output;
            }
        }

        /// <summary>
        /// Get bullet from bullet collection.
        /// </summary>
        /// <param name="idx">Index.</param>
        /// <returns>Bullet entity.</returns>
        public Bullet GetBullet(int idx)
        {
            if (idx < this.bullets.Count)
            {
                return this.bullets[idx];
            }

            return null;
        }

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

        /// <summary>
        /// Add decoration object to the map.
        /// </summary>
        /// <param name="position">Position of the object(tile pos).</param>
        /// <param name="id">Object id.</param>
        public void AddDecoration(Point position, int id)
        {
            try
            {
                this.decorations[position.Y][position.X] = id;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.WriteLine("GameWorld.AddDecoration: Bad value: (X: " + position.X + " Y: " + position.Y + ")");
            }
        }

        /// <summary>
        /// Remove decoration from the map.
        /// </summary>
        /// <param name="position">Position of the object(tile pos).</param>
        public void RemoveDecoration(Point position)
        {
            try
            {
                this.decorations[position.Y][position.X] = 0;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.WriteLine("GameWorld.RemoveDecoration: Bad value: (X: " + position.X + " Y: " + position.Y + ")");
            }
        }

        /// <summary>
        /// Search for decoration object.
        /// </summary>
        /// <param name="position">Position of the object(tile pos).</param>
        /// <returns>Object id.</returns>
        public int SearchDecoration(Point position)
        {
            try
            {
                return this.decorations[position.Y][position.X];
            }
            catch (IndexOutOfRangeException)
            {
                Debug.WriteLine("GameWorld.SearchDecoration: Bad value: (X: " + position.X + " Y: " + position.Y + ")");
            }

            return 0;
        }

        /// <summary>
        /// Add new bullet.
        /// </summary>
        /// <param name="bullet">Bullet entity.</param>
        public void AddBullet(Bullet bullet)
        {
            this.bullets.Add(bullet);
        }

        /// <summary>
        /// Remove bullet.
        /// </summary>
        /// <param name="bullet">Bullet entity.</param>
        public void RemoveBullet(Bullet bullet)
        {
            if (this.bullets.Contains(bullet))
            {
                this.bullets.Remove(bullet);
            }
        }
    }
}
