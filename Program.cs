using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;
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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
));

// Register repository
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();

// Register validator
builder.Services.AddScoped<IValidator<CreateWarehouseDTO>, CreateWarehouseDTOValidator>();

// Register automapper profiles
builder.Services.AddAutoMapper(typeof(Program));

// Add Identity
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(option =>
    {
        var keyJwt = builder.Configuration.GetValue<string>("KeyJwt");
        if (keyJwt != null)
            option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(keyJwt)),
                ClockSkew = TimeSpan.Zero
            };
    });

// Add Role policy
builder.Services.AddAuthorization(options =>
    {
        options.AddPolicy("IsManager", policy => policy.RequireClaim(ClaimTypes.Role, "manager"));
    });

// Add cors
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

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
