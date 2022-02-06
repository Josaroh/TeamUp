using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using TeamUp.Models;
using Xamarin.Essentials;
using System.Threading.Tasks;

namespace TeamUp.Repositories
{
    public class VisiteurRepository
    {
        SQLiteAsyncConnection connection;

		public string StatusMessage { get; set; }
        public VisiteurRepository(string dbPath)
        {
            connection = new SQLiteAsyncConnection(dbPath);
            connection.CreateTableAsync<Visiteur>();
        }

		public async Task AddNewUserAsync(string Identifiant, string nom, string prenom, string dateDeNaissance, string email, string motDePasse)
		{
			int result = 0;

			try
			{
				result = await connection.InsertAsync(new Visiteur { Identifiant = Identifiant,
																	 Nom = nom,
																	 Prenom = prenom,
																	 DateDeNaissance = dateDeNaissance,
																	 Email = email,
																     MotDePasse = motDePasse


				});
				StatusMessage = $"{result} utilisateur ajouté: {Identifiant}";
			}
			catch (Exception ex)
			{
				StatusMessage = $"Impossible d'ajouter l'utilisateur : {Identifiant}.\n Erreur : {ex.Message}";
			}
		}

		public async Task <List<Visiteur>> GetVisiteurAsync(string identifiant, string motDePasse)
		{
			
			try
			{

				

				return await connection.QueryAsync<Visiteur>("SELECT * FROM Visiteur WHERE Identifiant='"+identifiant+ "' AND MotDePasse='"+ motDePasse+"'");




			}
			catch (Exception ex)
			{
				StatusMessage = $"Impossible de récuperer la liste des utilisateurs\n Erreur : {ex.Message}";
				Console.WriteLine($"erreur");
			}
			return new List<Visiteur>();
		}

	}
}
