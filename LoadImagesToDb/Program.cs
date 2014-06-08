using ImageServer.Bussiness.FromDb;

namespace LoadImagesToDb
{
    class Program
    {
        static void Main(string[] args)
        {
            var wr = new DbWriter();
            wr.PutImageToDb(120, 453, @"C:\photoes\page.png");
        }
    }
}
