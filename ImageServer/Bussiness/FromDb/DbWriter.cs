using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImageServer.Bussiness.FromDb
{
    public class DbWriter
    {
        
        private DbTools tools = new DbTools();
        private readonly CroppingProcessor cropProcessor = new CroppingProcessor();
        private readonly ScalingProcessor scaleProcessor = new ScalingProcessor();


        public void PutImageToDb(int bookId, int pageId, string path)
        {
            using (var image = Image.FromFile(path))
            {
                using (var context = new TilesContext())
                {
                    var tiles = tools.GenerateTilesInfo(image.Width,image.Height);
                    var addedTiles = new List<PageTileInfo>();
                    foreach (PageTileInfo pt in tiles)
                    {
                        pt.BookId = bookId;
                        pt.PageId = pageId;
                        var added = context.Tiles.Add(pt);
                        addedTiles.Add(added);
                    }
                    context.SaveChanges();
                    foreach (var pt in addedTiles)
                    {
                        Image tileImage = cropProcessor.SizeCrop(image, pt.XOffset, pt.YOffset, pt.Width, pt.Heigth);
                        if (pt.IsScaled)
                        {
                            var scaled = scaleProcessor.SizeScaling((Bitmap)tileImage, pt.DestWidth, 0, false);
                            tileImage = scaled;
                        }
                        using (var memStream = new MemoryStream())
                        {
                            tileImage.Save(memStream, ImageFormat.Jpeg);
                            var bytes = memStream.ToArray();
                            var ptc = new PageTileContent() { InfoID = pt.ID, TileContent = bytes };
                            context.TilesContent.Add(ptc);
                        }
                    }
                    context.SaveChanges();
                }
                
            }
        }
    }
}