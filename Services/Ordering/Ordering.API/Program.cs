using Common.Logging;
using EventBus.Messages.Common;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.EventBusConsumer;
using Ordering.API.Extensions;
using Ordering.Application.Extensions;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Extensions;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy =>
    {
        policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
    });
});

//Serilog configuration
builder.Host.UseSerilog(Logging.ConfigureLogger);

builder.Services.AddControllers();

// Add API Versioning
builder.Services.AddApiVersioning(options =>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
});

builder.Services.AddApplicationServices();

builder.Services.AddInfraServices(builder.Configuration);

//Consumer class
builder.Services.AddScoped<BasketOrderingConsumer>();
builder.Services.AddScoped<BasketOrderingConsumerV2>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Ordering.API", Version = "v1" }); });

builder.Services.AddAutoMapper(typeof(Program).Assembly);

//Mass Transit
builder.Services.AddMassTransit(config =>
{
    //Mark this as consumer
    config.AddConsumer<BasketOrderingConsumer>();
    config.AddConsumer<BasketOrderingConsumerV2>();
    config.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host(builder.Configuration["EventBusSettings:HostAddress"]);
        //provide the queue name with consumer settings
        cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueue, c =>
        {
            c.ConfigureConsumer<BasketOrderingConsumer>(ctx);
        });
        //V2 Version
        cfg.ReceiveEndpoint(EventBusConstant.BasketCheckoutQueueV2, c =>
        {
            c.ConfigureConsumer<BasketOrderingConsumerV2>(ctx);
        });
    });
});
builder.Services.AddMassTransitHostedService();

var app = builder.Build();

app.MigrateDatabase<OrderContext>((context, services) =>
{
    var logger = services.GetService<ILogger<OrderContextSeed>>();
    OrderContextSeed.SeedAsync(context, logger).Wait();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
