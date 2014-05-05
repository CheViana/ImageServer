namespace ImageServer.Bussiness.ImageEventArgs
{
    public abstract class BaseImageArgs
    {
        public float Rotated { get; set; }
        public string Color { get; set; }
        public string Format { get; set; }
    }
}