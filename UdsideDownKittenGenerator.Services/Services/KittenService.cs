using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace UdsideDownKittenGenerator.Services
{
    public class KittenService : IKittenService
    {
        private readonly HttpClient _httpClient;

        public KittenService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Stream> GetKitten()
        {
            using var response = await _httpClient.GetAsync("cat");
            if (!response.IsSuccessStatusCode)
            {
                var exMessage = await response.Content.ReadAsStringAsync();
                throw new Exception(exMessage);
            }

            using var stream = await response.Content.ReadAsStreamAsync();
            await using var ms = await response.Content.ReadAsStreamAsync();

            Bitmap bitmap = new Bitmap(ms);
            bitmap.RotateFlip(RotateFlipType.Rotate180FlipX);

            var outputStream = new MemoryStream();
            bitmap.Save(outputStream, ImageFormat.Jpeg);
            outputStream.Seek(0, SeekOrigin.Begin);

            return outputStream;
        }
    }
}
