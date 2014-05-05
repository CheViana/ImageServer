using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using ImageServer.Bussiness;
using ImageServer.Bussiness.ImageEventArgs;

namespace ImageServer.Controllers
{
    public class ImageController : Controller
    {
        private readonly CroppingProcessor cropProcessor = new CroppingProcessor();
        private readonly ScalingProcessor scaleProcessor = new ScalingProcessor();
        private readonly RotatedProcessor rotatedProcessor = new RotatedProcessor();
        private readonly ColorProcessor colorProcessor = new ColorProcessor();
        private readonly FormatProcessor formatProcessor = new FormatProcessor();
        private readonly Image image = Image.FromFile(@"D:\wallpapers\july-10-photographydock-nocal-1920x1200.tif");

        private ActionResult RotateAndColorAndFormat(Image im, float rotate, string color, string format)
        {
            var bitmap = new Bitmap(im);
            if (Math.Abs(rotate) > 0.0001)
            {
                var rotatedImage = rotatedProcessor.RotateImg(bitmap, rotate);
                bitmap = rotatedImage;
            }
            if (!string.IsNullOrEmpty(color))
            {
                var colorImage = colorProcessor.ChangeColor(bitmap, color);
                bitmap = colorImage;
            }
            using (var memStream = new MemoryStream())
            {
                bitmap.Save(memStream, ImageFormat.Jpeg);
                var bytes = memStream.ToArray();
                var mime = formatProcessor.ConvertFormatToMime(format);
                return File(bytes, mime);
            }
        }

        //public ActionResult Index(BaseImageArgs args)
        //{
            
        //}


        public ActionResult SizeCropSizeScale(string id, int x, int y, int w, int h, int tw, int th, bool distorted = false, float rotated = 0, string color = "", string format = "jpg")
        {
            var cropped = cropProcessor.SizeCrop(image, x, y, w, h);
            var scaledCropped = scaleProcessor.SizeScaling(cropped, tw, th, distorted);
            return RotateAndColorAndFormat(scaledCropped, rotated, color, format);
        }
        public ActionResult SizeCropPercentageScale(string id, int x, int y, int w, int h, int p, float rotated = 0, string color = "", string format = "jpg")
        {
            var cropped = cropProcessor.SizeCrop(image, x, y, w, h);
            var scaledCropped = scaleProcessor.PercentageScaling(cropped, p);
            return RotateAndColorAndFormat(scaledCropped, rotated, color, format);
        }
        public ActionResult PercentageCropSizeScale(string id, int px, int py, int pw, int ph, int tw, int th, bool distorted = false, float rotated = 0, string color = "", string format = "jpg")
        {
           var cropped = cropProcessor.PercentageCrop(image, px, py, pw, ph);
           var scaledCropped = scaleProcessor.SizeScaling(cropped, tw, th, distorted);
           return RotateAndColorAndFormat(scaledCropped, rotated, color, format);
        }
        public ActionResult PercentageCropPercentageScale(string id, int px, int py, int pw, int ph, int p, float rotated = 0, string color = "", string format = "jpg")
        {
            var cropped = cropProcessor.PercentageCrop(image, px, py, pw, ph);
            var scaledCropped = scaleProcessor.PercentageScaling(cropped, p);
            return RotateAndColorAndFormat(scaledCropped, rotated, color, format);
        }

        public ActionResult Full(string id, float rotated = 0, string color = "", string format = "jpg")
        {
            return RotateAndColorAndFormat(new Bitmap(image), rotated, color, format);
        }
        public ActionResult SizeCropNoScaling(string id, int x, int y, int w, int h, float rotated = 0, string color = "", string format = "jpg")
        {
            var cropped = cropProcessor.SizeCrop(image, x, y, w, h);
            return RotateAndColorAndFormat(cropped, rotated, color, format);
        }
        public ActionResult PercentageCropNoScaling(string id, int px, int py, int pw, int ph, float rotated = 0, string color = "", string format = "jpg")
        {
            var cropped = cropProcessor.PercentageCrop(image, px, py, pw, ph);
            return RotateAndColorAndFormat(cropped, rotated, color, format);
        }
        public ActionResult SizeScaleNoCropping(string id, int tw, int th, bool distorted = false, float rotated = 0, string color = "", string format = "jpg")
        {
            var scaled = scaleProcessor.SizeScaling(new Bitmap(image), tw, th, distorted);
            return RotateAndColorAndFormat(scaled, rotated, color, format);
        }
        public ActionResult PercentageScaleNoCropping(string id, int p, float rotated = 0, string color = "", string format = "jpg")
        {
            var scaled = scaleProcessor.PercentageScaling(new Bitmap(image), p);
            return RotateAndColorAndFormat(scaled, rotated, color, format);
        }
    }
}
