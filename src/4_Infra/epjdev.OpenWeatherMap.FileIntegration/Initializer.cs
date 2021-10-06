using epjdev.OpenWeatherMap.FileIntegration.Reader;
using epjdev.OpenWeatherMap.FileIntegration.Writer;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace epjdev.OpenWeatherMap.FileHelper
{
    public static class Initializer
    {
        public static void StartFileIntegration(this IServiceCollection services)
        {
            services
                .AddSingleton<IFileReader, FileReaderImpl>()
                .AddSingleton<IFileWriter, FileWriterImpl>();
        }
    }
}
