using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Loki.Domain.Dtos;
using Dapper;

namespace Loki.Infrastructure.Persistence
{
    public static class OslDb
    {
        public static IDbConnection GetOpenConnection(string connectionString)
        {
            if (connectionString == null) throw new ArgumentException("ConnectionString can't be null");
            DbConnection cnn = new SqlConnection(connectionString);
            return cnn;
        }

        public static async Task<List<RecurringTaskLookupDto>> FetchRecurringTasks(string connectionString)
        {
            using var conn = GetOpenConnection(connectionString);

            var tasks = await conn.QueryAsync<RecurringTaskLookupDto>(@$"

                        Select t.taskEngineRecurringTaskID as Id
                            , t.corporationID as CorporationId
                            , ttl.label as TaskType
                            , t.isEnabled as IsEnabled
                            , t.schedule as Schedule
                            , t.parameters as Parameters
                            , t.taskDiscriminator as Discriminator
                        From 
                            bullhorn1.BH_TaskEngineRecurringTask t
                            Join bullhorn1.BH_TaskEngineTaskTypeLookup ttl
                                On t.taskEngineTaskTypeLookupID = ttl.taskEngineTaskTypeLookupID"
            );

            return tasks.ToList();
        }
    }
}
