using MatchScheduler.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchScheduler.Repository
{
    public class ScheduleDbContext : DbContext
    {
        public ScheduleDbContext() : base("ScheduleDb")
        {

        }

        public DbSet<Schedule> Schedules { get; set; }
    }
}
