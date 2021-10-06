namespace epjdev.OpenWeatherMap.FileIntegration.Writer
{
    public interface IFileWriter
    {
        void Write(string filePath, string content);
    }
}
