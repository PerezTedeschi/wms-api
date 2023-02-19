using System.IO;

namespace wms_api.Helpers
{
    public class FileHelper
    {
        public async static Task<byte[]> GetFileByteArray(IFormFile file)
        {
            long length = file.Length;
            if (length < 0)
            {
                return Array.Empty<byte>();
            }

            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
    }
}
