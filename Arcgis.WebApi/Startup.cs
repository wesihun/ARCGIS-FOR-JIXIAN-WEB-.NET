using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Arcgis.IService;
using Arcgis.Service;
using DataNs.SqlSugar;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using Swashbuckle.AspNetCore.Swagger;

namespace Arcgis.WebApi
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
            #region 获取链接字符串
            IConfigurationSection defaultConnection;
            //获取链接字符串11
            var connectionStrings = Configuration.GetSection("ConnectionStrings");
            var dataBaseType = connectionStrings.GetSection("DataBaseType");
            switch (dataBaseType.Value.ToString())
            {
                case "SqlServer":
                    defaultConnection = connectionStrings.GetSection("SqlConnection");
                    break;
                case "MySql":
                    defaultConnection = connectionStrings.GetSection("SqlConnection");
                    break;
                default:
                    defaultConnection = connectionStrings.GetSection("OracleConnection");
                    break;
            }
            DbContext._dataBaseType = dataBaseType.Value.ToString();
            DbContext.DefaultDbConnectionString = defaultConnection.Value.ToString();
            #endregion

            #region 配置跨域处理
            //配置跨域处理
            services.AddCors(options =>
                options.AddPolicy("XY",
                builder => builder.WithOrigins().AllowAnyMethod().AllowAnyHeader().AllowAnyOrigin()
                ));
            #endregion

            #region DbContext 注入
            services.AddScoped<IDbContext, DbContext>();
            #endregion

            #region [ApiController]
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.SuppressConsumesConstraintForFormFileParameters = true;
                options.SuppressInferBindingSourcesForParameters = true;
                options.SuppressModelStateInvalidFilter = true;
            });
            #endregion

            #region 注入相关的应用服务
            services.AddScoped<ISpecialInvestigationService, SpecialInvestigationService>();
            services.AddScoped<IPersonalCenterService, PersonalCenterService>();
            services.AddScoped<IlogService, LogService>();
            #endregion

            #region swaggerp配置
            //注册Swagger生成器，定义一个和多个Swagger 文档
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new Info
                //{
                //    Version = "v1",
                //    Title = "yilezhu's API",
                //    Description = "A simple example ASP.NET Core Web API",
                //    TermsOfService = "None",
                //    Contact = new Contact
                //    {
                //        Name = "依乐祝",
                //        Email = string.Empty,
                //        Url = "http://www.cnblogs.com/yilezhu/"
                //    },
                //    License = new License
                //    {
                //        Name = "许可证名字",
                //        Url = "http://www.cnblogs.com/yilezhu/"
                //    }
                //});
                //注册Swagger生成器，定义一个和多个Swagger 文档
                c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });

                // 为 Swagger JSON and UI设置xml文档注释路径
                //var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);//获取应用程序所在目录（绝对，不受工作目录影响，建议采用此方法获取路径）
                //var xmlPath = Path.Combine(basePath, "Arcgis.WebApi.xml");
                //c.IncludeXmlComments(xmlPath);


                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Arcgis.WebApi.xml");
                c.IncludeXmlComments(xmlPath);
                c.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });
            #endregion

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors("XY");
            //  app.UseHttpsRedirection();

            //启用中间件服务生成Swagger作为JSON终结点
            app.UseSwagger();
            //启用中间件服务对swagger-ui，指定Swagger JSON终结点
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            app.UseMvc();
        }
    }
}
