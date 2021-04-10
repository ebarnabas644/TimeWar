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

    /// <summary>
    /// Game rendering class.
    /// </summary>
    public class GameRenderer
    {
        private GameModel model;
        private Stopwatch spriteTimer;
        private Drawing backgroundCache;
        private Drawing walls;
        private System.Drawing.Point playerPosition;
        private Drawing playerCache;
        private double windowHeightCache;
        private double windowWidthCache;
        private Dictionary<string, Brush> staticBrushes;
        private Dictionary<Character, ImageBrush[][]> spriteBrushes;
        private int spriteFps;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRenderer"/> class.
        /// </summary>
        /// <param name="model">Game model entity.</param>
        public GameRenderer(GameModel model)
        {
            this.model = model;
            this.spriteTimer = new Stopwatch();
            if (this.model != null)
            {
                this.windowHeightCache = this.model.Camera.WindowHeight;
                this.windowWidthCache = this.model.Camera.WindowWidth;
            }

            this.spriteBrushes = new Dictionary<Character, ImageBrush[][]>();
            this.staticBrushes = new Dictionary<string, Brush>();
            this.spriteTimer.Start();
        }

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
            dg.Children.Add(this.GetPlayer());
            return dg;
        }

        private void Reset()
        {
            this.backgroundCache = null;
            this.walls = null;
            this.playerPosition = new System.Drawing.Point(1, 1);
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

        private Brush GetSpriteBrush(Character character, string fname)
        {
            if (!this.spriteBrushes.ContainsKey(character))
            {
                this.spriteBrushes.Add(character, Sprite.CreateSprite(character.Height, character.Width, fname));
            }

            return this.spriteBrushes[character][0][this.model.Hero.CurrentSprite];
        }

        private Drawing GetBackground()
        {
            Geometry g = new RectangleGeometry(new Rect(this.model.Camera.GetViewportX, this.model.Camera.GetViewportY, this.model.CurrentWorld.GameWidth, this.model.CurrentWorld.GameHeight));
            this.backgroundCache = new GeometryDrawing(this.GetBrush(this.model.CurrentWorld.WorldName), null, g);
            return this.backgroundCache;
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
            this.playerCache = new GeometryDrawing(this.GetSpriteBrush(this.model.Hero, this.model.Hero.SpriteFile), null, g);
            this.model.Hero.CurrentSprite = this.spriteFps % 4;
            this.playerPosition = this.model.Hero.Position;
            this.windowHeightCache = this.model.Camera.WindowHeight;
            this.windowWidthCache = this.model.Camera.WindowWidth;
            return this.playerCache;
        }
    }
}
