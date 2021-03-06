﻿using System;
using Orleans;
using Orleans.Runtime.Configuration;

namespace TwinPair.Service
{
    using System;
    using System.Net;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;
    using Orleans;
    using Orleans.Configuration;
    using Orleans.Hosting;

    namespace OrleansSiloHost
    {
        public class Program
        {
            public static int Main(string[] args)
            {
                return RunMainAsync().Result;
            }

            private static async Task<int> RunMainAsync()
            {
                try
                {
                    var host = await StartSilo();
                    Console.WriteLine("Press Enter to terminate...");
                    Console.ReadLine();

                    await host.StopAsync();

                    return 0;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return 1;
                }
            }

            private static async Task<ISiloHost> StartSilo()
            {
                // define the cluster configuration
                var builder = new SiloHostBuilder()
                    .UseLocalhostClustering()
                    .Configure<ClusterOptions>(options =>
                    {
                        options.ClusterId = "dev";
                        options.ServiceId = "TwinPairsApp";
                    })
                    .ConfigureApplicationParts(parts => parts.AddApplicationPart(typeof(TwinPairs.Core.GameGrain).Assembly).WithReferences())                    
                    .Configure<EndpointOptions>(options => options.AdvertisedIPAddress = IPAddress.Loopback)
                    .ConfigureLogging(logging => logging.AddConsole());

                var host = builder.Build();
                await host.StartAsync();
                return host;
            }
        }
    }
}
