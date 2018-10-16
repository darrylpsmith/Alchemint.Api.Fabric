using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Alchemint.Core;
using Alchemint.Bar;
using Swashbuckle.AspNetCore;
using Swashbuckle.AspNetCore.Swagger;


namespace Sam.Api
{
    public class Startup
    {

        ILogger Logger { get; } =
            WorkeFunctions.ApplicationLogging.CreateLogger<Startup>();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                 .AddJsonOptions(options => options.SerializerSettings.ContractResolver = new Newtonsoft.Json.Serialization.DefaultContractResolver());

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "SAM API", Version = "v1.0" });
            });


            services.AddSingleton(typeof(Storage<>));
            services.
                AddMvc(o => o.Conventions.Add(new GenericControllerRouteConvention())).
                ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider()));
                //ConfigureApplicationPartManager(m => m.FeatureProviders.Add(new RemoteControllerFeatureProvider()));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
                {
                    Debug.WriteLine(eventArgs.Exception.ToString());
                };
            }
            else
            {
                app.UseHsts();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), 
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "SAM API V1");
            });


            var myConfig = Configuration["Connection"];
            app.UseHttpsRedirection();
            app.UseMvc();


            if (env.IsDevelopment())
            {
                Logger.LogInformation($"DBTYPE : {Configuration["Connection:DBtype"]}");
            }

            string conn = "";

            if (Configuration["Connection:DBtype"] == "SQLITE")
            {
                //string path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                string path = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
                conn = Configuration["Connection:ConnStringSQLite"].Replace("@APPDATA@", path);
                if (Directory.Exists(conn) == false)
                {
                    Directory.CreateDirectory(conn);
                }

                conn = conn.Replace('\\', '/');
                conn += "SAMBACKEND5.sqlite";

                if (env.IsDevelopment())
                {
                    Logger.LogInformation($"DBFILE : {conn}");
                }

                WorkeFunctions.SetConnectInformation(
                    DatabaseType.SQLite, 
                    conn,
                    Configuration["DatabaseTenant:Code"],
                    Configuration["DatabaseTenant:Name"]);


                if (File.Exists(conn) == false)
                {
                    DBAccessSqlite db = new DBAccessSqlite(conn);
                    ((ICreateDatabase)db).Create();
                    WorkeFunctions.BusinessObjectAccess.StoreEntity(new ApiKey { Id = Guid.NewGuid().ToString(), ApiKeyValue = "33c35730-2deb-44ae-9a16-1dec27960052", TenantCode = Configuration["DatabaseTenant:Code"] });
                }

            }
            else if(Configuration["Connection:DBtype"] == "SQLSERVER")
            {
                conn = Configuration["Connection:ConnStringAzure"];

                WorkeFunctions.SetConnectInformation(
                    DatabaseType.SQLServer,
                    conn,
                    Configuration["DatabaseTenant:Code"],
                    Configuration["DatabaseTenant:Name"]);
            }
            else if (Configuration["Connection:DBtype"] == "SQLEXPRESS")
            {
                conn = Configuration["Connection:ConnStringSQLExpress"];

                WorkeFunctions.SetConnectInformation(
                    DatabaseType.SQLExpress,
                    conn,
                    Configuration["DatabaseTenant:Code"],
                    Configuration["DatabaseTenant:Name"]);

            }
            else if (Configuration["Connection:DBtype"] == "MYSQL")
            {
                conn = Configuration["Connection:ConnStringMySQL"];

                WorkeFunctions.SetConnectInformation(
                    DatabaseType.SQLExpress,
                    conn,
                    Configuration["DatabaseTenant:Code"],
                    Configuration["DatabaseTenant:Name"]);

            }
            else
            {
                throw new Exception("CONFIG ERROR: UNKNOWN DBTYPE");
            }

            if (conn.Trim().Length <= 0)
            {
                throw new Exception("CONFIG ERROR: CONNECTION");
            }

        }
    }
}
