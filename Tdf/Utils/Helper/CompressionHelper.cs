using System.IO;

namespace Tdf.Utils.Helper
{
    /// <summary>
    /// 利用DotNetZip进行快速压缩
    /// </summary>
    public class CompressionHelper
    {
        public static byte[] DeflateByte(byte[] str)
        {
            if (str == null)
            {
                return null;
            }
            using (var output = new MemoryStream())
            {
                using (var compressor = new Ionic.Zlib.GZipStream(
                output, Ionic.Zlib.CompressionMode.Compress,
                Ionic.Zlib.CompressionLevel.BestSpeed))
                {
                    compressor.Write(str, 0, str.Length);
                }
                return output.ToArray();
            }

        }
    }
}
