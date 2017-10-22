using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchScheduler.Calculation
{
    public class ScheduleCalculator : IScheduleCalculator
    {
        public List<Match> GetSchedule(int noOfTeams)
        {
            if (noOfTeams < 2)
                return new List<Match>();

            var daySchedules = new List<DaySchedule>();
            daySchedules.Add(new DaySchedule(0));

            for (int venue = 0; venue < 2; venue++)
            {
                for (int team1 = 0; team1 < noOfTeams; team1++)
                {
                    for (int team2 = team1 + 1; team2 < noOfTeams; team2++)
                    {
                        Match match = new Match { IsAtHome = (venue == 0), Team1 = team1, Team2 = team2 };

                        // Now determine when the day on which match should be scheduled.
                        int team1LastPlayedBeforeDays = 100;
                        int team2LastPlayedBeforeDays = 100;

                        bool matchAdded = false;
                        for (int day = 0; day < daySchedules.Count; day++)
                        {
                            if (daySchedules[day].Matches.Any(m => m.Team1 != null && (m.Team1 == team1 || m.Team2 == team1)))
                                team1LastPlayedBeforeDays = 0;
                            else
                                team1LastPlayedBeforeDays++;

                            if (daySchedules[day].Matches.Any(m => m.Team1 != null && (m.Team1 == team2 || m.Team2 == team2)))
                                team2LastPlayedBeforeDays = 0;
                            else
                                team2LastPlayedBeforeDays++;

                            for (int matchOnDay = 0; matchOnDay < Constants.MAX_MATCHES_PER_DAY; matchOnDay++)
                            {
                                if (daySchedules[day].Matches[matchOnDay].Team1 == null)
                                {
                                    if (team1LastPlayedBeforeDays > 1 && team2LastPlayedBeforeDays > 1)
                                    {
                                        match.Day = day;
                                        daySchedules[day].Matches[matchOnDay] = match;
                                        matchAdded = true;
                                        break;
                                    }
                                }
                            }
                            if (matchAdded)
                                break;
                        }

                        if (!matchAdded) // No suitable day found to add match. Hence add new day
                        {
                            if (team1LastPlayedBeforeDays == 0 || team2LastPlayedBeforeDays == 0) // If any of the team has played today only then add another empty day
                                daySchedules.Add(new DaySchedule(daySchedules.Count));

                            daySchedules.Add(new DaySchedule(daySchedules.Count));
                            int day = daySchedules.Count - 1;
                            match.Day = day;
                            daySchedules[day].Matches[0] = match;
                        }
                    }
                }
            }

            var result = new List<Match>();
            for (int i = 0; i < daySchedules.Count; i++)
                result.AddRange(daySchedules[i].Matches);

            return result;
        }
    }


    public class DaySchedule
    {
        public Match[] Matches { get; set; }
        public DaySchedule(int day)
        {
            this.Matches = new Match[Constants.MAX_MATCHES_PER_DAY];
            for (int i = 0; i < this.Matches.Count(); i++)
                this.Matches[i] = new Match() { Day = day };
        }
    }

    public class Match: IComparer<Match>
    {
        public int? Day { get; set; }

        public bool? IsAtHome { get; set; } // Venue of the match w.r.t. Team1

        public int? Team1 { get; set; }

        public int? Team2 { get; set; }

        public int Compare(Match x, Match y)
        {
            if (x.Day == y.Day && x.IsAtHome == y.IsAtHome && x.Team1 == y.Team1 && x.Team2 == y.Team2)
                return 0;
            else
                return 1;
        }

        public override string ToString()
        {
            if (Team1 == null)
                return string.Format("Day: {0}", Day.ToString());
            else
                return string.Format("Day: {0}, IsAtHome: {1}, Team1 {2}, Team2 {3}", Day, IsAtHome, Team1, Team2);
        }
    }
}
