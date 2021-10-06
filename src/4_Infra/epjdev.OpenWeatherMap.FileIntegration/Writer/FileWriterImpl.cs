using System.IO;
using System.Text;

namespace epjdev.OpenWeatherMap.FileIntegration.Writer
{
    internal class FileWriterImpl : IFileWriter
    {
        public void Write(string filePath, string content)
        {
            FileInfo fileInfo = new FileInfo(filePath);
            fileInfo.Directory.Create();

            byte[] bytes = Encoding.UTF8.GetBytes(content);

            using (FileStream outStream = new FileStream(fileInfo.FullName, FileMode.Open, FileAccess.Write, FileShare.ReadWrite))
            {
                outStream.Write(bytes, 0, bytes.Length);
            }
        }
    }
}
