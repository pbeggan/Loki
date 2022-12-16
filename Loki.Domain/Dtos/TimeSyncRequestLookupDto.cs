using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Bullhorn.BteOne.C4.Messages.TleSync.Time;

namespace Loki.Domain.Dtos
{
    //Look up by timesheetId, get versions and 
    public class TimeSyncRequestLookupDto
    {
        public long Id { get; set; }
        public int? PlacementId { get; set; }
        //public TimeSheetEntrySyncRequestPayload? EntriesOnlyPayload { get; set; }
        //public TimeSheetSyncRequestPayload? SheetsPayload { get; set; }
        //public string? PayloadType()
        //{
        //    if (EntriesOnlyPayload is not null) return "Entries Only";
        //    if (SheetsPayload is not null) return "Sheets";
        //    return null;
        //}
        public int RequestStatus { get; set; }
        public int NumOfProcessingAttempts { get; set; }
        public DateTime AddedAtUtc { get; set; }
    }
}
