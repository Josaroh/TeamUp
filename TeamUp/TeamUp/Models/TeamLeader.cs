using System;
using System.Collections.Generic;
using System.Text;

namespace TeamUp.Models
{
    public class TeamLeader
    {

        public List<Activité> ListeActivitéCreee = new List<Activité>();

        public void supprimerSonActivite(int idActivite)
        {
            ListeActivitéCreee.Remove(new Activité() { Id = idActivite });

            //supprimer dans la bd
         
        }
    }
}
