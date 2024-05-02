using System;
using CoreWCF;
using CoreWCF.Configuration;
using CoreWCF.Channels;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Contract;
using CoreWCF.Description;

namespace NetCoreServer
{
    public class Startup
    {
        public const int NETTCP_PORT = 8089;
        public const string HOST_IN_WSDL = "localhost";

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddServiceModelServices()
                    .AddServiceModelMetadata()
                    .AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseServiceModel(builder =>
            {
                // Add the Echo Service
                builder.AddService<EchoService>(serviceOptions =>
                { })
                .AddServiceEndpoint<EchoService, IEchoService>(new NetTcpBinding(), $"net.tcp://localhost:{NETTCP_PORT}/EchoService/netTcp");


                // Configure WSDL to be available over http & https
                var serviceMetadataBehavior = app.ApplicationServices.GetRequiredService<CoreWCF.Description.ServiceMetadataBehavior>();
                serviceMetadataBehavior.HttpGetEnabled = serviceMetadataBehavior.HttpsGetEnabled = true;
            });
        }
    }

}
