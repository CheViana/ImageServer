using System;
using System.Drawing;

namespace ImageServer.Bussiness
{
    public class ImageProcessor
    {
        
        public Bitmap SizeCrop(Image image, int x, int y, int w, int h)
        {
            return TileFromSizes(image, h, y, w, x);
        }


        public Bitmap PercentageCrop(Image image, int px, int py, int pw, int ph)
        {
            var finalWidth = (int) Math.Floor((pw/100.0) * image.Width);
            var finalHeigth = (int) Math.Floor((ph / 100.0) * image.Height);
            var xOffset = (int) Math.Floor(image.Width * (px / 100.0));
            var yOffset = (int) Math.Floor(image.Height * (py / 100.0));
            return TileFromSizes(image, finalHeigth, yOffset, finalWidth, xOffset);
        }

        private static Bitmap TileFromSizes(Image image, int targetHeigth, int yOffset, int targetWidth, int xOffset)
        {
            if (image.Height < targetHeigth + yOffset) targetHeigth = image.Height - yOffset;
            if (image.Width < targetWidth + xOffset) targetWidth = image.Width - xOffset;

            var currentTile = new Bitmap(targetWidth, targetHeigth);
            currentTile.SetResolution(image.HorizontalResolution, image.VerticalResolution);
            var currentTileGraphics = Graphics.FromImage(currentTile);
            currentTileGraphics.Clear(Color.Black);
            var absentRectangleArea = new Rectangle(xOffset, yOffset, targetWidth, targetHeigth);
            currentTileGraphics.DrawImage(image, 0, 0, absentRectangleArea, GraphicsUnit.Pixel);
            return currentTile;
        }

        public Image PercentageScaling(Bitmap image, int n)
        {
            var width = (int) Math.Floor(image.Width*(n/100.0));
            var height = (int) Math.Floor(image.Height*(n/100.0));
            return image.GetThumbnailImage(width, height, ThumbnailCallback, IntPtr.Zero);
        }

        public Image SizeScaling(Bitmap image, int width, int height, bool distorted)
        {
            double realRatio = ((double)image.Height) / ((double)image.Width);
            if (width != 0 & height != 0)
            {
                if (distorted) return image.GetThumbnailImage(width, height, ThumbnailCallback, IntPtr.Zero);
                // TODO not correct, when not distorted
                var heightAfterRatio = (int) Math.Floor(realRatio*width);
                var widthAfterRatio = (int) Math.Floor(height/realRatio);
                return image.GetThumbnailImage(widthAfterRatio, heightAfterRatio, ThumbnailCallback, IntPtr.Zero);
            }
            if (width != 0)
            {
                var heightAfterRatio = (int)Math.Floor(realRatio * width);
                return image.GetThumbnailImage(width, heightAfterRatio, ThumbnailCallback, IntPtr.Zero);
            }
            if (height != 0)
            {
                var widthAfterRatio = (int)Math.Floor(height / realRatio);
                return image.GetThumbnailImage(widthAfterRatio, height, ThumbnailCallback, IntPtr.Zero);
            }
            return image;
        }

        public static bool ThumbnailCallback()
        {
            return false;
        }
    }
}
