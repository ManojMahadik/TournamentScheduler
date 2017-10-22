using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MatchScheduler.ViewModels
{
    public class ScheduleItemViewModel
    {
        public int? Day { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public string MatchTitle
        {
            get {
                if (string.IsNullOrEmpty(Team1))
                    return "Break Day";
                else
                    return Team1 + " vs. " + Team2;
            }
        }

        public bool? IsAtHome { get; set; }

        public string Venue
        {
            get
            {
                if (!IsAtHome.HasValue)
                    return "-";
                else
                    return IsAtHome.Value ? Team1 : Team2;
            }
        }
    }
}