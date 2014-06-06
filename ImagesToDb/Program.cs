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

            //var tile1 = new PageTile() { IsFull = false, Width = 971, Heigth = 976, XOffset = 0, YOffset = 0 };
            //var tile2 = new PageTile() { IsFull = false, Width = 971, Heigth = 976, XOffset = 971, YOffset = 0 };
            //var tile3 = new PageTile() { IsFull = false, Width = 1942, Heigth = 1952, XOffset = 0, YOffset = 0 };

            //var tiles = new[]
            //{
            //    new PageTile() {IsFull = true, Width = 3885, Heigth = 3904, XOffset = 0, YOffset = 0},

            //    new PageTile() {IsFull = false, Width = 1942, Heigth = 1952, XOffset = 0, YOffset = 0 },
            //    new PageTile() {IsFull = false, Width = 1943, Heigth = 1952, XOffset = 1942, YOffset = 0},
            //    new PageTile() {IsFull = false, Width = 1942, Heigth = 1952, XOffset = 0, YOffset = 1952},
            //    new PageTile() {IsFull = false, Width = 1943, Heigth = 1952, XOffset = 1942, YOffset = 1952},

            //    new PageTile() {IsFull = false, Width = 971, Heigth = 976, XOffset = 0, YOffset = 0 },
            //    new PageTile() {IsFull = false, Width = 971, Heigth = 976, XOffset = 971, YOffset = 0 },
            //    new PageTile() {IsFull = false, Width = 971, Heigth = 976, XOffset = 0, YOffset = 976},
            //    new PageTile() {IsFull = false, Width = 971, Heigth = 976, XOffset = 971, YOffset = 976}
            //};



            //string region1 = "0,0,25,25";
            //string region2 = "975,10,25,25";
            //string region3 = "950,0,100,100";


            //var tools = new DbTools();
            //var tile1real = tools.LookForClosestTile(tiles, region1);
            //var tile2real = tools.LookForClosestTile(tiles, region2);
            //var tile3real = tools.LookForClosestTile(tiles, region3);

            var wr = new DbWriter();
            wr.PutImageToDb(125, 457, @"D:\Projects\ImageServer\ImageServer\image\stars.tif");

            var re = new DbReader();
            var im = re.GetImageFromDb("125-457", "0,0,100,100");

            using (var memStream = new MemoryStream())
            {
                var bytes = im.Item1.TileContent;
                memStream.Write(bytes, 0, bytes.Length);
                var image = Image.FromStream(memStream);
                image.Save(@"D:\wallpapers\new.jpg", ImageFormat.Jpeg);
            }

            
        }
    }
}
