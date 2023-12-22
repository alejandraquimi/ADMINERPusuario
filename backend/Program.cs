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


builder.Services.AddDbContext<BackendDbContext>();
 
builder.Services.AddSingleton<ScriptExecutor>();



var app = builder.Build();

string? DevelopmentIp = app.Configuration["DevelopmentIp"];
string? ProductionDomain = app.Configuration["ProductionDomain"];
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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


var scriptExecutor = app.Services.GetRequiredService<ScriptExecutor>();

await scriptExecutor.ExecuteScript();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
