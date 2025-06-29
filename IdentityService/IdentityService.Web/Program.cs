// ===================== //
// 📘 OPENIDDICT IDENTITY SERVICE DOCUMENTATION STYLE CODE //
// ===================== //

#region 🚀 Program Entry – Application Bootstrap & Configuration

/*
 * Sets up:
 *   🔹 Identity + OpenIddict
 *   🔹 Swagger (OAuth2 support)
 *   🔹 FluentValidation + AutoMapper
 *   🔹 Middleware: HTTPS, CORS, Auth, RateLimiting
 *   🔹 DbContext + Dependency Injection
 *   🔹 Default OpenIddict Client Seeding
 */

using FluentValidation;
using IdentityService.Web.Config;
using IdentityService.Web.Entities;
using IdentityService.Web.Features.Mappers;
using IdentityService.Web.Features.Validators;
using IdentityService.Web.Middlewares;
using IdentityService.Web.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using OpenIddict.Validation.AspNetCore;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

#endregion

#region 🧩 Services & DI Configuration

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpContextAccessor();
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();

builder.Services.AddScoped<ICredentialService<AppUser>, CredentialService>();

#endregion

#region 🗃️ DbContext & Identity Configuration

builder.Services.AddDbContext<AppIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    options.User.RequireUniqueEmail = true;
})
.AddEntityFrameworkStores<AppIdentityDbContext>()
.AddDefaultTokenProviders();

#endregion

#region 🔐 OpenIddict Configuration

builder.Services.AddOpenIddict()
    .AddCore(options =>
    {
        options.UseEntityFrameworkCore()
               .UseDbContext<AppIdentityDbContext>();
    })
    .AddServer(options =>
    {
        options.AllowPasswordFlow();
        options.AllowRefreshTokenFlow();

        options.SetTokenEndpointUris("/connect/token");
        options.SetAccessTokenLifetime(TimeSpan.FromMinutes(15));
        options.SetRefreshTokenLifetime(TimeSpan.FromDays(7));
        options.UseReferenceRefreshTokens();
        options.RequireProofKeyForCodeExchange();

        options.AddEphemeralEncryptionKey();
        options.AddEphemeralSigningKey();

        options.UseAspNetCore()
               .EnableTokenEndpointPassthrough();
    });

builder.Services.AddAuthentication(OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme);
builder.Services.AddAuthorization();

#endregion

#region 🔧 CORS Configuration

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

#endregion

#region 📘 Swagger Configuration (OAuth2-enabled)

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Identity Service API", Version = "v1" });

    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.OAuth2,
        Flows = new OpenApiOAuthFlows
        {
            Password = new OpenApiOAuthFlow
            {
                TokenUrl = new Uri("/connect/token", UriKind.Relative),
                Scopes = new Dictionary<string, string>
                {
                    { "api", "Access to API" },
                    { "openid", "User Identifier" },
                    { "email", "Email Info" },
                    { "profile", "Profile Info" },
                    { "offline_access", "Refresh Token support" }
                }
            }
        }
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "oauth2"
                }
            },
            SwaggerDefaults.OAuthScopes
        }
    });
});

#endregion

#region 🚀 App Middleware Pipeline

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Identity Service API V1");
        c.OAuthClientId("authservice-client");
        c.OAuthClientSecret("C78A11D0-4D8A-4B61-BB20-F6D937C6B7F4");
    });
}

app.UseHsts();
app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthentication();
app.UseAuthorization();

app.UseRateLimiter();

app.UseWhen(
    context => context.User.Identity?.IsAuthenticated == true,
    appBuilder =>
    {
        appBuilder.UseMiddleware<RateLimitingMiddleware>(new FixedWindowRateLimiterOptions
        {
            PermitLimit = 30,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
        });
    });

app.UseWhen(
    context => context.User.Identity?.IsAuthenticated != true,
    appBuilder =>
    {
        appBuilder.UseMiddleware<RateLimitingMiddleware>(new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromMinutes(1),
            QueueLimit = 0,
            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
        });
    });

app.MapControllers();

#endregion

#region 🌱 Seed OpenIddict Client (on startup)

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await OpenIddictSeeder.SeedAsync(services).ConfigureAwait(false);
}

#endregion

await app.RunAsync().ConfigureAwait(false);
