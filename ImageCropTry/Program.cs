using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace ImageCropTry
{
    class Program
    {
        
        static void TestBigImagePartDrawing()
        {
            using (var absentRectangleImage = (Bitmap)Bitmap.FromFile(@"C:\photoes\acid-trip-tiff.tif"))
            {
                using (var currentTile = new Bitmap(256, 256))
                {
                    currentTile.SetResolution(absentRectangleImage.HorizontalResolution, absentRectangleImage.VerticalResolution);

                    using (var currentTileGraphics = Graphics.FromImage(currentTile))
                    {
                        currentTileGraphics.Clear(Color.Black);
                        var absentRectangleArea = new Rectangle(300, 300, 256, 256);
                        currentTileGraphics.DrawImage(absentRectangleImage, 0, 0, absentRectangleArea, GraphicsUnit.Pixel);
                    }

                    currentTile.Save(@"C:\photoes\acid-trip-tile.bmp");
                }
            }
        }
        static void Main(string[] args)
        {
            TestBigImagePartDrawing();
        }
    }
}
