using System;
using eT3.LibraryApplication.Areas.Identity.Data;
using eT3.LibraryApplication.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(eT3.LibraryApplication.Areas.Identity.IdentityHostingStartup))]
namespace eT3.LibraryApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<MyContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("D6Context")));

                services.AddDefaultIdentity<ApplicationUser>()
                    .AddEntityFrameworkStores<MyContext>();
            });
        }
    }
}