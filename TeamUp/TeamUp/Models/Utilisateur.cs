using System;
using System.Collections.Generic;
using System.Text;

namespace TeamUp.Models
{
    public class Utilisateur
    {
        public string id { get; set; }
        public string identifiant { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string date_naissance { get; set; }
        public string email { get; set; }
        public string mot_de_passe { get; set; }
        public string profil_id { get; set; }

        public Utilisateur(string identifiant, string nom, string prenom, string dateNaissance, string email, string motDePasse)
        {
            this.identifiant = identifiant;
            this.nom = nom;
            this.prenom = prenom;
            this.date_naissance = dateNaissance;
            this.email = email;
            this.mot_de_passe = motDePasse;
        }

        public String toString()
        {
            string resultat;
            resultat = this.identifiant + " " + this.nom + " " + this.prenom + " " + this.date_naissance + " " + this.email + " " + this.mot_de_passe;
            return resultat;
        }
    }
}
