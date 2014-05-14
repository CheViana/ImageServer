using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImagesToDb
{
    public class Program
    {
        private static readonly Image image = Image.FromFile(@"C:\photoes\wallpapers\mar-14-hello-march-cheep-cheep-cal-1920x1080.jpg");

        public static void Main(string[] args)
        {
            //using (var memStream = new MemoryStream())
            //{
            //    image.Save(memStream, ImageFormat.Jpeg);
            //    var bytes = memStream.ToArray();
            //    using (var context = new TilesContext())
            //    {
            //        var tile = new PageTile()
            //        {
            //            PageId = 1,
            //            BookId = 1,
            //            Heigth = 1080,
            //            Width = 1920,
            //            XOffset = 0,
            //            YOffset = 0,
            //            TileContent = bytes
            //        };
            //        context.Tiles.Add(tile);
            //        context.SaveChanges();
            //    }
            //}
            //using (var context = new TilesContext())
            //{
            //    var tile = context.Tiles.FirstOrDefault();
            //    using (var memStream = new MemoryStream())
            //    {
            //        var bytes = tile.TileContent;
            //        memStream.Write(bytes, 0, bytes.Length);
            //        var image = Image.FromStream(memStream);
            //        image.Save(@"C:\photoes\new.jpg", ImageFormat.Jpeg);
            //    }
            //}
            
        }
    }
}
