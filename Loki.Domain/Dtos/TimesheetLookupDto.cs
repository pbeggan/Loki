namespace Loki.Domain.Dtos
{
    public class TimesheetLookupDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? CandidateName { get; set; } 
        public string GetCandidate() => string.IsNullOrWhiteSpace(CandidateName) ? $"User #{UserId} (Candidate Name Not Found)" : CandidateName;
        public int PlacementId { get; set; }
        public string? PlacementName { get; set; }
        public string GetPlacement() => string.IsNullOrWhiteSpace(PlacementName) ? $"Placement #{PlacementId} (Name Not Found)" : PlacementName;
        public DateTimeOffset WeekEndingDate { get; set; }
        public string? ExternalId { get; set; }
        public bool HasRecentChanges { get; set; }

        public string GetPaddedSheetId(int maxLength)
        {
            var str = $"({Id})";
            switch (maxLength - str.Length)
            {
                case 1:
                    str += "&nbsp;&nbsp;";
                    break;
                case 2:
                    str += "&nbsp;&nbsp;&nbsp;&nbsp;";
                    break;
                case 3:
                    str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    break;
                case 4:
                    str += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                    break;
            }
            return str;
        }
    }
}
