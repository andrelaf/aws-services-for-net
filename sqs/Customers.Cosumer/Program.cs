using Amazon.SQS;
using Customers.Cosumer;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.Configure<QueueSettings>(hostContext.Configuration.GetSection(QueueSettings.Key));
        services.AddSingleton<IAmazonSQS, AmazonSQSClient>();
        services.AddHostedService<QueueConsumerService>();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
    })
    .Build();

host.Run();
