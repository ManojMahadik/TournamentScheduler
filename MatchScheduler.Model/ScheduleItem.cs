using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchScheduler.Model
{
    public class ScheduleItem
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ScheduleItemId { get; set; }

        public int ScheduleId { get; set; }

        public int? Day { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }

        public bool? IsAtHome { get; set; }

        [ForeignKey("ScheduleId")]
        public Schedule Schedule { get; set; }
    }
}
