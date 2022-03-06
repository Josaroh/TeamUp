using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TeamUp.Models
{
    [Table("Profil")]
    public class Profil : Visiteur
    {

        [MaxLength(100), Unique, NotNull]
        public string Localisation { get; set; }

        [MaxLength(100), NotNull]
        public int Perimetre { get; set; }

        [MaxLength(100), NotNull]
        public string Preference { get; set; }

        public new string toString()
        {
            string resultat;
            resultat = "[" + this.Id.ToString() +
                " , " + this.Identifiant +
                " , " + this.Nom +
                " , " + this.Prenom +
                " , " + this.MotDePasse +
                " , " + this.DateDeNaissance +
                " , " + this.Email +
                " , " + this.Localisation +
                " , " + this.Perimetre +
                " , " + this.Preference + "]";

            return resultat;
        }

        public void modifierProfil(string nom, string prenom, string Identifiant, string email, string localisation, int perimetre, string preference)
        {
            this.Nom = nom;
            this.Prenom = prenom;
            this.Identifiant = Identifiant;
            this.Email = email;
            this.Localisation = localisation;
            this.Perimetre = perimetre;
            this.Preference = preference;
        }

    }
}
