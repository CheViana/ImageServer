using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace ImagesToDb
{
    public class DbWriter
    {
        
        private DbTools tools = new DbTools();
        private readonly CroppingProcessor cropProcessor = new CroppingProcessor();

        public void PutImageToDb(int bookId, int pageId, string path)
        {
            using (var image = Image.FromFile(path))
            {
                using (var context = new TilesContext())
                {
                    var tiles = tools.AnalyzePageForTileCreation(image.Width,image.Height);
                    foreach (PageTile pt in tiles)
                    {
                        pt.BookId = bookId;
                        pt.PageId = pageId;
                        var tile = cropProcessor.SizeCrop(image, pt.XOffset, pt.YOffset, pt.Width, pt.Heigth);
                        using (var memStream = new MemoryStream())
                        {
                            tile.Save(memStream, ImageFormat.Jpeg);
                            var bytes = memStream.ToArray();
                            pt.TileContent = bytes;
                            context.Tiles.Add(pt);
                        }
                    }
                    context.SaveChangesAsync();
                }
            }
        }
    }
}