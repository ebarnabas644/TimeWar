// <copyright file="Sprite.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Renderer
{
    using System;
    using System.Drawing;
    using System.Windows.Media;
    using System.Windows.Media.Imaging;

    /// <summary>
    /// Sprite static class.
    /// </summary>
    public static class Sprite
    {
        /// <summary>
        /// Create new image brush collection using spritesheet.
        /// </summary>
        /// <param name="height">Character height.</param>
        /// <param name="width">Character width.</param>
        /// <param name="fname">Spritesheet file name.</param>
        /// <returns>Return frames as Image brush 2D array.</returns>
        public static ImageBrush[][] CreateSprite(int height, int width, string fname)
        {
            string spriteFilename = string.Format(System.Globalization.CultureInfo.CurrentCulture, "Sprites/{0}.png", fname);
            System.Drawing.Bitmap src = System.Drawing.Image.FromFile(spriteFilename) as System.Drawing.Bitmap;
            int ximages = src.Width / width;
            int yimages = src.Height / height;
            int numberFrames = ximages * yimages;
            BitmapSource[][] sprites = new BitmapSource[yimages][];
            ImageBrush[][] brushes = new ImageBrush[yimages][];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = new BitmapSource[ximages];
                brushes[i] = new ImageBrush[ximages];
            }

            System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(0, 0, width, height);
            System.Drawing.Bitmap target;
            for (int row = 0; row < yimages; row++)
            {
                for (int col = 0; col < ximages; col++)
                {
                    int currentY = row * height;
                    int currentX = col * width;
                    cropRect.X = currentX;
                    cropRect.Y = currentY;
                    target = new Bitmap(cropRect.Width, cropRect.Height);
                    using (Graphics g = Graphics.FromImage(target))
                    {
                        g.DrawImage(src, new System.Drawing.Rectangle(0, 0, target.Width, target.Height), cropRect, System.Drawing.GraphicsUnit.Pixel);
                    }

                    BitmapSource frame = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(target.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(target.Width, target.Height));
                    sprites[row][col] = frame;
                    target.Dispose();
                }
            }

            for (int y = 0; y < yimages; y++)
            {
                for (int x = 0; x < ximages; x++)
                {
                    brushes[y][x] = new ImageBrush(sprites[y][x]);
                }
            }

            return brushes;
        }
    }
}
