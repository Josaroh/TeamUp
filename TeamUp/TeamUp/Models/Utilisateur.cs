using System;
using System.Collections.Generic;
using System.Text;

namespace TeamUp.Models
{
    public class Utilisateur
    {
        public string _0 { get; set; }
        public string id { get; set; }
        public string _1 { get; set; }
        public string identifiant { get; set; }
        public string _2 { get; set; }
        public string nom { get; set; }
        public string _3 { get; set; }
        public string prenom { get; set; }
        public string _4 { get; set; }
        public string date_naissance { get; set; }
        public string _5 { get; set; }
        public string email { get; set; }
        public string _6 { get; set; }
        public string mot_de_passe { get; set; }
        public string _7 { get; set; }
        public string profil_id { get; set; }

        public Utilisateur(string identifiant, string nom, string prenom, string dateNaissance, string email, string motDePasse)
        {
            this._1 = identifiant;
            this.identifiant = identifiant;
            this._2 = nom;
            this.nom = nom;
            this._3 = prenom;
            this.prenom = prenom;
            this._4 = dateNaissance;
            this.date_naissance = dateNaissance;
            this._5 = email;
            this.email = email;
            this._6 = motDePasse;
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
