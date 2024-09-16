using LabCircuitBreaker.Pages;
using LabCircuitBreaker.Services.Implementation;
using LabCircuitBreaker.Services.Interface;
using Microsoft.Extensions.Http;
using Polly;
using Polly.Extensions.Http;
using System.Net;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var circuitBreakerPolicy = GetCircuitBreakerPolicy();

//Se agrega la inyección de dependencia y la configuración del Circuit Breaker
builder.Services.AddHttpClient<IRandomUserService, RandomUserService>("ResilientClientRandomUser")
        .AddPolicyHandler(RetryPolicy()) //Policy first
        .AddPolicyHandler(circuitBreakerPolicy);

builder.Services.AddTransient<IRandomUserService, RandomUserService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    //app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();


static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .CircuitBreakerAsync(5, TimeSpan.FromSeconds(10),
        onBreak: (ex, @break) =>
        {
            IndexModel.StateCircuitBreaker = "Is Break";
            Task.Run(async () =>
            {
                await Task.Delay(10000);
                IndexModel.StateCircuitBreaker = "Is Half Open";
            });
        },
        onReset: () => IndexModel.StateCircuitBreaker = "Is Open",
        onHalfOpen: () => { } );
}

static IAsyncPolicy<HttpResponseMessage> RetryPolicy()
    => Policy<HttpResponseMessage>
    .Handle<HttpRequestException>()
    .RetryAsync(1);
