
using BusinessLogic;
using LDSApi;
using LDSApi.Middleware.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.AspNetCore.Server.Kestrel.Core;  // ? ADD THIS using
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.DbContext;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Threading.RateLimiting;
using UserModel;
using Utility;
using ConfigurationManager = LDSApi.ConfigManager;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("OtpPolicy", opt =>
    {
        opt.PermitLimit = 3;
        opt.Window = TimeSpan.FromMinutes(10);
        opt.QueueLimit = 0;
    });
});


builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("AuthPolicy", httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
            factory: _ => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 5,
                Window = TimeSpan.FromMinutes(1),
                QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                QueueLimit = 0
            }));
});
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.AddEndpointsApiExplorer();


builder.Services.Configure<KestrelServerOptions>(options => {
    options.Limits.MaxRequestBodySize = 104857600; // 100 MB
});

builder.Services.Configure<FormOptions>(options => {
    options.MultipartBodyLengthLimit = 104857600; // 100 MB
    options.ValueLengthLimit = int.MaxValue;
});

builder.Services.AddHttpContextAccessor();

//builder.Services.Configure<SmsSettings>(builder.Configuration.GetSection("SmsSettings"));

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Land Digitization System API", Version = "v.1.0" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter only JWT token without Bearer prefix"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

builder.Services.Configure<EncryptionSettings>(
    builder.Configuration.GetSection("EncryptionSettings"));

CommonVariables.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.Configure<DomainSettings>(
    builder.Configuration.GetSection("DomainSettings"));

builder.Services.AddHttpClient();

builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();
builder.Services.AddScoped<ILoginRepository, LoginRepository>();

builder.Services.AddScoped<IMasterBusiness, MasterBusiness>();
builder.Services.AddScoped<IMasterRepository, MasterRepository>();




builder.Services.AddScoped<ISignupBusiness, SignupBusiness>();
builder.Services.AddScoped<IAdminBusiness, AdminBusiness>();
builder.Services.AddScoped<IUserBusiness, UserBusiness>();
builder.Services.AddScoped<IManageUserBusiness, ManageUserBusiness>();
builder.Services.AddScoped<EncryptDecryptHelper>();


//builder.Services.AddScoped<IS3FileService, S3FileService>();

builder.Services.AddScoped<JwtUserHelper>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("WebAppPolicy", policy =>
    {
        policy.WithOrigins(
                "https://localhost:7099",
                "https://webtestingapp.runasp.net")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>
{
    options.IncludeErrorDetails = true;

    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = false,

        ClockSkew = TimeSpan.FromMinutes(5),

        ValidIssuer = ConfigManager.AppSetting["JWT:ValidIssuer"],
        ValidAudience = ConfigManager.AppSetting["JWT:ValidAudience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(ConfigManager.AppSetting["JWT:Secret"]))
    };

    options.Events = new JwtBearerEvents
    {
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("JWT ERROR: " + context.Exception.Message);

            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }

            return Task.CompletedTask;
        },

        //OnTokenValidated = context =>
        //{
        //    Console.WriteLine("JWT TOKEN VALIDATED SUCCESSFULLY");
        //    return Task.CompletedTask;
        //}
        OnTokenValidated = async context =>
        {
            var jti = context.Principal?
                .FindFirst(JwtRegisteredClaimNames.Jti)?.Value
                ?? context.Principal?.FindFirst("jti")?.Value;

            if (string.IsNullOrEmpty(jti))
            {
                context.Fail("Invalid token.");
                return;
            }

            var loginBusiness = context.HttpContext.RequestServices
                .GetRequiredService<ILoginBusiness>();

            bool isRevoked = await loginBusiness.IsTokenRevoked(jti);

            if (isRevoked)
            {
                context.Fail("Token has been revoked.");
            }
        }
    };
});
var app = builder.Build();

app.UseGlobalExceptionMiddleware();

// ?? Forwarded Headers (must be first — Nginx proxy) ??
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
});

// ?? Swagger ???????????????????????????????????????????
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "LDS API v1");
    c.RoutePrefix = "swagger";
});

// ?? Middleware Pipeline ???????????????????????????????
app.UseStaticFiles();
// app.UseHttpsRedirection(); ? REMOVED — Nginx handles HTTPS

app.Use(async (context, next) =>
{
    context.Response.Headers["X-Frame-Options"] = "DENY";
    context.Response.Headers["X-Content-Type-Options"] = "nosniff";
    context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
    context.Response.Headers["Permissions-Policy"] = "camera=(), microphone=(), geolocation=()";
    context.Response.Headers["Content-Security-Policy"] =
        "default-src 'self'; " +
        "script-src 'self'; " +
        "style-src 'self' 'unsafe-inline'; " +
        "img-src 'self' data: https:; " +
        "font-src 'self' data:; " +
        "connect-src 'self'; " +
        "frame-ancestors 'none'; " +
        "base-uri 'self'; " +
        "form-action 'self';";

    context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate, max-age=0";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "0";

    if (context.Request.IsHttps)
    {
        context.Response.Headers["Strict-Transport-Security"] =
            "max-age=31536000; includeSubDomains";
    }

    await next();
});
app.UseRouting();
app.UseCors("WebAppPolicy");
app.UseRateLimiter();
app.UseAuthentication(); // ? ADDED — required for JWT
app.UseAuthorization();
app.MapControllers();
app.Run();
