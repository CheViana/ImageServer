using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace ImageServer.Bussiness.FromDb
{
    public class PageTile
    {
        [Column(Order = 0), Key]
        public int BookId { get; set; }
        [Column(Order = 1), Key]
        public int PageId { get; set; }
        public int Heigth {get;set;}
        public int Width {get;set;}
        public int XOffset {get;set;}
        public int YOffset {get;set;}
        public byte[] TileContent { get; set; }
    }

    public class TilesContext : DbContext 
    {
        public DbSet<PageTile> Tiles { get; set; }
    }

}
