// <copyright file="MapRecordUI.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.Data
{
    using System;
    using System.Linq;
    using GalaSoft.MvvmLight;
    using TimeWar.Data.Models;

    /// <summary>
    /// Map ui data class.
    /// </summary>
    public class MapRecordUI : ObservableObject
    {
        private int mapId;
        private int? playerId;
        private TimeSpan runTime;
        private string mapName;
        private PlayerProfile player;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapRecordUI"/> class.
        /// </summary>
        public MapRecordUI()
        {
        }

        /// <summary>
        /// Gets or sets map id.
        /// </summary>
        public int MapRecordId
        {
            get { return this.mapId; }
            set { this.Set(ref this.mapId, value); }
        }

        /// <summary>
        /// Gets or sets map name.
        /// </summary>
        public string MapName
        {
            get { return this.mapName; }
            set { this.Set(ref this.mapName, value); }
        }

        /// <summary>
        /// Gets or sets player profile navigational property.
        /// </summary>
        public int? PlayerId
        {
            get { return this.playerId; }
            set { this.Set(ref this.playerId, value); }
        }

        /// <summary>
        /// Gets or sets player.
        /// </summary>
        public PlayerProfile Player
        {
            get { return this.player; }
            set { this.Set(ref this.player, value); }
        }

        /// <summary>
        /// Gets or sets run time.
        /// </summary>
        public TimeSpan RunTime
        {
            get { return this.runTime; }
            set { this.Set(ref this.runTime, value); }
        }

        /// <summary>
        /// Convert mapui entity to database entity.
        /// </summary>
        /// <param name="mapui">Map ui entity.</param>
        /// <returns>Map entity.</returns>
        public static MapRecord ConvertToMapEntity(MapRecordUI mapui)
        {
            MapRecord map = new MapRecord();
            if (mapui != null)
            {
                map.MapRecordId = mapui.MapRecordId;
                map.MapName = mapui.MapName;
                map.PlayerId = mapui.PlayerId;
                map.RunTime = mapui.RunTime;
                map.Player = mapui.Player;
            }

            return map;
        }

        /// <summary>
        /// Convert database map entity to ui entity.
        /// </summary>
        /// <param name="map">Map entity.</param>
        /// <returns>Mapui entity.</returns>
        public static MapRecordUI ConvertToMapUiEntity(MapRecord map)
        {
            MapRecordUI mapui = new MapRecordUI();
            if (map != null)
            {
                mapui.MapRecordId = map.MapRecordId;
                mapui.MapName = map.MapName;
                mapui.PlayerId = map.PlayerId;
                mapui.RunTime = map.RunTime;
                mapui.Player = map.Player;
            }

            return mapui;
        }

        /// <summary>
        /// Copy data from another Map element.
        /// </summary>
        /// <param name="other">Data source.</param>
        public void CopyFrom(MapRecordUI other)
        {
            this.GetType().GetProperties().ToList().ForEach(x => x.SetValue(this, x.GetValue(other)));
        }
    }
}
