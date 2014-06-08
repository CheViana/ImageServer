using System;
using ImageServer.Bussiness.FromDb;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ImageServer.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var tools = new DbTools();
            var tiles =
                tools.GenerateTilesInfo(3328, 4491)
                    .OrderByDescending(pti => pti.XOffset)
                    .ThenByDescending(pti => pti.YOffset);
            using(var file  =new System.IO.StreamWriter("c:\\test.txt"))
            {
                foreach (var pageTileInfo in tiles)
                {
                    file.WriteLine(pageTileInfo.XOffset+" "+pageTileInfo.YOffset+" "+pageTileInfo.Width+" "+pageTileInfo.Heigth);
                }
            }


        }
    }
}
