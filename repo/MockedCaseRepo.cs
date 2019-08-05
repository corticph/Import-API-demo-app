using System;
using System.Collections.Generic;
using System.Globalization;
using ImportAPIClient.Entity;

namespace ImportAPIClient.Repo
{

    public class MockedCaseRepo : ICaseRepo
    {
        ICollection<Case> ICaseRepo.GetAllCases()
        {
            return new List<Case>()
            {
                new Case
                {
                    Id = 2155510,
                    Batalion = "Battalion 2",
                    Division = "Battalion 2",
                    Latitude = (float?) 47.601643,
                    Longitude = (float?) 122.327656,
                    ExternalId = "2155510",
                    Events = new List<Event>()
                    {
                        new Event
                        {
                            Id = 5251,
                            Activity = "Cancel Response",
                            Comment = "Cancel Response",
                            Datetime = DateTime.ParseExact("2019-06-10 13:21:02.000", "yyyy-MM-dd HH:mm:ss.FFF", CultureInfo.InvariantCulture),
                            DispatcherInit = "TA",
                            RadioName = null,
                        },
                        new Event
                        {
                            Id = 5250,
                            Activity = "UserAction",
                            Comment = "UserAction",
                            Datetime = DateTime.ParseExact("2019-06-10 13:20:55.000", "yyyy-MM-dd HH:mm:ss.FFF", CultureInfo.InvariantCulture),
                            DispatcherInit = "TA",
                            RadioName = null,
                        },
                        new Event
                        {
                            Id = 5248,
                            Activity = "Sector Change",
                            Comment = "Sector Change",
                            Datetime = DateTime.ParseExact("2019-06-10 13:20:44.000", "yyyy-MM-dd HH:mm:ss.FFF", CultureInfo.InvariantCulture),
                            DispatcherInit = "TA",
                            RadioName = null,
                        },
                    }
                },
            };
        }
    }
}
