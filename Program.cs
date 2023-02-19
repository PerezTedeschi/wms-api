using FluentValidation;
using Microsoft.EntityFrameworkCore;
using wms_api;
using wms_api.DTO;
using wms_api.Repositories;
using wms_api.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaulConnection")
));

// Register repository
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

// Register validator
builder.Services.AddScoped<IValidator<CreateWarehouseDTO>, CreateWarehouseDTOValidator>();

// Register automapper profiles
builder.Services.AddAutoMapper(typeof(Program));

// 
builder.Services.AddCors(options =>
{
    var appUrl = builder.Configuration.GetValue<string>("AppUrl");
    if (appUrl != null)
        options.AddDefaultPolicy(builder =>
            builder.WithOrigins(appUrl).AllowAnyHeader().WithExposedHeaders("Content-Disposition").AllowAnyMethod()
        );
});

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

app.UseCors();

app.Run();
