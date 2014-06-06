using System;
using System.Drawing;

namespace ImagesToDb
{
    public class CroppingProcessor
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
    }
}
