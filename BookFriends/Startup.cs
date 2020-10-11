using BookFriendsDataAccess;
using BookFriendsDataAccess.Entities;
using BookFriendsDataAccess.Repository;
using BookFriendsDataAccess.Search;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BookFriends
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
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });
            services.AddDbContext<BookFriendsDbContext>(opt => opt.UseLazyLoadingProxies()
            .UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<IEntityRepository<CommunityGroup>, EntityRepository<CommunityGroup>>();
            services.AddScoped<IEntityRepository<CommunityMember>, EntityRepository<CommunityMember>>();
            services.AddScoped<IEntityRepository<PooledBook>, EntityRepository<PooledBook>>();
            services.AddScoped<IBfConfiguration, BfConfiguration>();
            services.AddScoped<IEntitySearch<CommunityGroup>, EntitySearch<CommunityGroup>>();
            services.AddScoped<IEntitySearch<PooledBook>, EntitySearch<PooledBook>>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                SeedDevelopmentDatabase(app);
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
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private void SeedDevelopmentDatabase(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                using (var dbContext = scope.ServiceProvider.GetRequiredService<BookFriendsDbContext>())
                {
                    BookFriendsDbInitializer.Seed(dbContext);
                }
            }
        }
    }
}
