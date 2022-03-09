using System;
using System.Collections.Generic;
using System.Text;

namespace TeamUp.Models
{
    class Activite
    {
        public string id { get; set; }
        public string a_pour_team_leader_id { get; set; }
        public string titre { get; set; }
        public string date { get; set; }
        public string heure_debut { get; set; }
        public string duree { get; set; }
        public string lieu { get; set; }
        public string niveau { get; set; }
        public string nbr_participant { get; set; }
        public string activite_terminee { get; set; }
        public string coute_id { get; set; }
    }
}