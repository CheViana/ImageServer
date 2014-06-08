using System;
using System.Linq;

namespace ImageServer.Bussiness.FromDb
{
    public class DbReader
    {
        private DbTools tools = new DbTools();
        
        public DBReaderResult GetImageFromDb(string id, string region, string scaling)
        {
            var bookId = tools.GetBookIdFromRequestId(id);
            var pageId = tools.GetPageIdFromRequestId(id);
            using (var context = new TilesContext())
            {
                var tiles = context.Tiles.Where(t => t.BookId == bookId && t.PageId == pageId);
                if (tiles.Any())
                {
                    var result = new DBReaderResult() {RegionNew = region, ScalingNew = scaling};
                    PageTileInfo pti;
                    if (scaling != "256,0" && scaling != "512,0" && !tiles.Any(t=>t.IsScaled))
                    {
                        var tuple = tools.LookForClosestTile(tiles, region);
                        pti = tuple.Item1;
                        result.RegionNew = tuple.Item2;
                    }
                    else
                    {
                        var parts = scaling.Split(',');
                        var scaleTo = int.Parse(parts[0]);
                        var tuple = tools.LookForClosestTileScaled(tiles, region, scaleTo);
                        pti = tuple.Item1;
                        result.RegionNew = tuple.Item2;
                        result.ScalingNew = tuple.Item3;
                    }
                    var tileContent = context.TilesContent.First(tc => tc.InfoID == pti.ID);
                    result.Content = tileContent;
                    return result;
                }
                return null;
            }
        }
    }

    public class DBReaderResult
    {
        public string RegionNew { get; set; }
        public string ScalingNew { get; set; }
        public PageTileContent Content { get; set; }
    }
}