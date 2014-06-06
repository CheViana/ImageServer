using System;
using System.Linq;

namespace ImagesToDb
{
    public class DbReader
    {
        private DbTools tools = new DbTools();
        
        public Tuple<PageTileContent,string> GetImageFromDb(string id, string region)
        {
            var bookId = tools.GetBookIdFromRequestId(id);
            var pageId = tools.GetPageIdFromRequestId(id);
            using (var context = new TilesContext())
            {
                var tiles = context.Tiles.Where(t => t.BookId == bookId && t.PageId == pageId);
                if (tiles.Any())
                {
                    var tuple = tools.LookForClosestTile(tiles, region);
                    var tileContent = context.TilesContent.First(tc => tc.InfoID == tuple.Item1.ID);
                    return new Tuple<PageTileContent, string>(tileContent,tuple.Item2);
                }
                return null;
            }
        }
    }
}