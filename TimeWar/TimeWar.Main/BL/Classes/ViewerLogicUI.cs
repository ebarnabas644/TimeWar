// <copyright file="ViewerLogicUI.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.BL.Classes
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using TimeWar.Main.BL.Interfaces;
    using TimeWar.Main.Data;

    /// <summary>
    /// Viewer logic ui class.
    /// </summary>
    internal class ViewerLogicUI : IViewerLogicUI
    {
        private Factory factory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewerLogicUI"/> class.
        /// </summary>
        /// <param name="factory">Factory instance.</param>
        public ViewerLogicUI(Factory factory)
        {
            this.factory = factory;
        }

        /// <inheritdoc/>
        public PlayerProfileUI GetSelectedProfile()
        {
            return PlayerProfileUI.ConvertToProfileUiEntity(this.factory.ViewerLogic.GetProfiles().Where(x => x.Selected == true).FirstOrDefault());
        }

        /// <inheritdoc/>
        public IList<MapRecordUI> GetMaps()
        {
            IList<MapRecordUI> mapUIs = new List<MapRecordUI>();
            this.factory.ViewerLogic.GetMaps().ToList().ForEach(x => mapUIs.Add(MapRecordUI.ConvertToMapUiEntity(x)));
            return mapUIs;
        }

        /// <inheritdoc/>
        public IList<PlayerProfileUI> GetProfiles()
        {
            IList<PlayerProfileUI> profileUIs = new List<PlayerProfileUI>();
            this.factory.ViewerLogic.GetProfiles().ToList().ForEach(x => profileUIs.Add(PlayerProfileUI.ConvertToProfileUiEntity(x)));
            return profileUIs;
        }

        /// <inheritdoc/>
        public IList<SaveUI> GetSaves()
        {
            IList<SaveUI> saveUIs = new List<SaveUI>();
            this.factory.ViewerLogic.GetSaves().ToList().ForEach(x => saveUIs.Add(SaveUI.ConvertToSaveUiEntity(x)));
            return saveUIs;
        }

        /// <inheritdoc/>
        public IList<MapFiles> LoadMaps()
        {
            string[] names = Directory.GetFiles("Leveldata").Select(x => x.Substring(10, x.Length - 14)).ToArray();
            string[] files = Directory.GetFiles("Leveldata");
            IList<MapFiles> maps = new List<MapFiles>();
            int mapcounter = 0;
            for (int i = 0; i < files.Length; i += 2)
            {
                maps.Add(new MapFiles(names[i], "/" + files[i].Replace('\\', '/'), this.GetMaps().Where(x => x.MapName == names[i]).OrderBy(x => x.RunTime).ToList()));
                mapcounter++;
            }

            return maps;
        }
    }
}
