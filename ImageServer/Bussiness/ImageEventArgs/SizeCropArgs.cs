using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageServer.Bussiness.ImageEventArgs
{
    public class SizeCropArgs: BaseImageArgs, ISizeCrop
    {
        public int XOffset { get; set; }
        public int YOffset { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}