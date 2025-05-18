using Amazon.Runtime;
using Amazon.S3;
using Api.Middleware;
using Application.Hubs;
using Application.Interfaces;
using Application.Mapper;
using Application.ScheduledTask;
using Application.Services;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MySql.Data.MySqlClient;
using System.Data;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Register IDbConnection with a concrete implementation
builder.Services.AddTransient<IDbConnection>(sp =>
    new MySqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddScoped(typeof(IBaseRepository<, >), typeof(BaseRepository<, >));
builder.Services.AddScoped(typeof(IBaseService<,>), typeof(BaseService<,>));
builder.Services.AddScoped(typeof(IUserRepository), typeof(UserRepository));
builder.Services.AddScoped(typeof(IUserService), typeof(UserService));
builder.Services.AddScoped(typeof(IMediaRepository), typeof(MediaRepository));
builder.Services.AddScoped(typeof(IMediaService), typeof(MediaService));
builder.Services.AddScoped(typeof(IPostRepository), typeof(PostRepository));
builder.Services.AddScoped(typeof(IPostService), typeof(PostService));
builder.Services.AddScoped(typeof(IAuthService), typeof(AuthService));
builder.Services.AddScoped(typeof(INotificationRepository), typeof(NotificationRepository));
builder.Services.AddScoped(typeof(INotificationService), typeof(NotificationService));
builder.Services.AddScoped(typeof(IContractService), typeof(ContractService));
builder.Services.AddScoped(typeof(IContractRepository), typeof(ContractRepository));
builder.Services.AddScoped(typeof(IClientReviewMcService), typeof(ClientReviewMcService));
builder.Services.AddScoped(typeof(IClientReviewMcRepository), typeof(ClientReviewMcRepository));
builder.Services.AddScoped(typeof(IMcReviewClientService), typeof(McReviewClientService));
builder.Services.AddScoped(typeof(IMcReviewClientRepository), typeof(McReviewClientRepository));

// Register AutoMapper by scanning the current assembly (or specify exactly)
builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

//signalR
builder.Services.AddSignalR();

// scheduled task
builder.Services.AddHostedService<ReviewBackgroundService>();


// Add CORS services.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader());
});

builder.Services.AddControllers();
builder.Services.AddSingleton<IAmazonS3>(sp =>
{
    var credentials = new StoredProfileAWSCredentials("FindMC"); // ví dụ "default" hoặc tên bạn đặt trong AWS Toolkit
    var config = new AmazonS3Config
    {
        RegionEndpoint = Amazon.RegionEndpoint.APSoutheast1 // Thay bằng region của bạn
    };
    return new AmazonS3Client(credentials, config);
});
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddSingleton(sp =>
{
    var s3Client = sp.GetRequiredService<IAmazonS3>();
    var bucketName = builder.Configuration["AWS:BucketName"];
    return new S3Service(s3Client, bucketName);
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//add authentication
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
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    })
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"];
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"];
    });
builder.Services.AddScoped<ITokenService, TokenService>();

var app = builder.Build();

// Use the CORS policy.
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom middleware for exception handling
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapHub<NotificationHub>("/notificationHub");

app.Run();
