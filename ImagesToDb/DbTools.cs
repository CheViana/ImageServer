using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace ImagesToDb
{
    public class DbTools
    {
        public int GetBookIdFromRequestId(string id)
        {
            var parts = id.Split('-');
            return int.Parse(parts[0]);
        }
        public int GetPageIdFromRequestId(string id)
        {
            var parts = id.Split('-');
            return int.Parse(parts[1]);
        }
        //filling in x-yoffset,width-height only
        public PageTile[] AnalyzePageForTileCreation(int width, int height)
        {
            return new PageTile[1];
        }

        public PageTile LookForClosestTile(IEnumerable<PageTile> allTiles)
        {
            if (allTiles.Count() == 1) return allTiles.ElementAt(0);

            return new PageTile();
        }

        public string UpdateRegionRequestParam(PageTile tile, string oldRegion)
        {
            return "full";
        }
    }
}