using System;
using FinalProject.Data;
using FinalProject.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(FinalProject.Areas.Identity.IdentityHostingStartup))]
namespace FinalProject.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                //services.AddDbContext<FlowerAppContext>(options =>
                //    options.UseSqlServer(context.Configuration
                //        .GetConnectionString("FinalProjectIdentityContextConnection")));
                //services.AddDbContext<FinalProjectIdentityContext>(options =>
                //  options.UseInMemoryDatabase("IdDb"));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<FlowerAppContext>();
            });
        }
    }
}