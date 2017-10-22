using Microsoft.VisualStudio.TestTools.UnitTesting;
using MatchScheduler.Calculation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchScheduler.Calculation.Tests
{
    [TestClass()]
    public class ScheduleCalculatorTests
    {
        ScheduleCalculator _calculator;

        [TestInitialize]
        public void SetupTestInitialise()
        {
            _calculator = new ScheduleCalculator();
        }

        [TestMethod()]
        public void GetSchedule_Should_Return_O_Matches_For_0_Teams()
        {
            var result = _calculator.GetSchedule(0);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod()]
        public void GetSchedule_Should_Return_O_Matches_For_1_Teams()
        {
            var result = _calculator.GetSchedule(0);
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void GetSchedule_Should_Return_6_Matches_For_2_Teams()
        {
            var expected = new List<Match>() {
                new Match{ Day = 0, IsAtHome = true, Team1 = 0, Team2 = 1},
                new Match{ Day = 0, IsAtHome = null, Team1 = null, Team2 = null},
                new Match{ Day = 1, IsAtHome = null, Team1 = null, Team2 = null},
                new Match{ Day = 1, IsAtHome = null, Team1 = null, Team2 = null},
                new Match{ Day = 2, IsAtHome = false, Team1 = 0, Team2 = 1},
                new Match{ Day = 2, IsAtHome = null, Team1 = null, Team2 = null},
            };

            var actual = _calculator.GetSchedule(2);
            Assert.IsTrue(AreMatchListEqual(expected, actual));
        }

        [TestMethod]
        public void GetSchedule_Should_Exactly_Contain_One_Home_And_One_Away_Match()
        {
            var result = _calculator.GetSchedule(4);

            // Check if home and away matches count is excatly one for matches between team 0 and team 2
            int homeCount = result.Count(t => t.IsAtHome == true && t.Team1 == 0 && t.Team2 == 2);
            int awayCount = result.Count(t => t.IsAtHome == true && t.Team1 == 0 && t.Team2 == 2);

            Assert.IsTrue(homeCount == awayCount && homeCount == 1);
        }

        [TestMethod]
        public void GetSchedule_Should_Not_Contain_Matches_On_Consecutive_Days_For_Same_Team()
        {
            int teamCount = 5;
            var result = _calculator.GetSchedule(teamCount);

            Assert.IsFalse(IsAnyTeamPlayedOnSameOrConsecutiveDay(teamCount, result));
        }

        private bool AreMatchListEqual(List<Match> source, List<Match> target)
        {
            if (source.Count != target.Count)
                return false;

            for(int i = 0; i < target.Count; i++)
            {
                if (source[i].Day == target[i].Day && source[i].IsAtHome == target[i].IsAtHome && source[i].Team1 == target[i].Team1 && source[i].Team2 == target[i].Team2)
                    return true;
                else
                    return false;
            }
            return true;
        }

        /// <summary>Check if any team played on same day or consecutive day.</summary>
        private bool IsAnyTeamPlayedOnSameOrConsecutiveDay(int noOfTeams, List<Match> matches)
        {
            for (int i = 0; i < noOfTeams; i++)
            {
                int lastPlayedDay = -100;
                for (int j = 0; j < matches.Count; j++)
                {
                    if (matches[j].Team1 == i || matches[j].Team2 == i)
                    {
                        if (matches[j].Day - lastPlayedDay < 2)
                            return true;

                        lastPlayedDay = matches[j].Day.Value;
                    }
                }
            }
            return false;
        }
    }
}