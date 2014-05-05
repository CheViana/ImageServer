using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageServer.Bussiness.ImageEventArgs
{
    interface ISizeScale
    {
        int TargetWidth { get; set; }
        int TargetHeight { get; set; }
        bool Distorted { get; set; }
    }
}