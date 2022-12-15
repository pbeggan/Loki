namespace Loki.Domain.Dtos
{
    public class EvalRunLogLookupDto
    {
        public long Id { get; set; }
        public DateTime AddedAtUtc { get; set; }
        public string LogSeverity { get; set; } = null!;
        public string Message { get; set; } = null!;
    }
}
