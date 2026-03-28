namespace OrderFlow.Worker;

public class Worker() : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            throw new NotImplementedException("Worker is not implemented");
        }
    }
}
