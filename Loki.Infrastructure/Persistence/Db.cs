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

        public static async Task<List<TimesheetLookupDto>> FetchTimesheetLookups(string connectionString)
        {
            using var conn = GetOpenConnection(connectionString);

            var timesheets = await conn.QueryAsync<TimesheetLookupDto>(@$"

                        Select ts.timeSheetId as Id
                            , p.placementId as PlacementId
                            , jo.title as PlacementName
                            , uc.userId as UserId
                            , uc.name as CandidateName
                            , ts.periodEndingAt as WeekEndingDate
                            , ts.externalId as ExternalId
                            , (select case when DATEDIFF(minute, ts.lastModifiedAtUtc, sysutcdatetime()) > 800 then 0 else 1 end) as HasRecentChanges
                        From 
                            bullhorn1.BH_Timesheet ts
                            Join bullhorn1.BH_Placement p
                                On ts.placementId = p.placementId
                            Join bullhorn1.BH_JobOpportunity jo
                                On p.jobPostingId = jo.jobPostingId
                            Join bullhorn1.BH_UserContact uc
                                On p.userId = uc.userId"
            );

            return timesheets.ToList();
        }

        public static async Task<List<EvalRunLogLookupDto>> FetchRunLogs(string connectionString, long runId)
        {
            using var conn = GetOpenConnection(connectionString);

            var runLogs = await conn.QueryAsync<EvalRunLogLookupDto>(@$"

                        Select erl.timeLaborEvalRunLogId as Id
                            , erl.addedAtUtc as AddedAtUtc
                            , lsl.label as LogSeverity
                            , erl.message as Message
                        From 
                            bullhorn1.BH_TimeLaborEvalRunLog erl
                            Join bullhorn1.BH_LogSeverityLookup lsl
                                On erl.logSeverityLookupID = lsl.logSeverityLookupID
                        Where erl.timeLaborEvalRunID = {runId}
                        Order By erl.addedAtUtc Desc"
            );

            return runLogs.ToList();
        }

        public static async Task<List<IssueLookupDto>> FetchLatestIssuesAsync(string connectionString)
        {
            using var conn = GetOpenConnection(connectionString);

            var issues = await conn.QueryAsync<IssueLookupDto>(@"

                        Select Top 100 i.issueId as Id
                            , i.action
                            , i.actionEntityName as ActionEntity
                            , i.actionEntityID 
                            , i.dateAdded
                            , ii.sourceEntity
                            , ii.sourceEntityID
                            , ii.comments
                            , ii.description
                          From bullhorn1.BH_Issue i
                          Join bullhorn1.BH_IssueItems ii
                            On i.issueID = ii.issueID
                          Order By i.issueID Desc"
            );

            return issues.ToList();
        }

        public static async Task<List<EvalRunLookupDto>> FetchLatestRunsAsync(string connectionString)
        {
            using var conn = GetOpenConnection(connectionString);
            //todo: placement names and ids
            // dictionary, timesheetid to placement name
            // expensesheetid to placement name
            var runs = await conn.QueryAsync<EvalRunLookupDto>(@"

                        With LatestLogs As
	                    (
		                    Select
                              latest_logs.timeLaborEvalRunID, JSON_VALUE(logs.[message], '$.message') as message
                            From
                              (Select
                                 Max(addedAtUtc) As latestUtc, timeLaborEvalRunID
                               From
                                  bullhorn1.BH_TimeLaborEvalRunLog
                               Group By
                                 timeLaborEvalRunID) As latest_logs
                            Join
                              bullhorn1.BH_TimeLaborEvalRunLog logs
                            On
                              logs.timeLaborEvalRunID = latest_logs.timeLaborEvalRunID And
                                logs.addedAtUtc = latest_logs.latestUtc
	                    )
                        Select Top 100 er.timeLaborEvalRunId as Id
                            , c.candidateId as CandidateId
                            , uc.name as CandidateName
                            , ctl.label as CalcType
                            , ci.label as PeriodRangeLabel
                            , er.startedAtUtc as StartedAtUtc
                            , er.timeLaborEvalRunStatusLookupId as RunStatusId
                            , logs.message as RunLogLastEntry
	                        , ExpenseSheetIds = (STUFF(
							 (SELECT ',' + CONVERT(varchar(10), epes.expenseSheetID) FROM bullhorn1.BH_TimeLaborEvalPeriodExpenseSheet epes 
								WHERE er.timeLaborEvalPeriodID = epes.timeLaborEvalPeriodID
							 FOR XML PATH ('')), 1, 1, ''
						   ))
						   , TimeSheetIds = (STUFF(
							 (SELECT ',' + CONVERT(varchar(10), epts.timeSheetID) FROM bullhorn1.BH_TimeLaborEvalPeriodTimeSheet epts 
								WHERE er.timeLaborEvalPeriodID = epts.timeLaborEvalPeriodID
							 FOR XML PATH ('')), 1, 1, ''
						   ))
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
