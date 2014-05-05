using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageServer.Bussiness.ImageEventArgs
{
    interface IPercentageScale
    {
        int TargetScalePercent { get; set; }
    }
}
