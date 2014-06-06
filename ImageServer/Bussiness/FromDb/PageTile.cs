using System.Data.Entity;

namespace ImageServer.Bussiness.FromDb
{
    public class PageTile
    {
        public int ID { get; set; }
        public int BookId { get; set; }
        public int PageId { get; set; }
        public int Heigth {get;set;}
        public int Width {get;set;}
        public int XOffset {get;set;}
        public int YOffset {get;set;}
        public bool IsFull { get; set; }
    }

    public class PageTileContent
    {
        public int ID { get; set; }
        public int InfoID { get; set; }
        public byte[] TileContent { get; set; }
    }

    public class TilesContext : DbContext 
    {
        public DbSet<PageTile> Tiles { get; set; }
        public DbSet<PageTileContent> TilesContent { get; set; }
    }

}
