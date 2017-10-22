﻿using MatchScheduler.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchScheduler.Repository
{
    public class ScheduleRepository : IScheduleRepository
    {
        ScheduleDbContext _context;

        public ScheduleRepository()
        {
            _context = new ScheduleDbContext();
        }

        public Schedule GetSchedule(int scheduleId)
        {
            return _context.Schedules.Include("ScheduleItems").FirstOrDefault(s => s.ScheduleId == scheduleId);
        }

        public void SaveSchedule(Schedule schedule)
        {
            try
            {
                _context.Schedules.Add(schedule);
                _context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
