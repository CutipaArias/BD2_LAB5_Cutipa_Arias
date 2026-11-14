using ClienteAPI.Data;
using Microsoft.EntityFrameworkCore;
using Prometheus;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BdClientesContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("ClienteDB")));

builder.Services.AddControllers()
    .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHealthChecks()
    .AddDbContextCheck<BdClientesContext>("database")
    .ForwardToPrometheus();

var app = builder.Build();
app.UseMetricServer();

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthorization();
app.UseHttpMetrics();
app.MapControllers();
app.MapHealthChecks("/health");

app.Run();
