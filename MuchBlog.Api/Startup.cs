using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MuchBlog.Api.Filters;
using MuchBlog.Domain.Services;
using MuchBlog.Domain.Services.IService;
using MuchBlog.Domain.Services.IServices;
using MuchBlog.Infrastructure.Data;

namespace MuchBlog.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            //Ìí¼Ó¶ÔControllerµÄÖ§³Ö
            services.AddControllers(config =>
            {
                //ÔÊÐí·µ»Ø406×´Ì¬Âë
                config.ReturnHttpNotAcceptable = true;
            }).AddNewtonsoftJson();

            //×¢²áDbContextµÄÅäÖÃ
            services.AddDbContext<BlogDbContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            //×¢²áAutoMapper
            services.AddAutoMapper(typeof(Startup));

            //×¢²á²Ö´¢
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IEssayRepository, EssayRepository>();
            services.AddScoped<IClassificationRepository, ClassificationRepository>();
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<CheckUserExistFilterAttribute>();
            services.AddScoped<CheckEssayExistFilterAttribute>();
            services.AddScoped<CheckClassificationExistFilterAttribute>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
