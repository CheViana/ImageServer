namespace ImageServer.Bussiness.ImageEventArgs
{
    public class SizeCropSizeScaleArgs: SizeCropArgs, ISizeScale
    {
        public int TargetWidth { get; set; }
        public int TargetHeight { get; set; }
        public bool Distorted { get; set; }
    }
}