namespace Loki.Domain.Dtos
{
    public class TimesheetLookupDto
    {
        public int Id { get; set; }
        public string PlacementName { get; set; } = null!;
        public string CandidateName { get; set; } = null!;
        public DateTimeOffset WeekEndingDate { get; set; }
        public string? ExternalId { get; set; }
        public bool HasRecentChanges { get; set; }
    }
}
