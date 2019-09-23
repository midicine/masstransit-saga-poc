using GreenPipes;
using MassTransit;
using MassTransit.AspNetCoreIntegration;
using MassTransit.RabbitMqTransport.Topology;
using MassTransit.Saga;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using POC.Saga.Application.Infrastructure;
using POC.Saga.Domain.Commands;
using POC.Saga.Infrastructure;
using System;
using ConfirmInvitation = POC.Saga.Application.Infrastructure.ConfirmInvitation;

namespace POC.Saga.Application
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
            => services
                .AddScoped<IEventDispatcher, EventDispatcher>()
                .AddMassTransit(
                    provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                    {
                        var bus = cfg.Host(Configuration.GetValue<Uri>("MessageBus:Url"), h =>
                        {
                            h.Username(Configuration["MessageBus:Username"]);
                            h.Password(Configuration["MessageBus:Password"]);
                        });

                        cfg.ReceiveEndpoint(bus, nameof(ConfirmInvitationStateMachine), e =>
                        {
                            e.PrefetchCount = 8;
                            e.StateMachineSaga(new ConfirmInvitationStateMachine(),
                                new InMemorySagaRepository<ConfirmInvitation>());
                            e.UseMessageRetry(r => r.Incremental(3,
                                    TimeSpan.FromSeconds(5),
                                    TimeSpan.FromSeconds(5))
                                .Ignore<ArgumentNullException>());
                        });

                        cfg.ConfigureEndpoints(provider);

                        cfg.UseMessageRetry(r => r.Incremental(3,
                                TimeSpan.FromSeconds(5),
                                TimeSpan.FromSeconds(5))
                            .Ignore<ArgumentNullException>());

                        cfg.PublishTopology.BrokerTopologyOptions = PublishBrokerTopologyOptions.MaintainHierarchy;

                        MapCommand<CreateAccount>();
                        MapCommand<CreateUser>();
                        MapCommand<ConfirmInvitation>();
                        MapCommand<DeleteAccount>();
                        MapCommand<DeleteInvitation>();
                    }),
                    cfg => cfg.AddConsumers(GetType().Assembly)
                )
                .AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Latest);

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();

            app.UseMvc();
        }

        private void MapCommand<TCommand>() where TCommand : class
        {
            var formatter = new MassTransit.RabbitMqTransport.RabbitMqMessageNameFormatter();
            var endpointName = formatter.GetMessageName(typeof(TCommand));
            EndpointConvention.Map<TCommand>(new Uri($"{Configuration.GetValue<string>("MessageBus:Url")}/{endpointName}"));
        }
    }
}
