using System.IO;
using System.Text;

namespace epjdev.OpenWeatherMap.FileIntegration.Reader
{
    internal class FileReaderImpl : IFileReader
    {
        public string Read(string filePath)
        {
            StringBuilder contentBuilder = new StringBuilder();

            using (var readStream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (StreamReader reader = new StreamReader(readStream))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                    contentBuilder.AppendLine(line);
            }

            return contentBuilder.ToString();
        }
    }
}
