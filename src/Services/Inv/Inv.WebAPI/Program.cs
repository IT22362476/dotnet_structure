using Audit.Core;
using Inv.Application.Extensions;
using Inv.Persistence.Extensions;
using Inv.WebAPI.Utility;
using JwtTokenAuthentication.Permission;
using Microsoft.AspNetCore.Authorization;
using JwtTokenAuthentication.Extensions;
using Microsoft.OpenApi.Models;
using JwtTokenAuthentication.Services;
using Asp.Versioning;
using System.Reflection;
using Inv.Infrastructure.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationLayer();

builder.Services.AddJwtAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddInfrastructureLayer();
builder.Services.AddPersistenceLayer(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

// Add services to the container.
builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        // Or use .Preserve to preserve object references
        // options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Preserve;
    });
builder.Services.AddEndpointsApiExplorer();
// CORS Access Config
string[] allowedOrigins = builder.Environment.IsDevelopment()
    ? new[] { "http://localhost:4200","http://localhost:5279", "https://localhost:7033", "https://localhost:5002","http://http://123.231.17.207:8002", "https://123.231.17.207:8004"
        ,"https://123.231.17.207:8006", "https://123.231.17.207:8007", "https://123.231.17.207:8008",
        "https://123.231.17.207:8009", "https://123.231.17.207:8010", "https://123.231.17.207:8011", }
    : new[] { "https://yourproductiondomain.com","http://http://123.231.17.207:8002", "https://123.231.17.207:8004","http://localhost:5279"
        ,"https://123.231.17.207:8006", "https://123.231.17.207:8007", "https://123.231.17.207:8008",
        "https://123.231.17.207:8009", "https://123.231.17.207:8010", "https://123.231.17.207:8011",
    };
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
          builder => builder.WithOrigins(allowedOrigins)// single domain .WithOrigins("https://www.yogihosting.com")
                            .AllowAnyMethod()  // multiple domain .WithOrigins(new string[] { "https://www.yogihosting.com", "https://example1.com", "https://example2.com" })
                            .AllowAnyHeader().SetIsOriginAllowed(origin => true)); // allow any origin;
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new HeaderApiVersionReader("X-Api-Version"));
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'V";
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Dayaratne ERP Inv API",
        Description = "A Dayaratne ERP Inv API Version 1",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Denuwan Sathsara",
            Email = string.Empty,
            Url = new Uri("https://www.facebook.com/denuwan.sathsara"),
        },

        License = new OpenApiLicense
        {
            Name = "Use under LICX",
            Url = new Uri("https://example.com/license"),
        }
    });
    // add basic instead of Bearer
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,// add basic instead of Bearer SecuritySchemeType.Http
        Scheme = "Bearer",        // add basic instead of Bearer
        BearerFormat = "JWT",          // add basic instead of Bearer remove this line
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
               {
             {
               new OpenApiSecurityScheme
               {
                   Reference = new OpenApiReference
                         {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer" // add basic instead of Bearer
                         }
               },
                 new string[] {}
             }
               });
    //Set the comments path for the Swagger JSON and UI.
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddSingleton<IAuthorizationPolicyProvider, CustomAuthorizationPolicyProvider>();
builder.Services.AddScoped<IAuthorizationHandler, MultiplePermissionAuthorizationHandler>();
builder.Services.AddScoped<TokenService, TokenService>();

//https://github.com/thepirat000/Audit.NET/blob/master/src/Audit.EntityFramework/README.md#install

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.UseHttpsRedirection();
app.UseRouting();              //do not chage the order between UseAuthentication and UseAuthorization()
app.UseCors("CorsPolicy");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
