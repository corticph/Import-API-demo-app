using System;

namespace ImportAPIClient.Entity
{
    public class Event
    {
        public int Id { get; set; }

        public string Activity { get; set; }

        public string Comment { get; set; }

        public DateTime Datetime { get; set; }

        public string RadioName { get; set; }

        public string DispatcherInit { get; set; }
    }
}