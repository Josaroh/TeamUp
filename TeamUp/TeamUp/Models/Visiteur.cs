using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TeamUp.Models
{
    [Table("Visiteur")]
    public class Visiteur
    {
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
    }
}
