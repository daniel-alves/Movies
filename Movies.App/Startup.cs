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
using Movies.App.Models.Genres;
using Movies.App.Models.Locations;
using Movies.App.Models.Movies;
using Movies.App.Validators;
using Movies.Infra.Contexts;
using Movies.Infra.Repositories.Common;
using Movies.Infra.Services.Common;
using Movies.Infra.Services.Genres;
using Movies.Infra.Services.Locations;
using Movies.Infra.Services.Movies;
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

            //registra os repositories no catainer D.I
            services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));

            //registra os services no container D.I
            services.AddScoped(typeof(ICommonCrudService<>), typeof(CommonCrudService<>));
            services.AddScoped<IGenreCrudService, GenreCrudService>();
            services.AddScoped<IMovieCrudService, MovieCrudService>();
            services.AddScoped<ILocationCrudService, LocationCrudService>();

            //registra os validators no container D.I
            services.AddTransient<IValidator<GenreViewModel>, GenreValidator>();
            services.AddTransient<IValidator<MovieViewModel>, MovieValidator>();
            services.AddTransient<IValidator<LocationViewModel>, LocationValidator>();

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
