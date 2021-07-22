using Infrastructure.Areas.Identity;
using Infrastructure.DataAccess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Flow.Infrastructure.DataAccess.Repositories;
using MediatR;
using Flow.Core.Contracts;
using Flow.Core.DomainModels;
using MongoDB.Bson.Serialization;
using AutoMapper;
using MongoDB.Bson.Serialization.IdGenerators;
using MongoDB.Bson;
using FlowUI.Utilities.LoggedInUserRequest;

namespace FlowUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddScoped<AuthenticationStateProvider, RevalidatingIdentityAuthenticationStateProvider<IdentityUser>>();

            //var autoMapperConfig = AutoMapperConfig.Bootstrap();
            services.AddAutoMapper(typeof(Startup));
            
            //services.AddMediatR(typeof(Startup));
            services.AddMediatR(typeof(User));
            services.AddMediatR(typeof(GetLoggedInUserRequest));

            // Set up MongoDb
            if (!string.IsNullOrWhiteSpace(Configuration.GetConnectionString("MongoDb")))
                services.AddSingleton<IMongoClient>(new MongoClient(Configuration.GetConnectionString("MongoDb")));

            RegisterBsonClassMaps();

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }

        /** Register class maps for MongoDb */
        public void RegisterBsonClassMaps()
        {
            BsonClassMap.RegisterClassMap<User>(cm =>
            {
                cm.AutoMap();
                cm.MapIdProperty(user => user.UserId);
            });

            BsonClassMap.RegisterClassMap<Post>(cm =>
            {
                cm.AutoMap();
                cm.SetIdMember(cm.GetMemberMap(post => post.PostId));
                cm.IdMemberMap.SetIgnoreIfDefault(true);
                cm.IdMemberMap.SetIdGenerator(CombGuidGenerator.Instance);
            });

            
            //BsonDefaults.GuidRepresentation = GuidRepresentation.Standard;

            //BsonSerializer.UseNullIdChecker = true; // used for reference types
            //BsonSerializer.UseZeroIdChecker = true; // used for value types
        }
    }
}
