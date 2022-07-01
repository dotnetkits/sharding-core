using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sample.SqlServerShardingTable.Entities;
using Sample.SqlServerShardingTable.VirtualRoutes;
using ShardingCore;
using ShardingCore.Sharding.ReadWriteConfigurations;
using ShardingCore.TableExists;

namespace Sample.SqlServerShardingTable
{
    public class Startup
    {
        public static readonly ILoggerFactory efLogger = LoggerFactory.Create(builder =>
        {
            builder.AddFilter((category, level) => category == DbLoggerCategory.Database.Command.Name && level == LogLevel.Information).AddConsole();
        });
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();
            //services.AddShardingDbContext<MyDbContext>((conStr, builder) =>
            //    {
            //        builder.UseSqlServer(conStr).UseLoggerFactory(efLogger);
            //    }).Begin(op =>
            //    {
            //        //�����ʹ��code-first����ѡ��false
            //        op.CreateShardingTableOnStart = true;
            //        //�����ʹ��code-first�����޸�Ϊfsle
            //        op.EnsureCreatedWithOutShardingTable = true;
            //        //���޷���ȡ·��ʱ�᷵��Ĭ��ֵ�����Ǳ���
            //        op.ThrowIfQueryRouteNotMatch = true;
            //    }).AddShardingTransaction((connection, builder) =>
            //    {
            //        builder.UseSqlServer(connection).UseLoggerFactory(efLogger);
            //    }).AddDefaultDataSource("ds0",
            //        "Data Source=localhost;Initial Catalog=EFCoreShardingTableDB;Integrated Security=True;")
            //    .AddShardingTableRoute(op =>
            //    {
            //        op.AddShardingTableRoute<SysUserVirtualTableRoute>();
            //        op.AddShardingTableRoute<OrderVirtualTableRoute>();
            //        op.AddShardingTableRoute<MultiShardingOrderVirtualTableRoute>();
            //    }).AddReadWriteSeparation(sp =>
            //    {
            //        return new Dictionary<string, IEnumerable<string>>()
            //        {
            //            {
            //                "ds0", new List<string>()
            //                {
            //                    "Data Source=localhost;Initial Catalog=EFCoreShardingTableDB;Integrated Security=True;"
            //                }
            //            }
            //        };
            //    },ReadStrategyEnum.Loop,defaultEnable:true).End();
            services.AddShardingDbContext<MyDbContext>().AddEntityConfig(op =>
            {
                //�����ʹ��code-first����ѡ��false
                op.CreateShardingTableOnStart = true;
                //�����ʹ��code-first�����޸�Ϊfsle
                op.EnsureCreatedWithOutShardingTable = true;
                //���޷���ȡ·��ʱ�᷵��Ĭ��ֵ�����Ǳ���
                op.ThrowIfQueryRouteNotMatch = false;
                op.AddShardingTableRoute<SysUserVirtualTableRoute>();
                op.AddShardingTableRoute<OrderVirtualTableRoute>();
                op.AddShardingTableRoute<MultiShardingOrderVirtualTableRoute>();
            }).AddConfig(op =>
            {
                op.ConfigId = "a";
                op.UseShardingQuery((conStr, builder) =>
                {
                    builder.UseSqlServer(conStr).UseLoggerFactory(efLogger);
                });
                op.UseShardingTransaction((connection, builder) =>
                {
                    builder.UseSqlServer(connection).UseLoggerFactory(efLogger);
                });
                op.AddDefaultDataSource("ds0",
                    "Data Source=localhost;Initial Catalog=EFCoreShardingTableDB;Integrated Security=True;");
                op.AddReadWriteSeparation(sp =>
                {
                    return new Dictionary<string, IEnumerable<string>>()
                    {
                        //{
                        //    "ds0", new List<string>()
                        //    {
                        //        "Data Source=localhost;Initial Catalog=EFCoreShardingTableDB;Integrated Security=True;"
                        //    }
                        //}
                    };
                }, ReadStrategyEnum.Loop, defaultEnable: true);
                op.ReplaceTableEnsureManager(sp=>new SqlServerTableEnsureManager<MyDbContext>());
            }).EnsureConfig();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseShardingCore();
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.InitSeed();
        }
    }
}
