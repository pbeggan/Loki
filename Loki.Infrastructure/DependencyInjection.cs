using Loki.Domain;
using Loki.Domain.Interfaces;
using Loki.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IAppSettings appSettings)
        {
            //services.AddTransient<IPerDiemsService, PerDiemsService>();
            //services.AddTransient<IApplyRulesetsService, ApplyRulesetsService>();

            //// Domain services
            //services.AddTransient<IPerDiemAdjustmentCalculatorService, PerDiemAdjustmentCalculatorService>();
            //services.AddTransient<IAdjustmentMergeService, AdjustmentMergeService>();

            // Inject IDbConnection, with implementation from SqlConnection class.
            
            //services.AddTransient<IDbConnection>((sp) => new SqlConnection(connectionString));

            //services.AddTransient<IRepository, Repository>();

            return services;
        }
    }

}
