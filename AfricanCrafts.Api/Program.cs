
using AfricanCrafts.Api.Middlewares;
using CacheHelper;
using IdentityHelper.BI;
using ImageKitFileManager;
using LoggerHelper;
using Product.Presentation;
using Serilog;
using UserManagement.Presentation;

namespace AfricanCrafts.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Add Services
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddHttpClient();

            // Product Module Register
            builder.Services.AddProductStrapping(builder.Configuration);
            builder.Services.AddUserManagerStrapping(builder.Configuration);


            builder.Services.AddImageKitFileManagerConfig(builder.Configuration);
            builder.Services.AddIdentityHelper();
            builder.Services.AddCacheHelperDI();
            builder.Services.AddCors();
            builder.Host.UseSerilog();

           
            



            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Exception Handling and Problem Details
            builder.Services.AddExceptionHandler<GlobalExceptionHandlerMiddleware>();
            builder.Services.AddProblemDetails();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSerilogRequestLogging();
                app.UseMiddleware<RequestContextLoggingMiddleware>();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseCors(corsBuilder =>
            corsBuilder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader());
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
