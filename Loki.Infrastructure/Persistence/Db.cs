using System.Data.Common;
using System.Data.SqlClient;
using System.Data;
using Loki.Domain.Dtos;
using Dapper;

namespace Loki.Infrastructure.Persistence
{
    public static class Db
    {
        public static IDbConnection GetOpenConnection(string connectionString)
        {
            if (connectionString == null) throw new ArgumentException("ConnectionString can't be null");
            DbConnection cnn = new SqlConnection(connectionString);
            return cnn;
        }

        public static async Task<List<EvalRunLookupDto>> FetchLatestRunsAsync(string connectionString)
        {
            using var conn = GetOpenConnection(connectionString);

            var runs = await conn.QueryAsync<EvalRunLookupDto>(@"

                        With LatestLogs As
	                    (
		                    Select
                              latest_logs.timeLaborEvalRunID, JSON_VALUE(logs.[message], '$.message') as message
                            From
                              (Select
                                 Max(timeLaborEvalRunLogID) As latestLogID, timeLaborEvalRunID
                               From
                                  bullhorn1.BH_TimeLaborEvalRunLog
                               Group By
                                 timeLaborEvalRunID) As latest_logs
                            Join
                              bullhorn1.BH_TimeLaborEvalRunLog logs
                            On
                              logs.timeLaborEvalRunLogID = latest_logs.latestLogID
	                    )
                        Select Top 25 er.timeLaborEvalRunId as Id
                            , c.candidateId as CandidateId
                            , uc.name as CandidateName
                            , ctl.label as CalcType
                            , ci.label as PeriodRangeLabel
                            , er.startedAtUtc as StartedAtUtc
                            , er.timeLaborEvalRunStatusLookupId as RunStatusId
                            , logs.message as RunLogLastEntry
                        From 
                            bullhorn1.BH_TimeLaborEvalRun er
                            Join bullhorn1.BH_TimeLaborEvalPeriod ep
                                On er.timeLaborEvalPeriodId = ep.timeLaborEvalPeriodId
                            Join bullhorn1.BH_Candidate c
                                On ep.candidateId = c.candidateId
                            Join bullhorn1.BH_CalendarInstance ci
                                On ep.calendarInstanceId = ci.calendarInstanceId
                            Join bullhorn1.BH_TimeLaborCalcTypeLookup ctl
                                On ep.timeLaborCalcTypeLookupId = ctl.timeLaborCalcTypeLookupId
                            Join bullhorn1.BH_UserContact uc
                                On c.userId = uc.userId
                            Join LatestLogs logs
                                On er.timeLaborEvalRunID = logs.timeLaborEvalRunID
                        Order By er.timeLaborEvalRunId Desc"
            );

            return runs.ToList();
        }

        private static CommandDefinition DefineCommand(string query, object parameters, IDbTransaction? transaction = null)
            => new(query, parameters, commandTimeout: 180, transaction: transaction);
    }
}
