using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TeamUp.Models
{
    [Table("Activité")]
    public class Activité
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100),NotNull]
        public string Titre { get; set; }

        [MaxLength(100), NotNull]
        public string Date { get; set; }

        [MaxLength(100), NotNull]
        public int HeureDebut { get; set; }

        [MaxLength(100), NotNull]
        public int Duree { get; set; }

        [MaxLength(100), NotNull]
        public string Lieu { get; set; }

        [MaxLength(100), NotNull]
        public string Niveau { get; set; }

        [MaxLength(100), NotNull]
        public int NbrParticipant { get; set; }

        [MaxLength(100), NotNull]
        public bool ActiviteTerminee { get; set; } = false;

        public TeamLeader TeamLeader { get; set; } = new TeamLeader();

        public List<Teammate> ListeTeammates { get; set; } = new List<Teammate>();



        public string toString()
        {
            string resultat;
            resultat = "[" + this.Id.ToString() +
                " , " + this.Titre +
                " , " + this.Date +
                " , " + this.HeureDebut +
                " , " + this.Duree +
                " , " + this.Lieu +
                " , " + this.Niveau +
                " , " + this.NbrParticipant +
                " , " + this.ActiviteTerminee + "]";

            return resultat;
        }

        public void modifierActivite(string titre, string date, int heure, int duree, string lieu, string niveau)
        {
            this.Titre = titre;
            this.Date = date;
            this.HeureDebut = heure;
            this.Duree = duree;
            this.Lieu = lieu;
            this.Niveau = niveau;

            //modifiaction en base de données --- créer le repository activité
        }

        public void supprimerActivite()
        {
            //detruit de la base de données
        }



        public void autoDestruction()
        {
            if (this.ActiviteTerminee)
            {
                //supprimer l'activité de la base de données
            }

        }

        public void estActiviteTerminee()
        {
            /*
            if(this.HeureDebut + this.Duree >= heure du smartphon){

                this.ActiviteTerminee=true;
            }
            */
        }
    }
}
