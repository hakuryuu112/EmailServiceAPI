using EmailServiceAPI.DBContext;
using EmailServiceAPI.Models;
using EmailServiceAPI.Provider;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<SMTPSettingModel>(builder.Configuration.GetSection("SmtpSettings"));

// Note : install MongoDB.Driver.Core, Microsoft.EntityFrameworkCore, Microsoft.EntityFrameworkCore.SqlServer
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseSqlServer(connectionString));

// Register MainProvider
builder.Services.AddTransient<MainProvider>();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder => builder
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmailServiceAPI V1");
        c.RoutePrefix = "swagger"; // Pastikan jalurnya benar
    });
}


app.UseCors("AllowAll");
app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
