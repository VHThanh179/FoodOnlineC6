using ASM.Share.Helpers;
using ASM.Share.Interfaces;
using ASM.Share.Models;
using ASM.Share.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ASM.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //services.AddControllers();
            //services.AddSwaggerGen(c =>
            //{
            //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASM.API", Version = "v1" });
            //});

            //fix loi Json loop
            services.AddControllers()
                .AddJsonOptions(o => o.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;
                    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidAudience = Configuration["Jwt:Audience"],
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "ASM.API", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      new string[] { }
                    }
                  });
            });

            services.AddDbContextPool<DataContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")
                , b => b.MigrationsAssembly("ASM.Server")));

            services.AddTransient<IMaHoaHelper, MaHoaHelper>();
            //services.AddTransient<IUploadHelper, UploadHelper>();

            services.AddTransient<IMonAnService, MonAnService>();

            services.AddTransient<INguoiDungService, NguoiDungService>();

            services.AddTransient<IKhachHangService, KhachHangService>();

            services.AddTransient<IDonHangService, DonHangService>();

            services.AddTransient<IDonHangChiTietService, DonHangChiTietService>();

            services.AddCors(options => options.AddPolicy(
                  "_mypolicy", builder => builder
                  .AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader()
              )
               );



            //services.AddCors(); // Make sure you call this previous to AddMvc     

            //services.AddCors(policy =>
            //{
            //    policy.AddPolicy("_myAllowSpecificOrigins", builder => builder.WithOrigins("http://localhost:62257/")
            //         .AllowAnyMethod()
            //         .AllowAnyHeader()
            //         .AllowCredentials());
            //});

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "ASM.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseCors("_mypolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
