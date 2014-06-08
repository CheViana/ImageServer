using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ImageServer.Bussiness.FromDb
{
    public class PageTileInfo
    {
        [Key]
        public int ID { get; set; }
        public int BookId { get; set; }
        public int PageId { get; set; }
        public int Heigth {get;set;}
        public int Width {get;set;}
        public int XOffset {get;set;}
        public int YOffset {get;set;}
        public bool IsFull { get; set; }

        public bool IsScaled { get; set; }
        public int DestWidth { get; set; }
    }

    public class PageTileContent
    {
        [Key]
        public int InfoID { get; set; }
        public byte[] TileContent { get; set; }
    }

    public class TilesContext : DbContext 
    {
        public TilesContext()
            : base("TilesContext")
        {
        }

        public DbSet<PageTileInfo> Tiles { get; set; }
        public DbSet<PageTileContent> TilesContent { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

    public class TilesInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<TilesContext>
    {
        protected override void Seed(TilesContext context)
        {
            //var wr = new DbWriter();
            //wr.PutImageToDb(120, 453, @"C:\photoes\page.png");
        }
    }

}
