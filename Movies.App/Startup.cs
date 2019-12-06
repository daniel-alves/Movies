using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.App.Mappings;
using Movies.Domain;
using Movies.Domain.Entities;
using Movies.Infra.Contexts;
using Movies.Infra.Repositories.Common;
using Movies.Infra.Services.Common;
using Movies.Infra.Validators;

namespace Movies.App
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options => {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //registra o automapper no container D.I
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            services.AddSingleton(mappingConfig.CreateMapper());

            services.AddMvc()
                .AddFluentValidation(fv => { fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            
            //registra o dbcontext do entity no catainer D.I
            services.AddDbContext<MovieContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MovieContext")));

            //registra repository genérico no catainer D.I
            services.AddScoped(typeof(IMovieRepository<>), typeof(MovieRepository<>));

            //registra o service genérico no container D.I
            services.AddScoped(typeof(IMovieCrudService<>), typeof(MovieCrudService<>));

            //registra os validators no container D.I
            services.AddTransient<IValidator<Genre>, GenreValidator>();
            services.AddTransient<IValidator<Movie>, MovieValidator>();
            services.AddTransient<IValidator<Location>, LocationValidator>();

        }


        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
