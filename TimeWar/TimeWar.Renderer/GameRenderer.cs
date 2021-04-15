// <copyright file="GameRenderer.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Game rendering class.
    /// </summary>
    public class GameRenderer
    {
        private GameModel model;
        private Stopwatch spriteTimer;
        private Drawing backgroundCache;
        private Drawing walls;
        private Drawing titleCache;
        private Drawing playerCache;
        private Dictionary<string, Brush> staticBrushes;
        private Dictionary<IGameObject, ImageBrush[][]> spriteBrushes;
        private Dictionary<string, IGameObject> gameObjects;
        private bool menuMode;
        private int spriteFps;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRenderer"/> class.
        /// </summary>
        /// <param name="model">Game model entity.</param>
        /// <param name="menuMode">Game menu mode.</param>
        public GameRenderer(GameModel model, bool menuMode)
        {
            this.model = model;
            this.spriteTimer = new Stopwatch();

            this.menuMode = menuMode;
            this.spriteBrushes = new Dictionary<IGameObject, ImageBrush[][]>();
            this.gameObjects = new Dictionary<string, IGameObject>();
            this.staticBrushes = new Dictionary<string, Brush>();
            this.spriteTimer.Start();
            this.WindowChanged = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the window changed.
        /// </summary>
        public bool WindowChanged { get; set; }

        /// <summary>
        /// Build drawed game world.
        /// </summary>
        /// <returns>Drawing with all entities for render.</returns>
        public Drawing BuildDrawing()
        {
            this.spriteFps = (int)this.spriteTimer.Elapsed.TotalMilliseconds / 100;
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(this.GetBackground());

            // dg.Children.Add(this.GetCollision());
            if (!this.menuMode)
            {
                dg.Children.Add(this.GetPlayer());
            }
            else
            {
                dg.Children.Add(this.GetTitle());
            }

            this.WindowChanged = false;
            return dg;
        }

        private void Reset()
        {
            this.backgroundCache = null;
            this.walls = null;
            this.staticBrushes.Clear();
        }

        private Brush GetBrush(string fname)
        {
            if (!this.staticBrushes.ContainsKey(fname))
            {
                // ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(uri)));
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri(string.Format(System.Globalization.CultureInfo.CurrentCulture, "Leveldata/{0}.png", fname), UriKind.Relative);
                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                this.staticBrushes.Add(fname, ib);
            }

            return this.staticBrushes[fname];
        }

        private Brush GetSpriteBrush(IGameObject obj)
        {
            if (!this.spriteBrushes.ContainsKey(obj))
            {
                this.spriteBrushes.Add(obj, Sprite.CreateSprite(obj.Height, obj.Width, obj.SpriteFile));
            }

            return this.spriteBrushes[obj][0][obj.CurrentSprite];
        }

        private Drawing GetBackground()
        {
            Geometry g = new RectangleGeometry(new Rect(this.model.Camera.GetViewportX, this.model.Camera.GetViewportY, this.model.CurrentWorld.GameWidth, this.model.CurrentWorld.GameHeight));
            if (this.menuMode)
            {
                int prevX = this.model.Hero.Position.X;
                int prevY = this.model.Hero.Position.Y;
                int prevCamX = this.model.Camera.GetViewportX;
                this.model.Hero.Position = new System.Drawing.Point(prevX + 1, prevY);
                if (prevCamX == this.model.Camera.GetViewportX)
                {
                    this.model.Hero.Position = new System.Drawing.Point(this.model.Camera.WindowWidth / 2, prevY);
                }
            }

            this.backgroundCache = new GeometryDrawing(this.GetBrush(this.model.CurrentWorld.WorldName), null, g);
            return this.backgroundCache;
        }

        private Drawing GetTitle()
        {
            if (!this.gameObjects.TryGetValue("title", out IGameObject title))
            {
                StaticObject newtitle = new StaticObject(83, 230, "title", new System.Drawing.Point((this.model.Camera.WindowWidth / 2) - (230 / 2 * this.model.CurrentWorld.Magnify / 2), this.model.Camera.WindowHeight / 10));
                this.gameObjects.Add("title", newtitle);
                title = newtitle;
            }

            if (this.WindowChanged)
            {
                this.gameObjects["title"].Position = new System.Drawing.Point((this.model.Camera.WindowWidth / 2) - (title.Width / 2 * this.model.CurrentWorld.Magnify / 2), this.model.Camera.WindowHeight / 10);
            }

            Geometry g = new RectangleGeometry(new Rect(title.Position.X, title.Position.Y, title.Width * this.model.CurrentWorld.Magnify / 2, title.Height * this.model.CurrentWorld.Magnify / 2));
            this.titleCache = new GeometryDrawing(this.GetSpriteBrush(title), null, g);
            title.CurrentSprite = this.spriteFps % this.spriteBrushes[title][0].Length;
            return this.titleCache;
        }

        private Drawing GetCollision() // For debug
        {
            GeometryGroup g = new GeometryGroup();
            for (int y = 0; y < this.model.CurrentWorld.GetTileHeight; y++)
            {
                for (int x = 0; x < this.model.CurrentWorld.GetTileWidth; x++)
                {
                    if (this.model.CurrentWorld.SearchGround(new System.Drawing.Point(x, y)))
                    {
                        g.Children.Add(new RectangleGeometry(new Rect(this.model.CurrentWorld.ConvertTileToPixel(x) + this.model.Camera.GetViewportX, this.model.CurrentWorld.ConvertTileToPixel(y) + this.model.Camera.GetViewportY, this.model.CurrentWorld.TileSize * this.model.CurrentWorld.Magnify, this.model.CurrentWorld.TileSize * this.model.CurrentWorld.Magnify)));
                    }
                }
            }

            this.walls = new GeometryDrawing(Brushes.White, null, g);
            return this.walls;
        }

        private Drawing GetPlayer()
        {
            Geometry g = new RectangleGeometry(new Rect(this.model.Camera.GetRelativeCharacterPosX, this.model.Camera.GetRelativeCharacterPosY, this.model.Hero.Width * this.model.CurrentWorld.Magnify, this.model.Hero.Height * this.model.CurrentWorld.Magnify));
            this.playerCache = new GeometryDrawing(this.GetSpriteBrush(this.model.Hero), null, g);
            this.model.Hero.CurrentSprite = this.spriteFps % this.spriteBrushes[this.model.Hero].Length;
            return this.playerCache;
        }
    }
}
