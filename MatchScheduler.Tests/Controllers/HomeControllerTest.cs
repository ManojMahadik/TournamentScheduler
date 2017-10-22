using MatchScheduler.Calculation;
using MatchScheduler.Controllers;
using MatchScheduler.Model;
using MatchScheduler.Repository;
using MatchScheduler.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MatchScheduler.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private Mock<IScheduleCalculator> _scheduleCalculator;
        private Mock<IScheduleRepository> _scheduleRepository;
        InputViewModel _InputViewModel;

        [TestInitialize]
        public void SetupTestInitialise()
        {
            _scheduleCalculator = new Mock<IScheduleCalculator>();
            _scheduleCalculator.Setup(s => s.GetSchedule(It.IsAny<int>())).Returns(new List<Calculation.Match>());

            _scheduleRepository = new Mock<IScheduleRepository>();
            _scheduleRepository.Setup(m => m.SaveSchedule(It.IsAny<Schedule>())).Callback<Schedule>(s => s.ScheduleId = 1); // Set the schedule Id to 1 while saving in repository.

            _scheduleRepository.Setup(m => m.GetSchedule(It.IsAny<int>())).Returns(new Schedule() { ScheduleId = 1, ScheduleItems = new List<ScheduleItem>(), TournamentName = "Test" });

            _InputViewModel = new InputViewModel
            {
                TournamentName = "Test",
                NoOfTeams = 2,
                NoOfTeamsDropDown = new SelectList(new List<string> { "1", "2" }),
                TeamNames = new List<string>() { "Test1", "Test2" }
            };
        }

        [TestMethod]
        public void Create_Returns_View_With_InputViewModel()
        {
            // Arrange
            HomeController controller = new HomeController(_scheduleCalculator.Object, _scheduleRepository.Object);

            // Act
            ViewResult result = controller.Create() as ViewResult;

            // Assert
            Assert.IsInstanceOfType(result.Model, typeof(InputViewModel));
        }

        [TestMethod]
        public void Create_Post_Returns_Calls_Save_On_Repository()
        {
            HomeController controller = new HomeController(_scheduleCalculator.Object, _scheduleRepository.Object);

           var result = controller.Create(_InputViewModel);

            _scheduleRepository.Verify(m => m.SaveSchedule(It.IsAny<Schedule>()), Times.Once);
        }

        [TestMethod]
        public void Create_Post_Redirects_To_Schedule_Action()
        {
            var controller = new HomeController(_scheduleCalculator.Object, _scheduleRepository.Object);

            var result = (RedirectToRouteResult)controller.Create(_InputViewModel);

            Assert.AreEqual(result.RouteValues["action"], "Schedule");
            Assert.AreEqual(result.RouteValues["id"], 1);
        }

        [TestMethod]
        public void Schedule_Returns_HttopNotFound_When_Id_Is_Null()
        {
            var controller = new HomeController(_scheduleCalculator.Object, _scheduleRepository.Object);

            var result = controller.Schedule(null);

            Assert.IsInstanceOfType(result, typeof(HttpNotFoundResult));
        }

        [TestMethod]
        public void Schedule_Returns_View_With_ScheduleViewModel()
        {
            var controller = new HomeController(_scheduleCalculator.Object, _scheduleRepository.Object);

            var result = controller.Schedule(1) as ViewResult;

            Assert.IsInstanceOfType(result.Model, typeof(ScheduleViewModel));
        }
    }
}
