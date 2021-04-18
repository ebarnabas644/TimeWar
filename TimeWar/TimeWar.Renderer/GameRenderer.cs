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
    using TimeWar.Model.Objects.Classes;
    using TimeWar.Model.Objects.Interfaces;

    /// <summary>
    /// Game rendering class.
    /// </summary>
    public class GameRenderer
    {
        private GameModel model;
        private Stopwatch spriteTimer;
        private Stopwatch test;
        private Drawing backgroundCache;
        private Drawing walls;
        private Drawing titleCache;
        private Drawing playerCache;
        private Dictionary<string, Brush> staticBrushes;
        private Dictionary<IGameObject, ImageBrush[][]> spriteBrushes;
        private Dictionary<string, IGameObject> gameObjects;
        private HashSet<IGameObject> uniqueObjectCache;
        private HashSet<string> loadCache;
        private int currentSprite;
        private bool firstRun;
        private DrawingGroup spritesCache;
        private bool menuMode;
        private bool scrollMode;
        private bool title;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameRenderer"/> class.
        /// </summary>
        /// <param name="model">Game model entity.</param>
        /// <param name="menuMode">Game menu mode.</param>
        /// <param name="scrollmode">Auto scrolling.</param>
        /// <param name="title">Title enabled.</param>
        public GameRenderer(GameModel model, bool menuMode, bool scrollmode = false, bool title = false)
        {
            this.model = model;
            this.spriteTimer = new Stopwatch();
            this.test = new Stopwatch();
            this.menuMode = menuMode;
            this.scrollMode = scrollmode;
            this.title = title;
            this.spriteBrushes = new Dictionary<IGameObject, ImageBrush[][]>();
            this.gameObjects = new Dictionary<string, IGameObject>();
            this.staticBrushes = new Dictionary<string, Brush>();
            this.uniqueObjectCache = new HashSet<IGameObject>();
            this.loadCache = new HashSet<string>();
            this.spriteTimer.Start();
            this.WindowChanged = false;
            this.currentSprite = 0;
            this.firstRun = true;
            this.InitDecorations();
            this.spritesCache = new DrawingGroup();
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
            this.currentSprite = (int)this.spriteTimer.Elapsed.TotalMilliseconds / 100;
            DrawingGroup dg = new DrawingGroup();
            dg.Children.Add(this.GetBackground());
            dg.Children.Add(this.GetDecorations());

            // dg.Children.Add(this.GetCollision());
            if (!this.menuMode)
            {
                dg.Children.Add(this.GetPlayer());
            }
            else if (this.menuMode && this.title)
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

        private Brush GetSpriteBrush(IGameObject obj, bool tiled = false, double boundx = 0, double boundy = 0)
        {
            if (!this.spriteBrushes.ContainsKey(obj))
            {
                ImageBrush[][] imageBrushes = Sprite.CreateSprite(obj.Height, obj.Width, obj.SpriteFile);
                if (tiled)
                {
                    for (int i = 0; i < imageBrushes.Length; i++)
                    {
                        for (int y = 0; y < imageBrushes[i].Length; y++)
                        {
                            imageBrushes[i][y].TileMode = TileMode.Tile;
                            imageBrushes[i][y].Viewport = new Rect(0, 0, (obj.Width * this.model.CurrentWorld.Magnify) / boundx, (obj.Height * this.model.CurrentWorld.Magnify) / boundy);
                            imageBrushes[i][y].ViewportUnits = BrushMappingMode.RelativeToBoundingBox;
                            imageBrushes[i][y].Stretch = Stretch.Fill;
                        }
                    }
                }

                this.spriteBrushes.Add(obj, imageBrushes);
            }

            return this.spriteBrushes[obj][0][this.currentSprite % this.spriteBrushes[obj][0].Length];
        }

        private Drawing GetBackground()
        {
            Geometry g = new RectangleGeometry(new Rect(this.model.Camera.GetViewportX, this.model.Camera.GetViewportY, this.model.CurrentWorld.GameWidth, this.model.CurrentWorld.GameHeight));
            if (this.menuMode && this.scrollMode)
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
                StaticObject newtitle = new StaticObject(83, 230, "title", new System.Drawing.Point((this.model.Camera.WindowWidth / 2) - (230 / 2 * this.model.CurrentWorld.Magnify / 2), this.model.Camera.WindowHeight / 10), true);
                this.gameObjects.Add("title", newtitle);
                title = newtitle;
            }

            if (this.WindowChanged)
            {
                this.gameObjects["title"].Position = new System.Drawing.Point((this.model.Camera.WindowWidth / 2) - (title.Width / 2 * this.model.CurrentWorld.Magnify / 2), this.model.Camera.WindowHeight / 10);
            }

            Geometry g = new RectangleGeometry(new Rect(title.Position.X, title.Position.Y, title.Width * this.model.CurrentWorld.Magnify / 2, title.Height * this.model.CurrentWorld.Magnify / 2));
            this.titleCache = new GeometryDrawing(this.GetSpriteBrush(title), null, g);
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
            return this.playerCache;
        }

        private void InitDecorations()
        {
            int id = 0;
            for (int y = 0; y < this.model.CurrentWorld.GetTileHeight; y++)
            {
                for (int x = 0; x < this.model.CurrentWorld.GetTileWidth; x++)
                {
                    if (!this.gameObjects.ContainsKey("deco" + id) && this.model.CurrentWorld.SearchDecoration(new System.Drawing.Point(x, y)) != 0)
                    {
                        StaticObject obj = new StaticObject(this.model.CurrentWorld.TileSize, this.model.CurrentWorld.TileSize, this.model.CurrentWorld.SearchDecoration(new System.Drawing.Point(x, y)).ToString(System.Globalization.CultureInfo.CurrentCulture), new System.Drawing.Point(this.model.CurrentWorld.ConvertTileToPixel(x), this.model.CurrentWorld.ConvertTileToPixel(y)));
                        this.gameObjects.Add("deco" + id, obj);
                        this.uniqueObjectCache.Add(obj);
                        id++;
                    }
                }
            }
        }

        private Drawing GetDecorations()
        {
            DrawingGroup dg = new DrawingGroup();
            Dictionary<string, GeometryGroup> deco = new Dictionary<string, GeometryGroup>();
            foreach (var item in this.gameObjects)
            {
                if (item.Value is StaticObject)
                {
                    StaticObject st = (StaticObject)item.Value;
                    if (!st.Hud)
                    {
                        Geometry g = new RectangleGeometry(new Rect(this.model.Camera.GetRelativeObjectPosX(st.Position.X), this.model.Camera.GetRelativeObjectPosY(st.Position.Y), st.Width * this.model.CurrentWorld.Magnify, st.Height * this.model.CurrentWorld.Magnify));
                        if (!deco.ContainsKey(st.SpriteFile))
                        {
                            deco.Add(st.SpriteFile, new GeometryGroup());
                        }

                        deco[st.SpriteFile].Children.Add(g);
                    }
                }
            }

            if (this.firstRun)
            {
                foreach (var item in this.gameObjects)
                {
                    if (item.Value is StaticObject)
                    {
                        StaticObject st = (StaticObject)item.Value;
                        if (!st.Hud && !this.loadCache.Contains(st.SpriteFile))
                        {
                            dg.Children.Add(new GeometryDrawing(this.GetSpriteBrush(item.Value, true, deco[item.Value.SpriteFile].Bounds.Size.Width, deco[item.Value.SpriteFile].Bounds.Size.Height), null, deco[item.Value.SpriteFile]));
                            this.loadCache.Add(st.SpriteFile);
                        }
                    }
                }

                this.firstRun = false;
            }

            foreach (var item in this.uniqueObjectCache)
            {
                if (item is StaticObject)
                {
                    StaticObject st = (StaticObject)item;
                    if (!st.Hud)
                    {
                        dg.Children.Add(new GeometryDrawing(this.GetSpriteBrush(item, true), null, deco[item.SpriteFile]));
                    }
                }
            }

            return dg;
        }
    }
}
