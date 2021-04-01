﻿// <copyright file="InitLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Xml.Linq;
    using TimeWar.Model;
    using TimeWar.Model.Objects;

    /// <summary>
    /// Init class for game model.
    /// </summary>
    public class InitLogic
    {
        private GameModel model;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitLogic"/> class.
        /// </summary>
        /// <param name="model">Game model instance.</param>
        /// <param name="mapName">Name of the game map.</param>
        public InitLogic(GameModel model, string mapName)
        {
            this.model = model;
            this.BuildModel(mapName);
        }

        private static string[][] GetGround(XDocument mapData)
        {
            string[][] grounddata = mapData.Element("map").Elements("layer").Where(x => x.Attribute("name").Value == "ground").Select(x => x.Element("data").Value).FirstOrDefault().Trim().Split("\n").Select(x => x.Split(",")).Select(x => x.Where(x => x.Length != 0).ToArray()).ToArray();
            return grounddata;
        }

        private static string[][] GetPoi(XDocument mapData)
        {
            string[][] poidata = mapData.Element("map").Elements("layer").Where(x => x.Attribute("name").Value == "pointofinterests").Select(x => x.Element("data").Value).FirstOrDefault().Trim().Split("\n").Select(x => x.Split(",")).Select(x => x.Where(x => x.Length != 0).ToArray()).ToArray();
            return poidata;
        }

        private static int GetTileSize(XDocument mapData)
        {
            return Convert.ToInt32(mapData.Element("map").Attribute("tilewidth").Value, System.Globalization.CultureInfo.CurrentCulture);
        }

        private static void FillGround(GameWorld gameWorld, string[][] grounddata)
        {
            for (int y = 0; y < gameWorld.GetTileHeight; y++)
            {
                for (int x = 0; x < gameWorld.GetTileWidth; x++)
                {
                    if (grounddata[y][x] == "1")
                    {
                        gameWorld.AddGround(new Point(x, y));
                    }
                }
            }
        }

        private static void FillPoi(GameWorld gameWorld, string[][] poidata)
        {
            for (int y = 0; y < gameWorld.GetTileHeight; y++)
            {
                for (int x = 0; x < gameWorld.GetTileWidth; x++)
                {
                    switch (Convert.ToInt32(poidata[y][x], System.Globalization.CultureInfo.CurrentCulture))
                    {
                        case 472: gameWorld.AddPointOfInterest("start", new Point(x, y)); break;
                        case 473: gameWorld.AddPointOfInterest("chechpoint", new Point(x, y)); break;
                        case 474: gameWorld.AddPointOfInterest("finish", new Point(x, y)); break;
                    }
                }
            }
        }

        private Player GetPlayer()
        {
            int startX = this.model.CurrentWorld.SearchPointOfInterest("start").X * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize;
            int startY = this.model.CurrentWorld.SearchPointOfInterest("start").Y * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize;
            return new Player(new Point(startX, startY), this.model.CurrentWorld.TileSize, 100);
        }

        private void BuildModel(string mapName)
        {
            XDocument mapData = XDocument.Load("Leveldata\\" + mapName + ".tmx");
            string[][] grounddata = GetGround(mapData);
            string[][] poidata = GetPoi(mapData);
            int gameWorldHeight = grounddata.Length;
            int gameWorldWidth = grounddata[0].Length;
            int tileSize = GetTileSize(mapData);
            this.model.CurrentWorld = new GameWorld(gameWorldHeight, gameWorldWidth, tileSize);
            this.model.CurrentWorld.WorldName = mapName;
            FillGround(this.model.CurrentWorld, grounddata);
            FillPoi(this.model.CurrentWorld, poidata);
            this.model.Hero = this.GetPlayer();
        }
    }
}