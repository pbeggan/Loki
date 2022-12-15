namespace Loki.Domain.Dtos
{
    public class IssueLookupDto
    {
        public int IssueId { get; set; }
        public string? Action { get; set; }
        public string? ActionEntity { get; set; }
        public int? ActionEntityId { get; set; }
        public DateTime DateAdded { get; set; }
        public List<IssueItemLookupDto>? Items { get; set; }
    }

    public class IssueItemLookupDto
    {
        public int IssueItemId { get; set; }
        public int IssueId { get; set; }
        public string? SourceEntity { get; set; }
        public int? SourceEntityId { get; set; }
        public string? Comments { get; set; }
        public string? Description { get; set; }
    }
}
