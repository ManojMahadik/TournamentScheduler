using MatchScheduler.Calculation;
using MatchScheduler.Model;
using MatchScheduler.Repository;
using MatchScheduler.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatchScheduler.Controllers
{
    public class HomeController : Controller
    {
        private IScheduleCalculator _scheduleCalculator;
        private IScheduleRepository _scheduleRepository;

        public HomeController(IScheduleCalculator calculator, IScheduleRepository scheduleRepository)
        {
            _scheduleCalculator = calculator;
            _scheduleRepository = scheduleRepository;
        }

        public ActionResult Create()
        {
            InputViewModel model = new InputViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(InputViewModel model)
        {
            // If invalid model then return index view to correct the errors.
            if (!ModelState.IsValid)
                return View("Create", model);

            // Check if any team name is null or empty
            if (model.TeamNames.Any(t => string.IsNullOrEmpty(t)))
            {
                ModelState.AddModelError(string.Empty, "Team Name Cannot Empty");
                return View("Create", model);
            }

            var schedule = _scheduleCalculator.GetSchedule(model.NoOfTeams);

            // Save the schedule
            var dbSchedule = new Schedule
            {
                TournamentName = model.TournamentName,
                ScheduleItems = schedule.Select(s => new ScheduleItem
                {
                    Day = s.Day,
                    IsAtHome = s.IsAtHome,
                    Team1 = s.Team1.HasValue ? model.TeamNames[s.Team1.Value] : null,
                    Team2 = s.Team2.HasValue ? model.TeamNames[s.Team2.Value] : null
                }).ToList()
            };

            _scheduleRepository.SaveSchedule(dbSchedule);

            return RedirectToAction("Schedule", new { id = dbSchedule.ScheduleId });
        }

        public ActionResult Schedule(int? id)
        {
            if (!id.HasValue)
                return new HttpNotFoundResult();

            var dbSchedule = _scheduleRepository.GetSchedule(id.Value);

            if (dbSchedule == null)
                return new HttpNotFoundResult();

            var viewSchedule = new ScheduleViewModel
            {
                TournamentName = dbSchedule.TournamentName,
                ScheduleItems = dbSchedule.ScheduleItems.Select(t => new ScheduleItemViewModel
                {
                    Day = t.Day,
                    IsAtHome = t.IsAtHome,
                    Team1 = t.Team1,
                    Team2 = t.Team2
                }).ToList()
            };

            return View(viewSchedule);
        }
    }
}