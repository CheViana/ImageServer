using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ImageServer.Bussiness.FromDb;

namespace LoadImagesToDb
{
    class Program
    {
        static void Main(string[] args)
        {
            var wr = new DbWriter();
            wr.PutImageToDb(123, 456, @"D:\wallpapers\stars.tif");

            var re = new DbReader();
            var im = re.GetImageFromDb("123-456", "0,0,100,100");
        }
    }
}
