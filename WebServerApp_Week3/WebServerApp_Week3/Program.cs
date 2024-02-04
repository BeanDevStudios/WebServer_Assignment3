using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WebServerApp_Week3.Middleware;
//using TokenService;

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

            builder.Services.AddDbContext<BusinessContext>(Option =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString(connectionName));
            });

            //builder.Services.AddSingleton<IValidator, Validator>();
            //builder.Services.AddScoped<IValidator, Validator>();
            //builder.Services.AddTransient<IValidator, Validator>();
            builder.Services.AddTransient<IValidator>(validator => new Validator(true));

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //Token Service
            //builder.Services.AddSwaggerGen(options =>
            //{
            //    // using System.Reflection;
            //    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            //    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            //});
            //
            //var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
            //                    .AddJsonFile("appsettings.json")
            //                    .Build();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseMiddleware<QueryStringMiddleware>();

            app.UseQueryStrings();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("Hello World");

                await next(context);

                Console.WriteLine("Responding to request");
            });

            app.UseWhen((context) = context.Request.Query.Count >0, (appBuilder) =>
            {
                app.UseMiddleware<AlertMiddleware>();
            });
            app.UseAuthorization();

            app.Map("/api/departments", async (context) =>
            {
                await context.Response.WriteAsync("Hit the mapped Middleware");
            });

            app.MapControllers();

            app.Run();
        }

        static void habdleBranch1() { }
        static void HandleBranch2() { }
    }
}
