using MatchScheduler.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchScheduler.Repository
{
    public interface IScheduleRepository
    {
        Schedule GetSchedule(int scheduleId);

        void SaveSchedule(Schedule schedule);
    }
}
