using System.Drawing.Imaging;
using Emgu.CV.Structure;
using DirectShowLib;
using Emgu.CV;

namespace MultiWebcamCapture
{
    public partial class Program
    {
        private const int MaxCameras = 4;

        static async Task Main()
        {
            await CaptureWebcamScreenshotsAsync();
        }

        public static async Task CaptureWebcamScreenshotsAsync()
        {
            var availableCameras = GetAvailableCameras();
            var captureTasks = new List<Task>();

            for (int i = 0; i < Math.Min(MaxCameras, availableCameras.Length); i++)
            {
                int cameraIndex = i;
                captureTasks.Add(CaptureFromCameraAsync(cameraIndex));
            }

            await Task.WhenAll(captureTasks);
        }

        private static DsDevice[] GetAvailableCameras()
        {
            return DsDevice.GetDevicesOfCat(FilterCategory.VideoInputDevice);
        }

        private static async Task CaptureFromCameraAsync(int cameraIndex)
        {
            await Task.Run(() =>
            {
                using var capture = new VideoCapture(cameraIndex);
                if (!capture.IsOpened)
                {
                    return;
                }

                using var frame = capture.QueryFrame();
                if (frame == null)
                {
                    return;
                }

                SaveFrameAsImage(frame, cameraIndex);
            });
        }

        private static void SaveFrameAsImage(Mat frame, int cameraIndex)
        {
            using var bitmap = frame.ToImage<Bgr, byte>().ToBitmap();
            string savePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Webcam Screenshot {cameraIndex + 1}.png");
            ImageCodecInfo pngEncoder = GetEncoder(ImageFormat.Png);
            if (pngEncoder != null)
            {
                bitmap.Save(savePath, pngEncoder, null);
            }
            else
            {
                throw new InvalidOperationException("PNG encoder not found.");
            }
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}