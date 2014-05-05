using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using ImageServer.Bussiness;

namespace ImageServer.Controllers
{
    public class ImageController : Controller
    {
        private readonly ImageProcessor processor = new ImageProcessor();
        private readonly Image image = Image.FromFile(@"D:\wallpapers\july-10-photographydock-nocal-1920x1200.tif");

        private ActionResult ImageActionResult(Image im)
        {
            using (var memStream = new MemoryStream())
            {
                im.Save(memStream, ImageFormat.Jpeg);
                var bytes = memStream.ToArray();
                return base.File(bytes, "image/jpeg");
            }
        }

        public ActionResult SizeCropSizeScale(string id, int x, int y, int w, int h, int tw, int th, bool distorted)
        {
            var cropped = processor.SizeCrop(image, x, y, w, h);
            var scaledCropped = processor.SizeScaling(cropped, tw, th, distorted);
            return ImageActionResult(scaledCropped);
        }
        public ActionResult SizeCropPercentageScale(string id, int x, int y, int w, int h, int p)
        {
            var cropped = processor.SizeCrop(image, x, y, w, h);
            var scaledCropped = processor.PercentageScaling(cropped, p);
            return ImageActionResult(scaledCropped);
        }
        public ActionResult PercentageCropSizeScale(string id, int px, int py, int pw, int ph, int tw, int th, bool distorted)
        {
           var cropped = processor.PercentageCrop(image, px, py, pw, ph);
            var scaledCropped = processor.SizeScaling(cropped, tw, th, distorted);
            return ImageActionResult(scaledCropped);
        }
        public ActionResult PercentageCropPercentageScale(string id, int px, int py, int pw, int ph, int p)
        {
            var cropped = processor.PercentageCrop(image, px, py, pw, ph);
            var scaledCropped = processor.PercentageScaling(cropped, p);
            return ImageActionResult(scaledCropped);
        }

        public ActionResult Full(string id)
        {
            return ImageActionResult(new Bitmap(image));
        }
        public ActionResult SizeCropNoScaling(string id, int x, int y, int w, int h)
        {
            var cropped = processor.SizeCrop(image, x, y, w, h);
            return ImageActionResult(cropped);
        }
        public ActionResult PercentageCropNoScaling(string id, int px, int py, int pw, int ph)
        {
            var cropped = processor.PercentageCrop(image, px, py, pw, ph);
            return ImageActionResult(cropped);
        }
        public ActionResult SizeScaleNoCropping(string id, int tw, int th, bool distorted)
        {
            var scaled= processor.SizeScaling(new Bitmap(image), tw, th, distorted);
            return ImageActionResult(scaled);
        }
        public ActionResult PercentageScaleNoCropping(string id, int p)
        {
            var scaled = processor.PercentageScaling(new Bitmap(image), p);
            return ImageActionResult(scaled);
        }
    }
}
