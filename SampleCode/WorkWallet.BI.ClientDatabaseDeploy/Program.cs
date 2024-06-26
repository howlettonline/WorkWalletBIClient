﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WorkWallet.BI.ClientDatabaseDeploy;
using WorkWallet.BI.ClientDatabaseDeploy.Services;

var hostBuilder = Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((builder) =>
                {
#if DEBUG
                    builder.AddUserSecrets<Program>();
#endif
                })
                .ConfigureServices((context, services) =>
                {
                    services
                        .Configure<AppSettings>(context.Configuration.GetSection(nameof(AppSettings)))
                        .AddHostedService<DeployDatabaseService>();
                });

await hostBuilder.RunConsoleAsync();
