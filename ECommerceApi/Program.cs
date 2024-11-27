using Ecommerce.DAL.Repositories.Implementation.Authentication;
using Ecommerce.DAL.Repositories.Interfaces.Authentication;
using Ecommerce.Service.Common;
using Ecommerce.Service.Configuration;
using Ecommerce.Service.Implementations.Email;
using Ecommerce.Service.Implementations.Products;
using Ecommerce.Service.Implementations.Token;
using Ecommerce.Service.Interfaces.Authentication;
using Ecommerce.Service.Interfaces.Email;
using Ecommerce.Service.Interfaces.Products;
using Ecommerce.Service.Interfaces.Token;
using ECommerce.DAL;
using ECommerce.DAL.Repositories.Implementation.Products;
using ECommerce.DAL.Repositories.Interfaces.Products;
using ECommerceApi.Middlewares;
using ECommerceApi.Services.Implementations.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ECommerceDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUserService, UserService> ();
builder.Services.AddScoped<IUserRepository, UserRepository> ();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddScoped<ITokenService>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    return new TokenService(config["JWTSecretKey"]);
});

var key = builder.Configuration["JWTSecretKey"];
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key))
    };
});

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));
builder.Services.AddTransient<IEmailService, EmailService>();

var pathToCertificate = Path.Combine(Directory.GetCurrentDirectory(), "localhost.pfx");
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(7152, listenOptions =>
    {
        listenOptions.UseHttps(pathToCertificate,"12345");
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAllOrigins");

app.UseAuthorization();

app.MapControllers();

app.Run();