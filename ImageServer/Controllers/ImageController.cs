using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Drawing;
using System.Drawing.Imaging;

using System.IO;

namespace ImageServer.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Index(int x,int y,int w,int h,int percentage=100)
        {
            using (var absentRectangleImage = (Bitmap)Bitmap.FromFile(@"D:\wallpapers\july-10-photographydock-nocal-1920x1200.tif"))
            {
                var imageHeigth = absentRectangleImage.Height;
                var imageWidth = absentRectangleImage.Width;
                var finalWidth = w;
                var finalHeigth = h;
                if (imageHeigth < y + h)
                {
                    finalHeigth = imageHeigth - y;
                }
                if (imageWidth < x + w)
                {
                    finalWidth = imageWidth - x;
                }
                using (var currentTile = new Bitmap(finalWidth, finalHeigth))
                {
                    currentTile.SetResolution(absentRectangleImage.HorizontalResolution, absentRectangleImage.VerticalResolution);

                    using (var currentTileGraphics = Graphics.FromImage(currentTile))
                    {
                        currentTileGraphics.Clear(Color.Black);
                        var absentRectangleArea = new Rectangle(x, y, w, h);
                        currentTileGraphics.DrawImage(absentRectangleImage, 0, 0, absentRectangleArea, GraphicsUnit.Pixel);
                    }

                    using (var memStream = new MemoryStream())
                    {
                        currentTile.Save(memStream, ImageFormat.Jpeg);
                        var bytes = memStream.ToArray();
                        return base.File(bytes,"image/jpeg");
                    }
                }
            }           
        }

        public ActionResult Full(string format = "jpeg") 
        {
            using (var absentRectangleImage =
                    (Bitmap) Bitmap.FromFile(@"D:\wallpapers\july-10-photographydock-nocal-1920x1200.tif"))
            {
                using (var memStream = new MemoryStream())
                {
                    absentRectangleImage.Save(memStream, ImageFormat.Jpeg);
                    var bytes = memStream.ToArray();
                    return base.File(bytes, "image/"+format);
                }
            }
        }


    }
}
