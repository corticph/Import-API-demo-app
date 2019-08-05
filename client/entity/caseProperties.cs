using System.Runtime.Serialization;
using System.Collections.Generic;

namespace ImportAPIClient.Client.Entity
{

    [DataContract(Name="caseProperties")]
    public class CaseProperties
    {
        [DataMember(Name= "case_id")]
        public string CaseId { get; set; }

        [DataMember(Name = "custom_properties")]
        public Dictionary<string, string> CustomProperties { get; set; }

        [DataMember(Name= "latitude")]
        public float? Latitude { get; set; }

        [DataMember(Name= "longitude")]
        public float? Longitude { get; set; }
    }
}