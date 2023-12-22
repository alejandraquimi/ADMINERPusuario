global using Microsoft.EntityFrameworkCore;
global using backend.Models;
using backend.Services;

using backend.Controllers;

using Microsoft.Extensions.Configuration;

using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using System.Net;
using System.Text.Json.Serialization;

using System.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.CodeAnalysis.Scripting;
var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json", optional: false);

builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c=>{
    c.SwaggerDoc("v1",new OpenApiInfo{Title="software Lion",Version="v1"});
    c.AddSecurityDefinition("Bearer",new OpenApiSecurityScheme
    {
        Description="Jwt Authorization",
        Name="Authorization",
        In=ParameterLocation.Header,
        Type=SecuritySchemeType.ApiKey,
        Scheme="Bearer"
    }
    );
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference=new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            Array.Empty<string>()
        }
    }
    );
    }

);
builder.Services.AddScoped<LoginService>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options=>
    {
        options.TokenValidationParameters=new TokenValidationParameters
        {
           ValidateIssuerSigningKey=true,
           IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]??"")) ,
           ValidateIssuer=false,
           ValidateAudience=false

        };
    });
builder.Services.AddSingleton<IPasswordHasher, BCryptPasswordHasher>();


builder.Services.AddDbContext<BackendDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ScriptExecutor>();
   

var app = builder.Build();

using var scope = app.Services.CreateScope();
var serviceProvider = scope.ServiceProvider;
var scriptExecutor = serviceProvider.GetRequiredService<ScriptExecutor>();

await scriptExecutor.ExecuteScript();

string? DevelopmentIp = app.Configuration["DevelopmentIp"];
string? ProductionDomain = app.Configuration["ProductionDomain"];
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName.Equals("Development_Remote"))
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName.Equals("Development_Remote"))
{
    app.UseCors(policy => {
        policy.WithOrigins($"http://{DevelopmentIp}", "http://localhost:3000")
           .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
} 
else
{
    app.UseCors(policy => {
        policy.WithOrigins($"http://{ProductionDomain}", $"https://{ProductionDomain}")
           .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
}


app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
app.Run();