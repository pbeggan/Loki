using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loki.Domain.Dtos
{
    public class TimesheetVersionDto
    {
        public int VersionId { get; set; }
        public int VersionNumber { get; set; }
        public DateTime AddedAtUtc { get; set; }
        public List<TimesheetVersionEntryDto> Entries { get; set; } = new List<TimesheetVersionEntryDto>();

        public TimesheetVersionEntryDto[] EntriesByDay(DayOfWeek dayOfWeek) => Entries.Where(e => e.Day.DayOfWeek == dayOfWeek).ToArray();
    }

    public class TimesheetVersionEntryDto
    {
        public long Id { get; set; }
        public int VersionId { get; set; }
        public DateTime Day { get; set; }
        public string? ExternalId { get; set; }
        public decimal Quantity { get; set; }
        public long? ChangedEntryId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
