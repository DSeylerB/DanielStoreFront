﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DanielStoreFront.Models;

namespace DanielStoreFront
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
            services.AddMvc();
            services.AddAntiforgery();
            services.AddSession();

            services.AddDbContext<DanielTestContext>(
                opt => opt.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<DanielTestContext>()
                .AddDefaultTokenProviders();

            services.AddTransient<SendGrid.SendGridClient>((x) => 
            {
                return new SendGrid.SendGridClient(Configuration["sendGrid"]);
            } );


            services.AddTransient<Braintree.BraintreeGateway>((x) =>
            {
                return new Braintree.BraintreeGateway(
                    Configuration["braintree.environment"],
                    Configuration["braintree.merchantid"],
                    Configuration["braintree.publickey"],
                    Configuration["braintree.privatekey"]
                );
            });

            services.AddTransient<SmartyStreets.USStreetApi.Client>((x) =>
            {
                var client = new SmartyStreets.ClientBuilder(
                    Configuration["smartystreets.authid"],
                    Configuration["smartystreets.authtoken"])
                        .BuildUsStreetApiClient();

                return client;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure
            (IApplicationBuilder app, IHostingEnvironment env, DanielTestContext context)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseAuthentication();
            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            DBInitializer.Initialize(context);
        }
    }
}
