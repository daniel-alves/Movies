using AutoMapper;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Movies.App.Describers.Identity;
using Movies.App.Mappings;
using Movies.App.Models.Accounts;
using Movies.App.Models.Genres;
using Movies.App.Models.Locations;
using Movies.App.Models.Movies;
using Movies.App.Validators;
using Movies.Infra.Data.Contexts;
using Movies.Infra.Repositories.Common;
using Movies.Infra.Repositories.Genres;
using Movies.Infra.Repositories.MovieLocations;
using Movies.Infra.Repositories.Movies;
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //registra as configurações no D.I
            services.AddSingleton(Configuration);

            //registra o automapper no container D.I
            var mappingConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
            services.AddSingleton(mappingConfig.CreateMapper());

            services.AddMvc()
                .AddFluentValidation(fv => { fv.RunDefaultMvcValidationAfterFluentValidationExecutes = false; })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            //registra o dbcontext do entity no catainer D.I
            services.AddDbContext<MovieContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MovieContext")));

            //registra o identity no container D.I
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<MovieContext>()
                .AddErrorDescriber<PtBrIdentityErrorDescriber>();

            //registra os repositories no catainer D.I
            services.AddScoped(typeof(ICommonRepository<>), typeof(CommonRepository<>));
            services.AddScoped(typeof(IGenreRepository), typeof(GenreRepository));
            services.AddScoped(typeof(IMovieRepository), typeof(MovieRepository));
            services.AddScoped(typeof(IMovieLocationRepository), typeof(MovieLocationRepository));

            //registra os services no container D.I
            services.AddScoped<IGenreCrudService, GenreCrudService>();
            services.AddScoped<IMovieCrudService, MovieCrudService>();
            services.AddScoped<ILocationCrudService, LocationCrudService>();

            //registra os validators no container D.I
            services.AddScoped<IValidator<GenreViewModel>, GenreValidator>();
            services.AddScoped<IValidator<MovieViewModel>, MovieValidator>();
            services.AddScoped<IValidator<LocationViewModel>, LocationValidator>();
            services.AddScoped<IValidator<RegisterViewModel>, RegisterValidator>();
            services.AddScoped<IValidator<LoginViewModel>, LoginValidator>();

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

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Location}/{action=Index}");
            });
        }
    }
}
