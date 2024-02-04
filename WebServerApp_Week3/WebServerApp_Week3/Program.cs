using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WebServerApp_Week3.Middleware;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using TokenService;
using WebServerApp_Week3.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Assignment_2.Data;

namespace WebServerApp_Week3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            string connectionName = string.Empty;

            if (args.Length == 0) { }
            else { }

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString(connectionName));
            });

            builder.Services.AddSingleton<IValidator>();
            //builder.Services.AddScoped<IValidator, Validator>();
            //builder.Services.AddTransient<IValidator, Validator>();
            //builder.Services.AddTransient<IValidator>(validator => new Validator(true));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Token Service
            builder.Services.AddSwaggerGen(options =>
            {
                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                                .AddJsonFile("appsettings.json")
                                .Build();

            /*******************************************
             *  Start JWT Security Configuration
             *  ****************************************/
            var secret = "MyVerySuperSecureSecretSharedKey";
            var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
            var issuer = "http://www.uc.edu/IT3047C";
            var audience = "WebServerApplicationDevelopment";

            builder.Services.AddAuthentication(OptionsBuilderConfigurationExtensions =>
            {
                OptionsBuilderConfigurationExtensions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = secretKey,

                    ValidateIssuer = true,
                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });

            /*****************************************
             *  End JWT Security Configuration
             *  **************************************/

           var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<QueryStringMiddleware>();

            app.UseMiddleware<IpSecurityMiddleware>();

            //app.UseQueryStrings();

            //app.Use(async (context, next) =>
            //{
            //    Console.WriteLine("Hello World");

            //    await next(context);

            //    Console.WriteLine("Responding to request");
            //});

            //app.UseWhen((context) = context.Request.Query.Count >0, (appBuilder) =>
            //{
            //    app.UseMiddleware<AlertMiddleware>();
            //});
            //app.UseAuthorization();

            //app.Map("/api/departments", async (context) =>
            //{
            //    await context.Response.WriteAsync("Hit the mapped Middleware");
            //});

            app.MapControllers();

            app.Run();
        }

        static void habdleBranch1() { }
        static void HandleBranch2() { }
    }
}
