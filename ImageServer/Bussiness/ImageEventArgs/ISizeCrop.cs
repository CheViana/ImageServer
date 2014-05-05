using System.Security.Cryptography.X509Certificates;

namespace ImageServer.Bussiness.ImageEventArgs
{
    interface ISizeCrop
    {
        int XOffset { get; set; }
        int YOffset { get; set; }
        int Width { get; set; }
        int Height { get; set; }
    }
}