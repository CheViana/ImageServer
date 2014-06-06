using System;
using System.Linq;

namespace ImagesToDb
{
    public class DbReader
    {
        private DbTools tools = new DbTools();
        
        public Tuple<PageTile,string> GetImageFromDb(string id, string region)
        {
            var bookId = tools.GetBookIdFromRequestId(id);
            var pageId = tools.GetPageIdFromRequestId(id);
            using (var context = new TilesContext())
            {
                var tiles = context.Tiles.Where(t => t.BookId == bookId && t.PageId == pageId);
                return !tiles.Any() ? null : tools.LookForClosestTile(tiles, region);
            }
        }
    }
}