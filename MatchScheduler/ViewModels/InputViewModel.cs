using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MatchScheduler.ViewModels
{
    public class InputViewModel
    {
        const int MAX_TEAMS = 32;
        private List<int> _teamsDropDownItems = Enumerable.Range(2, MAX_TEAMS - 1).ToList();
        private List<string> _teamNames;
        private SelectList _selectList;
        public InputViewModel()
        {
            _selectList = new SelectList(_teamsDropDownItems);
            _teamNames = new List<string>();
            for (int i = 0; i < MAX_TEAMS; i++)
                _teamNames.Add("Team " + (i+1).ToString());
            NoOfTeams = 4;
        }

        [DisplayName("Tournament Name")]
        [MaxLength(30)]
        [Required]
        public string TournamentName { get; set; }

        [DisplayName("No. of Teams")]
        [Range(2, 32)]
        public int NoOfTeams { get; set; }

        public List<string> TeamNames
        {
            get
            {
                return _teamNames;
            }
            set {
                _teamNames = value;
            }
        }

        public SelectList NoOfTeamsDropDown
        {
            get { return _selectList; }
            set { _selectList = value; }
        }
    }
}