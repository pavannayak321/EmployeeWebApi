using AutoMapper;
using EmployeeWebApi;
using EmployeeWebApi.Data;
using EmployeeWebApi.Model;
using Microsoft.EntityFrameworkCore;
using Azure.Identity;
using Azure.Core.Extensions;
using Azure.Security.KeyVault;
using Microsoft.Extensions.Azure;
using EmployeeWebApi.AzureKeyVaultUtility;

var builder = WebApplication.CreateBuilder(args);

//var keyVaultEndpoint = new Uri(Environment.GetEnvironmentVariable("VaultUri"));
//builder.Configuration.AddAzureKeyVault(keyVaultEndpoint, new DefaultAzureCredential());



// Add services to the container.
IMapper automapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(automapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddAzureClients(azureClientFactoryBuilder =>
{
    azureClientFactoryBuilder.AddSecretClient(builder.Configuration.GetSection("KeyVault"));
});

builder.Services.AddSingleton<IKeyVaultManager, KeyVaultManager>();

builder.Services.AddDbContext<EmployeeDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDbContextURL"));
});

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
if (app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHsts();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
