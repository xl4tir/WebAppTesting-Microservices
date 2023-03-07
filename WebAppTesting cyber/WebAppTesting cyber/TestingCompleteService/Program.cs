using Microsoft.EntityFrameworkCore;
using TestingCompleteService.AsyncDataServices;
using TestingCompleteService.Data;
using TestingCompleteService.EventProcessing;
using TestingCompleteService.SyncDataServices.Grpc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMem"));
builder.Services.AddScoped<ITestingCompleteRepository, TestingCompleteRepository>();

builder.Services.AddSingleton<IEventProcessor, EventProcessor>();

builder.Services.AddHostedService<MessageBusSubscriber>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ITestingDataClient, TestingDataClient>();



//Add Mapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();

PrepDb.PrepPopulation(app);

app.UseAuthorization();

app.MapControllers();

app.Run();



