using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using SQLiteNetExtensions;
using SQLiteNetExtensions.Attributes;

namespace TeamUp.Models
{
    [Table("Visiteur")]
    public class Visiteur
    {
        //ATTRIBUTS

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [MaxLength(100), Unique, NotNull]
        public string Identifiant { get; set; }

        [MaxLength(100),NotNull]
        public string Nom { get; set; }

        [MaxLength(100), NotNull]
        public string Prenom { get; set; }

        [MaxLength(100), NotNull]
        public string MotDePasse { get; set; }

        [MaxLength(100), NotNull]
        public string DateDeNaissance { get; set; }

        [MaxLength(100), NotNull]
        public string Email { get; set; }

        public List<Activité> ListeActivites { get; set; } = new List<Activité>();



        //METHODES USUELLES

        public string toString()
        {
            string resultat;
            resultat = "[" + this.Id.ToString() + 
                " , " + this.Identifiant + 
                " , " + this.Nom +
                " , " + this.Prenom + 
                " , " + this.MotDePasse +
                " , " + this.DateDeNaissance +
                " , " + this.Email + "]";

            foreach (Activité Activité in this.ListeActivites)
            {
                resultat += Activité.toString();
            }

            return resultat;
        }


    }
}
