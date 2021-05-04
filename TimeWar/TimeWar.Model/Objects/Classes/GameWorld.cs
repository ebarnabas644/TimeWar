// <copyright file="GameWorld.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Model.Objects
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using TimeWar.Model.Objects.Classes;

    /// <summary>
    /// Game world details, settings.
    /// </summary>
    public class GameWorld
    {
        private bool[][] ground;
        private int[][] decorations;
        private List<Enemy> enemies;
        private List<PointOfInterest> pointOfInterests;
        private List<Bullet> bullets;
        private List<Enemy> checkPointEnemies;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameWorld"/> class.
        /// </summary>
        /// <param name="height">Height in tile.</param>
        /// <param name="width">Width in tile.</param>
        /// <param name="tileSize">Game tile size.</param>
        /// <param name="magnify">Zoom extent of the game world(default value = 3).</param>
        public GameWorld(int height, int width, int tileSize, int magnify = 4)
        {
            this.ground = new bool[height][];
            this.decorations = new int[height][];
            this.enemies = new List<Enemy>();
            for (int i = 0; i < this.ground.Length; i++)
            {
                this.ground[i] = new bool[width];
                this.decorations[i] = new int[width];
            }

            this.pointOfInterests = new List<PointOfInterest>();
            this.bullets = new List<Bullet>();
            this.Magnify = magnify;
            this.TileSize = tileSize;
            this.GameWidth = this.TileSize * width * this.Magnify;
            this.GameHeight = this.TileSize * height * this.Magnify;
        }

        /// <summary>
        /// Gets or sets startpoint.
        /// </summary>
        public Point StartPoint { get; set; }

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
        /// Gets or sets a value indicating whether enemies are loaded.
        /// </summary>
        public bool EnemiesLoaded { get; set; }

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
        /// Gets the number of the enemies.
        /// </summary>
        public int EnemyCount
        {
            get { return this.enemies.Count; }
        }

        /// <summary>
        /// Gets bullets.
        /// </summary>
        /// <returns>Return currently spawned bullets collection.</returns>
        public IReadOnlyList<Bullet> GetBullets
        {
            get
            {
                IReadOnlyList<Bullet> output;
                lock (this.bullets)
                {
                    output = this.bullets.ToList();
                }

                return output;
            }
        }

        /// <summary>
        /// Gets enemies.
        /// </summary>
        public IReadOnlyList<Enemy> GetEnemies
        {
            get
            {
                IReadOnlyList<Enemy> output;
                lock (this.enemies)
                {
                    output = this.enemies.ToList();
                }

                return output;
            }
        }

        /// <summary>
        /// Gets a list of pois.
        /// </summary>
        public IEnumerable<PointOfInterest> GetPois
        {
            get
            {
                IReadOnlyList<PointOfInterest> output;
                lock (this.pointOfInterests)
                {
                    output = this.pointOfInterests.ToList();
                }

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
        /// Add new poi.
        /// </summary>
        /// <param name="poi">POI entity.</param>
        public void AddPOI(PointOfInterest poi)
        {
            this.pointOfInterests.Add(poi);
        }

        /// <summary>
        /// Remove poi from the collection.
        /// </summary>
        /// <param name="poi">Point of interest.</param>
        public void RemovePOI(PointOfInterest poi)
        {
            this.pointOfInterests.Remove(poi);
        }

        /// <summary>
        /// Get poi entity.
        /// </summary>
        /// <param name="idx">Index.</param>
        /// <returns>Poi entity.</returns>
        public PointOfInterest GetPoi(int idx)
        {
            if (idx < this.pointOfInterests.Count)
            {
                return this.pointOfInterests[idx];
            }

            return null;
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
            this.bullets.Remove(bullet);
        }

        /// <summary>
        /// Add new enemy.
        /// </summary>
        /// <param name="enemy">Character entity.</param>
        public void AddEnemy(Enemy enemy)
        {
            this.enemies.Add(enemy);
        }

        /// <summary>
        /// Remove enemy from the collection.
        /// </summary>
        /// <param name="enemy">Character entity.</param>
        public void RemoveEnemy(Enemy enemy)
        {
            this.enemies.Remove(enemy);
        }

        /// <summary>
        /// Get enemy entity.
        /// </summary>
        /// <param name="idx">Index.</param>
        /// <returns>Character entity.</returns>
        public Enemy GetEnemy(int idx)
        {
            if (idx < this.enemies.Count)
            {
                return this.enemies[idx];
            }

            return null;
        }

        /// <summary>
        /// Save enmies.
        /// </summary>
        public void SaveEnemies()
        {
            Debug.WriteLine("Enemies saved");
            this.checkPointEnemies = DeepCopy(this.enemies);
        }

        /// <summary>
        /// Gets returns checkpoint saved enemies.
        /// </summary>
        public void LoadEnemies()
        {
            Debug.WriteLine("Enemies loaded");
            this.EnemiesLoaded = true;
            this.enemies = DeepCopy(this.checkPointEnemies);
        }

        private static List<Enemy> DeepCopy(List<Enemy> enemies)
        {
            List<Enemy> enemyList = new List<Enemy>();
            foreach (Enemy enemy in enemies)
            {
                Enemy e = new Enemy(enemy.Position, enemy.CurrentHealth, enemy.Height, enemy.Width, enemy.Type, enemy.SpriteFile);
                enemyList.Add(e);
            }

            return enemyList;
        }
    }
}
