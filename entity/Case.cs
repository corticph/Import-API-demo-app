using System.Collections.Generic;
using System;

namespace ImportAPIClient.Entity
{
    public class Case
    {
        public int Id { get; set; }

        public string ExternalId { get; set; }

        public float? Latitude { get; set; }

        public float? Longitude { get; set; }

        public String Division { get; set; }

        public String Batalion { get; set; }

        public ICollection<Event> Events { get; set; }
    }
}