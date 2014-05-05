using System;
using System.Drawing;

namespace ImageServer.Bussiness
{
    public class ScalingProcessor
    {
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