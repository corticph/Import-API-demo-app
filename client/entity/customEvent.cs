using System.Runtime.Serialization;
using System.Collections.Generic;
using System;
using System.Globalization;

namespace ImportAPIClient.Client.Entity
{

    [DataContract(Name="customEvent")]
    public class CustomEvent
    {
        [DataMember(Name="id")]
        public String Id { get; set; }
        
        [DataMember(Name="name")]
        public string Name { get; set; }

        [DataMember(Name="text")]
        public string Text { get; set; }

        [DataMember(Name="datetime")]
        public string Datetime { get; set; }

        [DataMember(Name="triage_session_id")]
        public String TriageSessionId { get; set; }

        [DataMember(Name="case_id")]
        public string CaseId { get; set; }

        [DataMember(Name="created_at")]
        public String CreatedAt { get; set; }

        [DataMember(Name="created_by")]
        public String CreatedBy { get; set; }
        
        [DataMember(Name="custom_properties", IsRequired = true)]
        public Dictionary<string, string> CustomProperties { get; set; }
        
        [IgnoreDataMember]
        public DateTime? CreatedAtDatetime
        {
            get
            {
                return CreatedAt ==  null ? (DateTime?) null : DateTime.ParseExact(CreatedAt, "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ", CultureInfo.InvariantCulture);
            }
        }

        [IgnoreDataMember]
        public DateTime DatetimeDatetime
        {
            get
            {
                return DateTime.ParseExact(Datetime, "yyyy-MM-ddTHH:mm:ss.FFFFFFFZ", CultureInfo.InvariantCulture);
            }
            set
            {
                Datetime = value.ToString("yyyy-MM-ddTHH:mm:ss.FFFFFFFZ");
            }
        }
    }
}