using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Buns.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Timer = System.Timers.Timer;

namespace Buns.Web.BackgroundServices
{
    public class DeleteInconsistentBunsBackgroundService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<DeleteInconsistentBunsBackgroundService> _logger;
        
        private readonly SemaphoreSlim _semaphore;
        private readonly Timer _timer;

        private const int SemaphoreWaitMs = 60 * 1000;
        private const int TimerDelayMs = 5 * 60 * 1000;
        private const int TakeLimit = 1000;

        public DeleteInconsistentBunsBackgroundService(IServiceScopeFactory scopeFactory, ILogger<DeleteInconsistentBunsBackgroundService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            
            _semaphore = new SemaphoreSlim(1, 1);
            _timer = new Timer(TimerDelayMs);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            OnTimerTick(null, null);
            
            _timer.Elapsed += OnTimerTick;
            _timer.AutoReset = true;
            _timer.Enabled = true;
            
            return Task.CompletedTask;
        }

        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            using var scope = _scopeFactory.CreateScope();
            var applicationDbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            
            _semaphore.Wait(SemaphoreWaitMs);
            
            //fixme: не транслируется в sqlite 
            /*var buns = _applicationDbContext.Buns
                    .Where(x => !x.IsAbleToSell)
                    .Take(TakeLimit)
                    .ToList();*/
            var buns = applicationDbContext.Buns
                .Take(TakeLimit)
                .ToList()
                .Where(x => !x.IsAbleToSell)
                .ToList();
            _logger.LogInformation($"Deleting {buns.Count} inconsistent buns");
            applicationDbContext.RemoveRange(buns);
            applicationDbContext.SaveChanges();
            
            _semaphore.Release();
        }

        public override void Dispose()
        {
            _semaphore.Dispose();
            _timer.Dispose();
            base.Dispose();
        }
    }
}