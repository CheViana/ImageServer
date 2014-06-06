using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using ImageServer.Bussiness;

namespace ImageServer.Controllers
{
    public class ImageController : Controller
    {
        private readonly CroppingProcessor cropProcessor = new CroppingProcessor();
        private readonly ScalingProcessor scaleProcessor = new ScalingProcessor();
        private readonly RotatedProcessor rotatedProcessor = new RotatedProcessor();
        private readonly ColorProcessor colorProcessor = new ColorProcessor();
        private readonly FormatProcessor formatProcessor = new FormatProcessor();
        

        private ActionResult RotateAndColorAndFormat(Image im, float rotate, string colorformat)
        {
            string color = "native";
            string format = "jpg";
            if (colorformat.Contains("."))
            {
                var parts = colorformat.Split('.');
                if (!string.IsNullOrWhiteSpace(parts[0])) color = parts[0];
                if (!string.IsNullOrWhiteSpace(parts[1])) format = parts[1];
            }
            else
            {
                if (!string.IsNullOrWhiteSpace(colorformat)) color = colorformat;
            }

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

        private Bitmap Crope(Image im, string region)
        {
            //full ?
            if (region == "full") return new Bitmap(im);

            // percentage or coordinates cropp ?
            var parts = region.Split(new[] { ',', ':' });
            // region=pct:10,10,80,80
            if (region.StartsWith("pct:"))
            {
                return cropProcessor.PercentageCrop(im, int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]), int.Parse(parts[4]));                
            }
            // region=0,10,100,200
            return cropProcessor.SizeCrop(im, int.Parse(parts[0]), int.Parse(parts[1]), int.Parse(parts[2]), int.Parse(parts[3]));
        }

        private Image Scale(Bitmap bitmap, string size)
        {
            //full ?
            if (size == "full") return bitmap;

            // percentage or coordinates scale ?                     
            var parts = size.Split(new[] { ',', ':' });
            // size=pct:50
            if (size.StartsWith("pct:"))
            {
                return scaleProcessor.PercentageScaling(bitmap, int.Parse(parts[1]));
            }
            // size=50,
            if (string.IsNullOrEmpty(parts[1]))
            {
                return scaleProcessor.SizeScaling(bitmap, int.Parse(parts[0]), 0, false);
            }
            // size=,50
            if (string.IsNullOrEmpty(parts[0]))
            {
                return scaleProcessor.SizeScaling(bitmap, 0, int.Parse(parts[1]), false);
            }
            // size=50,50
            return scaleProcessor.SizeScaling(bitmap, int.Parse(parts[0]), int.Parse(parts[1]), true);            
        }

        public ActionResult GetImageTile(string id, string region, string size, float rotation =0, string colorformat = "native.jpg")
        {
            using (var image = Image.FromFile(Server.MapPath("/image/stars.tif")))
            {
                var croppedImage = Crope(image, region);
                var scaledImage = Scale(croppedImage, size);
                var rotatedColorFormat = RotateAndColorAndFormat(scaledImage, rotation, colorformat);
                return rotatedColorFormat;
            }
        }

        public JsonResult Info(string id)
        {
            return Json(
                new
                {
                    @context = "http://library.stanford.edu/iiif/image-api/1.1/context.json",
                    @id = "http://localhost:2344/images/id",
                    width = 3885,
                    height = 3904,
                    scale_factors = new[] { 1, 2, 4, 8, 16, 32, 64 },
                    tile_width = 512,
                    tile_height = 512,
                    formats = new[] { "jpg", "png" },
                    qualities = new[] { "native"},
                    profile = "http://library.stanford.edu/iiif/image-api/1.1/compliance.html#level2"
                }, JsonRequestBehavior.AllowGet);
        }
    }
}
