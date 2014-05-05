namespace ImageServer.Bussiness.ImageEventArgs
{
    public class SizeCropPercentageScaleArgs: SizeCropArgs,IPercentageScale
    {
       public int TargetScalePercent { get; set; }
    }
}