using JelloBackend.Data;
using JelloBackend.Hubs;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPermission", policy =>
    {
        policy.AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:3000")
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = "oidc";
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect("oidc", options =>
    {
        options.Authority = "https://localhost:5001";
        options.ClientId = "webAppClient";
        options.ClientSecret = "-cdNq:6S:9H2E6n";
        options.ResponseType = "code";
        options.CallbackPath = "/signin-oidc";
        options.SaveTokens = true;
        options.RequireHttpsMetadata = false;
    }).AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:5001";
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false
        };
        options.RequireHttpsMetadata = false;
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var path = context.HttpContext.Request.Path;
                if (path.StartsWithSegments("/learningHub"))
                {
                    // Attempt to get a token from a query sting used by WebSocket
                    var accessToken = context.Request.Query["access_token"];

                    // If not present, extract the token from Authorization header
                    if (string.IsNullOrWhiteSpace(accessToken))
                    {
                        accessToken = context.Request.Headers["Authorization"]
                            .ToString()
                            .Replace("Bearer ", "");
                    }

                    context.Token = accessToken;
                }

                return Task.CompletedTask;
            }
        };
    });;
JwtSecurityTokenHandler.DefaultMapInboundClaims = false;
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DatabaseContext")));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options=>options.AddSignalRSwaggerGen());

var app = builder.Build();
app.UseCors("ClientPermission");
app.MapHub<BoardControlHub>("/hubs/BoardControlHub");
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();