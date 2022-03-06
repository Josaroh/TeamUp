using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TeamUp.Models
{
    public class Teammate : Visiteur
    {
        List<Activité> ListeActivitéRejointe;



        public new string toString()
        {
            string resultat = "";

            foreach (Activité Activité in this.ListeActivitéRejointe)
            {
                resultat += Activité.toString();
            }

            return resultat;
        }

        public void seDesinscrireActivite(int idActivite)
        {
            ListeActivitéRejointe.Remove(new Activité() { Id = idActivite });
        }

    }

    
}
