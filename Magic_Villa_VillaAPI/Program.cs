

using Magic_Villa_VillaAPI;
using Magic_Villa_VillaAPI.Data;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Magic_Villa_VillaAPI.Repository.IRepositpory;
using Magic_Villa_VillaAPI.Repository;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddAutoMapper(typeof(MappingConfig));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IVillaRepository,VillaRepository>();
builder.Services.AddScoped<IVillaNumberRepository,VillaNumberRepository>();


builder.Services.AddDbContext<ApplicatonDbContext>(options => options
.UseSqlServer(builder.Configuration.GetConnectionString("DefaultSQLConnection")));

//builder.Services.AddSingleton<ILogging,Logging>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
