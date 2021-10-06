using epjdev.OpenWeatherMap.HostApplication;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace epjdev.OpenWeatherMap.Host
{
    class Program
    {
        static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            Console.CancelKeyPress += (sender, eArgs) =>
            {
                _quitEvent.Set();
                eArgs.Cancel = true;
            };

            IServiceProvider serviceProvider = Initializer.Start();
            IHostApplication application = serviceProvider.GetService<IHostApplication>();

            CancellationTokenSource tokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = tokenSource.Token;

            Task task = Task.Run(application.Execute, cancellationToken);

            _quitEvent.WaitOne();

            tokenSource.Cancel();
            tokenSource.Dispose();

            application.Dispose();
        }
    }
}
