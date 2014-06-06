using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Xml.XPath;

namespace ImagesToDb
{
    public class DbTools
    {
        private int smallestRegion = 512;

        private static int GetStringPart(string word, int index)
        {
            var parts = word.Split('-');
            return int.Parse(parts[index]);
        }

        public int GetBookIdFromRequestId(string id)
        {
            return GetStringPart(id, 0);
        }
        public int GetPageIdFromRequestId(string id)
        {
            return GetStringPart(id, 1);
        }
        //filling in x-yoffset,width-height only
        public IEnumerable<PageTile> AnalyzePageForTileCreation(int width, int height)
        {
            var tiles = new List<PageTile>
            {
                //full tile
                new PageTile() {IsFull = true, Heigth = height, Width = width, XOffset = 0, YOffset = 0}
            };
            AnalyzeRec(tiles,width,height,0,0);
            return tiles;
        }

        private void AnalyzeRec(ICollection<PageTile> pageTiles, int width, int height,int x, int y)
        {
            if (width > smallestRegion & height > smallestRegion)
            {
                var width1 = width/2;
                var width2 = width - width1;
                var height1 = height/2;
                var height2 = height - height1;
                pageTiles.Add(new PageTile() { IsFull = false, Width = width1, Heigth = height1, XOffset = x, YOffset = y });
                pageTiles.Add(new PageTile() { IsFull = false, Width = width2, Heigth = height1, XOffset = x+width1, YOffset = y });
                pageTiles.Add(new PageTile() { IsFull = false, Width = width1, Heigth = height2, XOffset = x, YOffset = y + height1 });
                pageTiles.Add(new PageTile() { IsFull = false, Width = width2, Heigth = height2, XOffset = x + width1, YOffset = y + height1 });
                AnalyzeRec(pageTiles, width1, height1, x, y);
                AnalyzeRec(pageTiles, width2, height1, x + width1, y);
                AnalyzeRec(pageTiles, width1, height2, x, y + height1);
                AnalyzeRec(pageTiles, width2, height2, x + width1, y + height1);
            }
        }

        public Tuple<PageTile,string> LookForClosestTile(IEnumerable<PageTile> allTiles, string region)
        {
            if (region == "full") return new Tuple<PageTile, string>(allTiles.First(t => t.IsFull),region);

            var tiles = allTiles.ToArray();
            if (tiles.Count() == 1) return new Tuple<PageTile, string>(tiles.ElementAt(0),region);

            var fullTile = tiles.First(t => t.IsFull);

            //looking for tiles that our region fits in
            var fittedTiles = tiles.Where(pageTile => RegionFitsInTile(pageTile, region, fullTile.Width, fullTile.Heigth)).ToList();

            //and then looking for smallest of them
            var smallestDims = fittedTiles.Min(t => t.Width * t.Heigth);
            var smallestTile = fittedTiles.First(t => t.Width * t.Heigth == smallestDims);
            var newRegion = UpdateRegionRequestParam(smallestTile, region, fullTile.Width, fullTile.Heigth);
            return new Tuple<PageTile, string>(smallestTile, newRegion);
        }

        private bool RegionFitsInTile(PageTile pageTile, string region, int width, int height)
        {
            int x, y, h, w;
            GetDimsInPx(region, width, height, out x, out y, out w, out h);
            return (x >= pageTile.XOffset && x < pageTile.XOffset + pageTile.Width &&
                    y >= pageTile.YOffset && y < pageTile.YOffset + pageTile.Heigth &&
                    x+w <= pageTile.XOffset + pageTile.Width &&
                    y + h <= pageTile.YOffset + pageTile.Heigth);
        }

        private static void GetDimsInPx(string region, int width, int height, out int x, out int y, out int w, out int h)
        {
            var regionParts = region.Split(new[] {',', ':'});
            if (region.StartsWith("pct:"))
            {
                var xperc = int.Parse(regionParts[1]);
                var yperc = int.Parse(regionParts[2]);
                var wperc = int.Parse(regionParts[3]);
                var hperc = int.Parse(regionParts[4]);
                x = CroppingProcessor.OffsetFromPercToPx(width, xperc);
                y = CroppingProcessor.OffsetFromPercToPx(height, yperc);
                w = CroppingProcessor.DimentionFromPercToPx(width, wperc);
                h = CroppingProcessor.DimentionFromPercToPx(height, hperc);
            }
            else
            {
                x = int.Parse(regionParts[0]);
                y = int.Parse(regionParts[1]);
                w = int.Parse(regionParts[2]);
                h = int.Parse(regionParts[3]);
            }
        }


        private string UpdateRegionRequestParam(PageTile tile, string oldRegion, int width, int height)
        {
            if (oldRegion == "full") return "full";
            int x, y, h, w;
            GetDimsInPx(oldRegion, width, height, out x, out y, out w, out h);

            //old w and h remains, but x and y are going to get new values
            return (x - tile.XOffset) + "," + (y - tile.YOffset) + "," + w + "," + h;
        }
    }
}