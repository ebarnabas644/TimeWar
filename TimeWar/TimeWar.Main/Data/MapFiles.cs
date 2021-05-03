// <copyright file="MapFiles.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Main.Data
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    /// Map data class.
    /// </summary>
    public class MapFiles
    {
        private ObservableCollection<MapRecordUI> mapRecordUIs;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapFiles"/> class.
        /// </summary>
        /// <param name="name">Name of the map.</param>
        /// <param name="path">Path of the map.</param>
        /// <param name="mapRecordUIs">Map record collection.</param>
        public MapFiles(string name, string path, IList<MapRecordUI> mapRecordUIs)
        {
            this.Name = name;
            this.Path = path;
            this.mapRecordUIs = new ObservableCollection<MapRecordUI>();
            mapRecordUIs.Select(x => x).ToList().ForEach(x => this.mapRecordUIs.Add(x));
        }

        /// <summary>
        /// Gets or sets map name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets map path.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Gets map records.
        /// </summary>
        public ObservableCollection<MapRecordUI> MapRecords
        {
            get { return this.mapRecordUIs; }
        }
    }
}
