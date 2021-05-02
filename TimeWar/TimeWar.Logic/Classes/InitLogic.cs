// <copyright file="InitLogic.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Logic
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Xml.Linq;
    using TimeWar.Logic.Classes;
    using TimeWar.Logic.Classes.Characters;
    using TimeWar.Model;
    using TimeWar.Model.Objects;
    using TimeWar.Model.Objects.Classes;

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

        private static string[][] GetDecorations(XDocument mapData)
        {
            string[][] decodata = mapData.Element("map").Elements("layer").Where(x => x.Attribute("name").Value == "spritedeco").Select(x => x.Element("data").Value).FirstOrDefault().Trim().Split("\n").Select(x => x.Split(",")).Select(x => x.Where(x => x.Length != 0).ToArray()).ToArray();
            return decodata;
        }

        private static string[][] GetEnemies(XDocument mapData)
        {
            string[][] decodata = mapData.Element("map").Elements("layer").Where(x => x.Attribute("name").Value == "enemy").Select(x => x.Element("data").Value).FirstOrDefault().Trim().Split("\n").Select(x => x.Split(",")).Select(x => x.Where(x => x.Length != 0).ToArray()).ToArray();
            return decodata;
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
                    if (grounddata[y][x] != "0")
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
                        case 472: gameWorld.StartPoint = new Point(x, y); break;
                        case 473: gameWorld.AddPOI(new PointOfInterest(POIType.Checkpoint, 1, 1, "testenemy", new Point(x, y))); break;
                        case 474: gameWorld.AddPOI(new PointOfInterest(POIType.Finish, 1, 1, "testenemy", new Point(x, y))); break;
                        case 496: gameWorld.AddPOI(new PointOfInterest(POIType.HealthKit, 1, 1, "testenemy", new Point(x, y))); break;
                        case 497: gameWorld.AddPOI(new PointOfInterest(POIType.HighJump, 1, 1, "testenemy", new Point(x, y))); break;
                        case 498: gameWorld.AddPOI(new PointOfInterest(POIType.Invincibility, 1, 1, "testenemy", new Point(x, y))); break;
                        case 499: gameWorld.AddPOI(new PointOfInterest(POIType.RapidFire, 1, 1, "testenemy", new Point(x, y))); break;
                        case 500: gameWorld.AddPOI(new PointOfInterest(POIType.UnlockWeapon, 1, 1, "testenemy", new Point(x, y))); break;
                    }
                }
            }
        }

        private static void FillDeco(GameWorld gameWorld, string[][] decodata)
        {
            for (int y = 0; y < gameWorld.GetTileHeight; y++)
            {
                for (int x = 0; x < gameWorld.GetTileWidth; x++)
                {
                    if (decodata[y][x] != "0")
                    {
                        gameWorld.AddDecoration(new Point(x, y), Convert.ToInt32(decodata[y][x], System.Globalization.CultureInfo.CurrentCulture) - 1);
                    }
                }
            }
        }

        private void FillEnemy(GameWorld gameWorld, string[][] enemydata)
        {
            for (int y = 0; y < gameWorld.GetTileHeight; y++)
            {
                for (int x = 0; x < gameWorld.GetTileWidth; x++)
                {
                    switch (Convert.ToInt32(enemydata[y][x], System.Globalization.CultureInfo.CurrentCulture))
                    {
                        case 475: gameWorld.AddEnemy(new Enemy(new Point(x * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize, (y * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize) - (EnemyInitLogic.BasicEnemyHeight * this.model.CurrentWorld.TileSize)), EnemyInitLogic.RapidFireEnemyHealth, EnemyInitLogic.BasicEnemyHeight, EnemyInitLogic.BasicEnemyWidth, EnemyType.RapidFire, EnemyInitLogic.RapidFireEnemySpritesheet)); break;

                        case 476: gameWorld.AddEnemy(new Enemy(new Point(x * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize, (y * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize) - (EnemyInitLogic.BasicEnemyHeight * this.model.CurrentWorld.TileSize)), EnemyInitLogic.HeavyEnemyHealth, EnemyInitLogic.BasicEnemyHeight, EnemyInitLogic.BasicEnemyWidth, EnemyType.Heavy, EnemyInitLogic.HeavyEnemySpritesheet)); break;

                        case 477: gameWorld.AddEnemy(new Enemy(new Point(x * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize, (y * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize) - (EnemyInitLogic.BasicEnemyHeight * this.model.CurrentWorld.TileSize)), EnemyInitLogic.BurstEnemyHealth, EnemyInitLogic.BasicEnemyHeight, EnemyInitLogic.BasicEnemyWidth, EnemyType.Burst, EnemyInitLogic.BurstEnemySpritesheet)); break;

                        case 478: gameWorld.AddEnemy(new Enemy(new Point(x * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize, (y * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize) - (EnemyInitLogic.BasicEnemyHeight * this.model.CurrentWorld.TileSize)), EnemyInitLogic.FastEnemyHealth, EnemyInitLogic.BasicEnemyHeight, EnemyInitLogic.BasicEnemyWidth, EnemyType.Fast, EnemyInitLogic.FastEnemySpritesheet)); break;

                        case 479: gameWorld.AddEnemy(new Enemy(new Point(x * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize, (y * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize) - (EnemyInitLogic.BasicEnemyHeight * this.model.CurrentWorld.TileSize)), EnemyInitLogic.BasicEnemyHealth, EnemyInitLogic.BasicEnemyHeight, EnemyInitLogic.BasicEnemyWidth, EnemyType.Basic, EnemyInitLogic.BasicEnemySpritesheet)); break;
                    }
                }
            }
        }

        private Player GetPlayer()
        {
            int startX = this.model.CurrentWorld.StartPoint.X * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize;
            int startY = this.model.CurrentWorld.StartPoint.Y * this.model.CurrentWorld.Magnify * this.model.CurrentWorld.TileSize;
            return new Player(new Point(startX, startY - (InitConfig.PlayerHeight * this.model.CurrentWorld.TileSize)), InitConfig.PlayerHealth, InitConfig.PlayerHeight, InitConfig.PlayerWidth, InitConfig.PlayerSpritesheet);
        }

        private void BuildModel(string mapName)
        {
            XDocument mapData = XDocument.Load("Leveldata\\" + mapName + ".tmx");
            string[][] grounddata = GetGround(mapData);
            string[][] poidata = GetPoi(mapData);
            string[][] decodata = GetDecorations(mapData);
            string[][] enemydata = GetEnemies(mapData);
            int gameWorldHeight = grounddata.Length;
            int gameWorldWidth = grounddata[0].Length;
            int tileSize = GetTileSize(mapData);
            this.model.CurrentWorld = new GameWorld(gameWorldHeight, gameWorldWidth, tileSize);
            this.model.CurrentWorld.WorldName = mapName;
            FillGround(this.model.CurrentWorld, grounddata);
            FillPoi(this.model.CurrentWorld, poidata);
            FillDeco(this.model.CurrentWorld, decodata);
            this.FillEnemy(this.model.CurrentWorld, enemydata);
            this.model.Hero = this.GetPlayer();
        }
    }
}
