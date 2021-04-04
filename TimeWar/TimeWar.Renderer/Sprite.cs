// <copyright file="Sprite.cs" company="Time War">
// Copyright (c) Time War. All rights reserved.
// </copyright>

namespace TimeWar.Renderer
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Media.Imaging;
    using System.Drawing;
    using System.Windows.Media;

    /// <summary>
    /// Sprite static class.
    /// </summary>
    public static class Sprite
    {
        public static ImageBrush[][] CreateSprite(int height, int width)
        {
            string spriteFilename = "Sprites/testman.png"; //for method 1
            int ximages = 1; //The number of sprites in each row
            int yimages = 4; //The number of sprites in each column
            int numberFrames = ximages * yimages;
            int currentFrameCount = 0;
            string fname = "testman";
            BitmapSource[][] sprites = new BitmapSource[yimages][];
            ImageBrush[][] brushes = new ImageBrush[yimages][];
            for (int i = 0; i < sprites.Length; i++)
            {
                sprites[i] = new BitmapSource[ximages];
                brushes[i] = new ImageBrush[ximages];
            }

            System.Drawing.Rectangle cropRect = new System.Drawing.Rectangle(0, 0, width, height);
            System.Drawing.Bitmap src = System.Drawing.Image.FromFile(spriteFilename) as System.Drawing.Bitmap;
            System.Drawing.Bitmap target;
            for (int row = 0; row < yimages; row++)
            {
                for (int col = 0; col < ximages; col++)
                {
                    int currentX = row * width;
                    int currentY = col * height;
                    cropRect.X = currentX;
                    cropRect.Y = currentY;
                    target = new Bitmap(cropRect.Width, cropRect.Height);
                    using (Graphics g = Graphics.FromImage(target))
                    {
                        g.DrawImage(src, new System.Drawing.Rectangle(0, 0, target.Width, target.Height), cropRect, System.Drawing.GraphicsUnit.Pixel);
                    }

                    BitmapSource frame = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(target.GetHbitmap(), IntPtr.Zero, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromWidthAndHeight(target.Width, target.Height));
                    sprites[row][col] = frame;
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
