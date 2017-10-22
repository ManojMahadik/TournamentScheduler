using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MatchScheduler.ViewModels
{
    public class ScheduleViewModel
    {
        [DisplayName("Tournament Name")]
        public string TournamentName { get; set; }

        public List<ScheduleItemViewModel> ScheduleItems { get; set; }
    }
}