using Contracts;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Repository;
using Service;
using Service.Contracts;
using WalletApp.Data;
using WalletApp.Data.Seed;
using WalletApp.Extensions;
using WalletApp.Presentation.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<WalletDbContext>(opt =>
{
    opt.UseInMemoryDatabase("WalletAppDb");
});
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddControllers()
    .AddApplicationPart(typeof(WalletApp.Presentation.AssemblyReference).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<AuthenticationFilter>();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddTransient<IAsyncSeeder, DbSeeder>();
builder.Services.Configure<KestrelServerOptions>(opt =>
{
    opt.AllowSynchronousIO = true;
});
var app = builder.Build();


app.ConfigureExceptionHandler();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader();
});

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();
