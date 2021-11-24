using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

namespace InsuranceCompany
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Environment = env;
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
           

            services.AddDbContext<Models.InsuranceContext>(options =>
            {
                var connectionString = Configuration.GetConnectionString("InsuranceContext");
              

                if (Environment.IsDevelopment())
                {
                    options.UseSqlite(connectionString);
                }
                else
                {
                    //options.UseSqlServer(connectionString);
                }
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=ClientController}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{ controller = ProductController}/{ action = Index}/{ id ?}");

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{ controller = OrderController}/{ action = Index}/{ id ?}");
            });
        }
    }




    /*Public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Environment = env;
        Configuration = configuration;
    }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllersWithViews();
        services.AddDbContext<DbContext>(options =>
        options.UseSqlite(Configuration.GetConnectionString("MvcMovieContext")));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }
        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Database}/{action=Index}/{id?}"
                );

            endpoints.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Clients}/{action=Index}/{id?}"
                );

            endpoints.MapControllerRoute(
                name: "Default",
                pattern: "{controller=Products}/{action=Index}/{id?}"
                );
            //pattern: "{localhost:5001/Database}/{localhost:5001/Database/Index/{id?}");
        });
    }
}
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):
        base(options)
    {

    }*/
}



