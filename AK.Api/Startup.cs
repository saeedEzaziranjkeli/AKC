using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;
using AK.Api.Extensions;
using AK.Domain.Interfaces;
using AK.Infrastructure.Data;
using AK.Infrastructure.Repository;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace AK.Api
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
            services.AddControllers();
            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            );

            services.AddDbContext<EFDbContext>((s) =>
                    s.UseSqlServer(Configuration.GetConnectionString(ApiConstant.DbConnectionKey)), ServiceLifetime.Singleton
            );
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IAuthRepository, AuthRepository>();
            var assembly = AppDomain.CurrentDomain.Load(ApiConstant.ApplicationProjectName);
            services.AddMediatR(assembly);
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8
                            .GetBytes(Configuration.GetSection(ApiConstant.AuthToken).Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
