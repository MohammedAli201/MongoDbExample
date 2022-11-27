using System.Text;
using AspNetCore.Identity.MongoDbCore.Extensions;
using AspNetCore.Identity.MongoDbCore.Infrastructure;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson.Serialization.Serializers;
using MongoDbExample.Features.Users;
using MongoDbExample.Models;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MongoDbExample.Features.Courses;
using MongoDbExample.Features.Students;

var builder = WebApplication.CreateBuilder(args);

MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(new GuidSerializer(MongoDB.Bson.BsonType.String));
MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(new DateTimeSerializer(MongoDB.Bson.BsonType.String));
MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(MongoDB.Bson.BsonType.String));
var mongoDbSettings = builder.Configuration.GetSection(nameof(SchoolDatabaseSettings)).Get<SchoolDatabaseSettings>();
// var dbConfig = builder.Configuration.GetSection(("SchoolDatabaseSettings"));
var mongoDbIdentityConfig = new MongoDbIdentityConfiguration
{
    MongoDbSettings = new MongoDbSettings
    {

        ConnectionString = mongoDbSettings.ConnectionString,
        DatabaseName = mongoDbSettings.DatabaseName


    },

    IdentityOptionsAction = option =>
    {
        option.Password.RequireDigit = false;
        option.Password.RequiredLength = 8;
        option.Password.RequireNonAlphanumeric = true;
        option.Password.RequireLowercase = false;


        // lockout
        option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
        option.Lockout.MaxFailedAccessAttempts = 5;
        option.User.RequireUniqueEmail = true;

    }

};
// Add services to the container.
builder.Services.ConfigureMongoDbIdentity<ApplicationUser, ApplicationRole, Guid>(mongoDbIdentityConfig)
        .AddUserManager<UserManager<ApplicationUser>>()
        .AddSignInManager<SignInManager<ApplicationUser>>()
        .AddRoleManager<RoleManager<ApplicationRole>>()
        .AddDefaultTokenProviders();


builder.Services.AddAuthentication(x =>
           {
               x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
               x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
           })
           .AddJwtBearer(x =>
           {
               x.RequireHttpsMetadata = false;
               x.SaveToken = true;
               x.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("/uVxQ~NyP}w0A=$<FQ;4;`rXI\\'9]7wb<(yB")),
                   ValidateIssuer = false,
                   ValidateAudience = false
               };
           });

builder.Services.AddControllers();

builder.Services.Configure<SchoolDatabaseSettings>(
    builder.Configuration.GetSection(nameof(SchoolDatabaseSettings))
);


builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());




builder.Services.AddScoped<IUserRepository, UserRepository>();


builder.Services.AddScoped<ICoursesRepository, CoursesRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

// We are mapping the values in the appsetting.json file to the configuration model. then we are using the addSingleton method to provide the configuration model to the project

builder.Services.AddSingleton<ISchoolDatabaseSettings>(provider =>
        provider.GetRequiredService<IOptions<SchoolDatabaseSettings>>().Value);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

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
