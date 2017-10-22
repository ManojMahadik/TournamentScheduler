using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchScheduler.Model
{
    public class Schedule
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleId { get; set; }

        [DisplayName("Tournament Name")]
        public string TournamentName { get; set; }

        public List<ScheduleItem> ScheduleItems { get; set; }
    }
}
