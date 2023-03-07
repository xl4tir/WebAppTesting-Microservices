
using Microsoft.EntityFrameworkCore;
using WebAppTesting_cyber.Data;

using WebAppTesting_cyber.SyncDataServices.Http;

using WebAppTesting_cyber.AsyncDataServices;
using WebAppTesting_cyber.SyncDataServices.Grpc;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.



bool isProduction = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";


builder.Services.AddScoped<ITesting, TestingRepository>();


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddGrpc();
builder.Services.AddSingleton<IMessageBusClient, MessageBusClient>();

builder.Services.AddHttpClient<ITestingCompleteDataClient, HttpTestingCompleteDataClient>();



if (!isProduction)
{

    Console.WriteLine($"Is Dev?---->>> {isProduction}");
    Console.WriteLine("-----> Using SQL server DB");
    var connection = builder.Configuration.GetConnectionString("TestingConn");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(connection));
}
else
{
    Console.WriteLine($"Is Dev?---->>> {isProduction}");
    Console.WriteLine("-----> Using In memoRY DB");
    builder.Services.AddDbContext<AppDbContext>(options => options.UseInMemoryDatabase("InMem"));
}




builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();





var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = ((IApplicationBuilder)app).ApplicationServices.CreateScope())
{
    AppDbContext content = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    PrepDb.PrepPopulation(content, isProduction);
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<GrpcTestingService>();

    endpoints.MapGet("/protos/testing.proto", async context =>
    {
        await context.Response.WriteAsync(File.ReadAllText("Protos/testing.proto"));
    });
});

app.UseAuthorization();

app.MapControllers();


app.Run();




