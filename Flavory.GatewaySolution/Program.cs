using Flavory.GatewaySolution.Extensions;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.AddAppAuthetication();  // To add support authentication for ocelot

builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);  // configuration to configure ocelot.json
builder.Services.AddOcelot(builder.Configuration); // It should be BEFORE builder.Build();

var app = builder.Build();


app.MapGet("/", () => "Hello World!");
app.UseOcelot().GetAwaiter().GetResult();  // to call ocelot because it is asynchronous we have to set it to awaiter!
app.Run();
