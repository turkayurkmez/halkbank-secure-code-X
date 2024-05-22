
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using SecurityInRESTService.Security;
using SecurityInRESTService.Services;
using System.Text;

namespace SecurityInRESTService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddCors(option =>
            {
                option.AddPolicy("allow", builder =>
                {
                    /*
                     * http://www.halkbank.com.tr/krediler/basvur
                     * https://www.halkbank.com.tr
                     * https://stage.halkbank.com.tr
                     * https://stage.halkbank.com.tr:7575                     *                     
                     */

                    builder.AllowAnyOrigin();
                    builder.AllowAnyMethod();
                    builder.AllowAnyHeader();

                });
            });

            //builder.Services.AddAuthentication("Basic")
            //                .AddScheme<BasicOption, BasicHandler>("Basic", null);

            builder.Services.AddScoped<UserService>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                            .AddJwtBearer(opt =>
                            {
                                opt.TokenValidationParameters = new TokenValidationParameters
                                {
                                    ValidateIssuer = true,
                                    ValidateAudience = true,
                                    ValidIssuer = "server.halkbank",
                                    ValidAudience = "client.halkbank",
                                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("bu-cümle-token-onayi-icin-onemli")),
                                    ValidateIssuerSigningKey = true
                                };


                            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            
            }



            app.UseHttpsRedirection();
            app.UseCors("allow");
            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
