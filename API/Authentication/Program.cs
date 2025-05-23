using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Https.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Authentication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseKestrel()
                .UseStartup<Startup>()
                .ConfigureKestrel((context, options) => {
                   /*  options.Listen(IPAddress.Loopback, 5003, listenOptions => {
                         listenOptions.UseHttps("Certificates/certificate.pfx", "password1");
                    });*/
                      //  options.ListenAnyIP(5003 );
                        options.ListenAnyIP(5003, listenOptions => {
                         //   listenOptions.UseHttps("Certificates/certificate.pfx", "password1");
                        });
                });
    }
}
