using QRCoder;
using System.Drawing.Imaging;
using System.Drawing;

namespace TicketLibrary.Services
{
    public class generateQRCode
    {
        public static string GenerateQRCodeBase64(string qrCodeText)
        {
            if (!string.IsNullOrEmpty(qrCodeText))
            {
                try
                {

                    QRCodeGenerator qrCodeGenerate = new QRCodeGenerator();
                    QRCodeData qrCodeData = qrCodeGenerate.CreateQrCode(qrCodeText, QRCodeGenerator.ECCLevel.Q);
                    QRCode qrCode = new QRCode(qrCodeData);

                    string appDataDirectory = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    string filePath = Path.Combine(appDataDirectory, "QREmail.png");

                    using (Bitmap qrBitMap = qrCode.GetGraphic(20))
                    {
                        qrBitMap.Save(filePath, ImageFormat.Png);
                        qrBitMap.Dispose();
                    }

                    return filePath;
                }
                catch (Exception ex) 
                {
                    throw;
                }
            }

            return "";
        }
    }
}
