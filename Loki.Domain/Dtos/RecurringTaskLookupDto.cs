namespace Loki.Domain.Dtos
{
    public class RecurringTaskLookupDto
    {
        public long Id { get; set; }
        public int CorporationId { get; set; }
        public string TaskType { get; set; } = null!;
        public bool IsEnabled { get; set; }
        public string Schedule { get; set; } = null!;
        public string? Parameters { get; set; } 
        public string? Discriminator { get; set; }
        public string GetTaskStatusCss()
        {
            return IsEnabled switch
            {
                true => "oi oi-circle-check green",
                false => "oi oi-circle-x red",
            };
        }
    }
}
