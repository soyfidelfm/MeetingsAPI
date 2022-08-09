using Microsoft.AspNetCore.Mvc;
using MeetingsAPI.Objects;

namespace MeetingsAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Meetings : ControllerBase
    {
        [HttpPost(Name = "GetMeetingsWithOverlaps")]        
        public List<Meeting> GetMeetingsWithOverlaps(List<Meeting> meetings)
        {            
            //Instance the list that we will response.
            List<Meeting> result = new List<Meeting>();
            //Iterate all the dates that we receive. 
            meetings.ForEach(f =>
            {
                //A second itaration occurr to compare the two dates meetings
                meetings.ForEach(f1 =>
                {
                    //Validate that we are not comparing the same meeting.
                    if (f.EndDate != f1.EndDate && f.StartDate != f1.StartDate)
                    {
                        if(f.StartDate < f1.EndDate && f1.StartDate < f.EndDate)
                        {
                            //To be sure that we wont add twice the same date.
                            if(!result.Contains(f))
                                result.Add(f);
                            if (!result.Contains(f1))
                                result.Add(f1);
                        }
                    }
                });
            });
            return result;
            
        }
    }
}
#region Notes
/*
Example: The following data shows the conflicting scheduled meeting times as marked with an asterisk (*)
    *Jan 1st,   2016  9:00am   – Jan 1st,    2016 11:00am
     Jan 11th,  2016  10:00am  – Jan 11th ,  2016 1:30pm
     Jan 11th,  2016  1:30pm   – Jan 11th,   2016 4:00pm
     Jan 5th,   2016  9:00am   – Jan 5th,    2016 11:00am
    *Dec 29th,  2015  9:00am   – Jan 1st,    2016 10:00am
            */
/* ------------EXAMPLE FOR REQUEST ON SWAGGER------------
    [
      {
        "startDate": "2016-01-01T09:00:00.00Z",
        "endDate": "2016-01-01T11:00:00.00Z"
      },
    {
        "startDate": "2016-01-11T10:00:00.00Z",
        "endDate": "2016-01-11T13:30:00.00Z"
      },
    {
        "startDate": "2016-01-11T13:30:00.00Z",
        "endDate": "2016-01-11T16:00:00.00Z"
      },
    {
        "startDate": "2016-01-05T09:00:00.00Z",
        "endDate": "2016-01-05T11:00:00.00Z"
      },
    {
       "startDate": "2015-01-01T09:00:00.00Z",
        "endDate": "2016-01-01T10:00:00.00Z"
      }
    ]
 */
#endregion