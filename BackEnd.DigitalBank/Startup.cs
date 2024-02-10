using AutoMapper;
using CoreWCF;
using CoreWCF.Configuration;
using Infraestructure.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Persistence.Context;
using Serilog;
using System.Text;

namespace BackEnd.DigitalBank
{
    public class Startup
    {
        private IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddCors(o =>
            {
                o.AddPolicy("CorsApi",
                    builder => builder.WithOrigins("http://localhost:4200")
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .WithHeaders("Content-Type")
                    );
            });


            services.AddControllers();

            //Documentation API
            services.AddSwaggerGen(options =>
            options.SwaggerDoc(
                "v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Digital bank",
                    Description = "DigitalBank",
                    TermsOfService = new Uri("https://example.com/contact"),
                    Contact = new OpenApiContact
                    {
                        Name = "DigitalBank",
                        Url = new Uri("https://example.com/contact")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Licence",
                        Url = new Uri("https://example.com/license")
                    }
                })
            );

            services.AddEndpointsApiExplorer();

            //Mapper Singleton
            IMapper mapper = new MapperConfiguration(m => { m.AddProfile(new AutoMapperProfile()); }).CreateMapper();

            services.AddSingleton(mapper);
            services.AddServiceModelServices();


            //Database mapping, in profiles to configuration.
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration.GetConnectionString("DataBase")));

            //Auth
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["KeyJwt"]!)),
                    ClockSkew = TimeSpan.Zero
                });

            //Configuration serilog - Application Logs           
            Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Middleware's

            // Environment configuration
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("CorsApi");

            app.UseAuthorization();

            //app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
