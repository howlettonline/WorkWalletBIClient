﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkWallet.BI.ClientCore.Interfaces.Services;
using WorkWallet.BI.ClientCore.Options;
using WorkWallet.BI.ClientServices;

try
{
    using IHost host = Host.CreateDefaultBuilder(args)
        .ConfigureServices((context, services) =>
        {
            services
                .Configure<ProcessorServiceOptions>(context.Configuration.GetSection("ClientOptions"))
                .AddProcessorService()
                .AddSQLService(options =>
                {
                    options.SqlDbConnectionString = context.Configuration.GetConnectionString("ClientDb")!;
                });
        })
        .Build();

    IServiceProvider serviceProvider = host.Services;

    await serviceProvider.GetRequiredService<IProcessorService>().RunAsync();

    return 0;
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
    return 1;
}
