using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki.Domain.Dtos
{
    public class EvalRunLookupDto
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public string CandidateName { get; set; } = null!;
        public string CalcType { get; set; } = null!;
        public string PeriodRangeLabel { get; set; } = null!;
        public int RunStatusId { get; set; }
        public string RunLogLastEntry { get; set; } = null!;
    }
}
