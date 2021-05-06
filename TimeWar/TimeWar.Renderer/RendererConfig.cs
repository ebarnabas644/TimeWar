// <copyright file="RendererConfig.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Renderer config class.
    /// </summary>
    public static class RendererConfig
    {
        /// <summary>
        /// Gets layers height.
        /// </summary>
        public static int LayersHeight { get; } = 600;

        /// <summary>
        /// Gets layers width.
        /// </summary>
        public static int LayersWidth { get; } = 430;

        /// <summary>
        /// Gets number of tile horizontal repeat.
        /// </summary>
        public static double BackgroundHorizontalTileNumber { get; } = 6;

        /// <summary>
        /// Gets number of tile vertical repeat.
        /// </summary>
        public static double BackgroundVerticalTileNumber { get; } = 1;

        /// <summary>
        /// Gets number of background layers.
        /// </summary>
        public static int NumberOfLayers { get; } = 3;

        /// <summary>
        /// Gets Layers vertical speed.
        /// </summary>
        public static IReadOnlyList<double> LayersVerticalSpeed { get; } = new List<double> { 0.02, 0.02, 0.02 };

        /// <summary>
        /// Gets Layers horizontal speed.
        /// </summary>
        public static IReadOnlyList<double> LayersHorizontalSpeed { get; } = new List<double> { 0.6, 0.4, 0.1 };

        /// <summary>
        /// Gets Layers sprite file names.
        /// </summary>
        public static IReadOnlyList<string> LayersSpriteFile { get; } = new List<string> { "backgroundlayer1", "backgroundlayer2", "backgroundlayer3" };

        /// <summary>
        /// Gets Layers horizontal offset.
        /// </summary>
        public static int LayersHorizontalOffset { get; }

        /// <summary>
        /// Gets Layers vertical offset.
        /// </summary>
        public static int LayersVerticalOffset { get; } = -800;
    }
}
