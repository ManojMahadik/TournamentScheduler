using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchScheduler.Calculation
{
    public interface IScheduleCalculator
    {
        List<Match> GetSchedule(int noOfTeams);
    }
}
