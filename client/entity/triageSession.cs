using System.Runtime.Serialization;
using System;
using System.Globalization;

namespace ImportAPIClient.Client.Entity
{
    [DataContract(Name="triageSession")]
    public class TriageSession
    {
        [DataMember(Name="id")]
        public string Id { get; set; }
        
        [DataMember(Name="case_id")]
        public string CaseId { get; set; }
        
        [DataMember(Name="started_at")]
        public string StartedAt { get; set; }
        
        [IgnoreDataMember]
        public DateTime StartedAtDatetime
        {
            get
            {
                return DateTime.ParseExact(StartedAt, "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ", CultureInfo.InvariantCulture);
            }
        }
    }
}