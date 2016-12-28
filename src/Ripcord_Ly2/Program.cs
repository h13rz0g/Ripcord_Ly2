using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using System.Threading;
using Ripcord_Ly2.Hosting.Extensions;
using Ripcord_Ly2.Hosting;

namespace Ripcord_Ly2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("http://localhost:9000/")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            using (host)
            {
                using (var cts=new CancellationTokenSource())
                {
                    host.Run((service)=>
                    {
                        var hosts = new RipcordHost(
                            service,
                            System.Console.In,
                            System.Console.Out,
                            args);
                        hosts
                            .RunAsync()
                            .Wait();

                        cts.Cancel();
                    },cts.Token,"Shutdwon Ctrl+C");
                }
            }

            
        }
    }
}
