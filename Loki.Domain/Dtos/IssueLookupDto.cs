namespace Loki.Domain.Dtos
{
    public class IssueLookupDto
    {
        public int IssueId { get; set; }
        public string? Action { get; set; }
        public string? ActionEntityName { get; set; }
        public int? ActionEntityId { get; set; }
        public DateTime DateAdded { get; set; }
        public List<IssueItemLookupDto>? Items { get; set; }

        public string ToActionString()
        {
            var actionStr = string.Empty;
            if (!string.IsNullOrWhiteSpace(Action))
                actionStr += Action;
            if (!string.IsNullOrWhiteSpace(ActionEntityName))
            {
                actionStr += $" on {ActionEntityName}";
                if (ActionEntityId.GetValueOrDefault(0) > 0)
                    actionStr += " " + ActionEntityId;
            }
            return actionStr.Trim();
        } 
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
