using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enchere.Models;
using Enchere.Models.ViewModel;
using System.Configuration;
using System.Data.SqlClient;


namespace Enchere.Dal {
    public class EvaluationRequette {
        public static void insertEvaluation(Evaluation ev) {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            string request = "INSERT INTO Evaluation VALUES ('" + ev.Id.Trim() + "','" + ev.IdEnchere.Trim() + "', '" + ev.Date.ToString("yyyy-MM-dd") + "', " + ev.Cote + ", '" + "', '" + ev.Commentaire + ev.IdMembreDe + "', '" + ev.IdMembreA +  "')";
            SqlCommand command = new SqlCommand(request, connection);

            try {
                connection.Open();
                command.ExecuteNonQuery();
            } catch (Exception e) {
                System.Console.WriteLine(e.Message);
            } finally {
                connection.Close();
            }
        }


        public static Evaluation getEvaluation(string id, string IdM) {

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT * FROM Evaluation WHERE IdMembreDe = '" + IdM + "' AND IdEnchere = '" + id + "'";

            SqlCommand command = new SqlCommand(request, connection);

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Evaluation ev = null;
                if (reader.Read()) {
                    ev = new Evaluation((string)reader["Id"], (string)reader["IdEnchere"], (DateTime)reader["Date"], (int)reader["Cote"], (string)reader["Commentaire"], (string)reader["IdMembreDe"], (string)reader["IdMembreA"]);
                }
                reader.Close();
                return ev;
            } catch (Exception e) {
                System.Console.WriteLine(e.Message);
            } finally {
                connection.Close();
            }
            return null;

        }
    }
}