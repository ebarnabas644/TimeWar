// <copyright file="GameRenderer.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;
    using TimeWar.Model;

    /// <summary>
    /// Game rendering class.
    /// </summary>
    public class GameRenderer
    {
        private GameModel model;
        private Drawing backgroundCache;
        private System.Drawing.Point playerPosition;
        private Drawing playerCache;
        private double windowHeightCache;
        private double windowWidthCache;
        private Dictionary<string, Brush> brushes;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRenderer"/> class.
        /// </summary>
        /// <param name="model">Game model entity.</param>
        public GameRenderer(GameModel model)
        {
            this.model = model;
            if (this.model != null)
            {
                this.windowHeightCache = this.model.Camera.WindowHeight;
                this.windowWidthCache = this.model.Camera.WindowWidth;
            }

            this.brushes = new Dictionary<string, Brush>();
        }

        /// <summary>
        /// Build drawed game world.
        /// </summary>
        /// <returns>Drawing with all entities for render.</returns>
        public Drawing BuildDrawing()
        {
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(this.GetBackground());
            dg.Children.Add(this.GetPlayer());
            return dg;
        }

        private void Reset()
        {
            this.backgroundCache = null;
            this.playerPosition = new System.Drawing.Point(1, 1);
            this.brushes.Clear();
        }

        private Brush GetBrush(string fname)
        {
            if (!this.brushes.ContainsKey(fname))
            {
                // ImageBrush ib = new ImageBrush(new BitmapImage(new Uri(uri)));
                BitmapImage bmp = new BitmapImage();
                bmp.BeginInit();
                bmp.UriSource = new Uri(string.Format(System.Globalization.CultureInfo.CurrentCulture, "Leveldata/{0}.png", fname), UriKind.Relative);
                bmp.EndInit();
                ImageBrush ib = new ImageBrush(bmp);
                this.brushes.Add(fname, ib);
            }

            return this.brushes[fname];
        }

        private Drawing GetBackground()
        {
            Geometry g = new RectangleGeometry(new Rect(this.model.Camera.GetViewportX, this.model.Camera.GetViewportY, this.model.CurrentWorld.GameWidth, this.model.CurrentWorld.GameHeight));
            this.backgroundCache = new GeometryDrawing(this.GetBrush(this.model.CurrentWorld.WorldName), null, g);
            return this.backgroundCache;
        }

        private Drawing GetPlayer()
        {
            if (this.playerCache == null || this.playerPosition != this.model.Hero.Position || this.windowHeightCache != this.model.Camera.WindowHeight || this.windowWidthCache != this.model.Camera.WindowWidth)
            {
                Geometry g = new RectangleGeometry(new Rect(this.model.Camera.GetRelativeCharacterPosX, this.model.Camera.GetRelativeCharacterPosY, this.model.CurrentWorld.TileSize * this.model.CurrentWorld.Magnify, this.model.CurrentWorld.TileSize * this.model.CurrentWorld.Magnify));
                this.playerCache = new GeometryDrawing(Brushes.Red, null, g);
                this.playerPosition = this.model.Hero.Position;
                this.windowHeightCache = this.model.Camera.WindowHeight;
                this.windowWidthCache = this.model.Camera.WindowWidth;
            }

            return this.playerCache;
        }
    }
}
