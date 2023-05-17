using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki.Domain.Dtos
{
    public class EvalRunLookupDto
    {
        public long Id { get; set; }
        public int CandidateId { get; set; }
        public string CandidateName { get; set; } = null!;
        public string CalcType { get; set; } = null!;
        public string PeriodRangeLabel { get; set; } = null!;
        public DateTime StartedAtUtc { get; set; }
        public DateTime EvalRequestedAtUtc { get; set; }
        public int RunStatusId { get; set; }
        public string RunLogLastEntry { get; set; } = null!;
        public string? TimeSheetIds { get; set; }
        public string? ExpenseSheetIds { get; set; }
        public string GetRunStatusCss()
        {
            return RunStatusId switch
            {
                1 or 2 => "oi oi-clock blue",
                3 => "oi oi-circle-check green",
                4 => "oi oi-circle-x red",
                _ => throw new Exception($"Unhandled run status ID [{RunStatusId}]"),
            };
        }
    }
}
