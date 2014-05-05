using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ImageServer.Bussiness.ImageEventArgs
{
    interface IPercentageCrop
    {
        int XOffsetPercent { get; set; }
        int YOffsetPercent { get; set; }
        int WidthPercent { get; set; }
        int HeightPercent { get; set; }
    }
}