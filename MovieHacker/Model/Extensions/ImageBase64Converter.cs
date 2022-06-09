using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;
using System.Drawing.Imaging;

namespace MovieHacker.Model.Extensions
{
    public class ImageBase64Converter
    {
        public static Image Base64ToImage(string base64String)
        {

            // Convert Base64 String to byte[]
            byte[] imageBytes = Convert.FromBase64String(base64String);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            // Convert byte[] to Image
            ms.Write(imageBytes, 0, imageBytes.Length);
            Image image = Image.FromStream(ms, true);
            return image;
        }
        public static string ImageToBase64(Image image, ImageFormat format)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                // Convert Image to byte[]
                image.Save(ms, format);
                byte[] imageBytes = ms.ToArray();

                // Convert byte[] to Base64 String
                string base64String = Convert.ToBase64String(imageBytes);
                return base64String;
            }
        }
        public static string? ImageToBase64(string? path)
        {
            if(path == null) return null;
            return ImageToBase64(Image.FromFile(path), ImageFormat.Jpeg);
        }
        public static string ImageToBase64(Image img)
        {
            return ImageToBase64(img, ImageFormat.Jpeg);
        }

        public static BitmapImage? ToXAMLView(string base64String)
        {
            if (base64String == null) return null;
            byte[] binaryData = Convert.FromBase64String(base64String);

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();
            bi.StreamSource = new MemoryStream(binaryData);
            bi.EndInit();
            return bi;
        }
    }
}
