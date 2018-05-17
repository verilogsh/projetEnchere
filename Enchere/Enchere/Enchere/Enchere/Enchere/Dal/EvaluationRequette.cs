using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enchere.Models;
using Enchere.Models.ViewModel;
using System.Configuration;
using System.Data.SqlClient;


namespace Enchere.Dal
{
    public class EvaluationRequette
    {
        public static void insertEvaluation(Evaluation ev)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            string request = "INSERT INTO Evaluation VALUES ('" + ev.Id.Trim() + "','" + ev.IdEnchere.Trim() + "', '" + ev.Date.ToString("yyyy-MM-dd") + "', " + ev.Cote + ", '" + "', '" + ev.Commentaire + ev.IdMembreDe + "', '" + ev.IdMembreA + "')";
            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        public static List<Evaluation> getEvaluationsByIdMembre(string IdMembre) {

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT * FROM Evaluation WHERE IdMembreDe = '" + IdMembre;

            SqlCommand command = new SqlCommand(request, connection);

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Evaluation> ev = null;
                while (reader.Read()) {
                    ev.Add(new Evaluation((string)reader["Id"], (string)reader["IdEnchere"], (DateTime)reader["Date"], (int)reader["Cote"], (string)reader["Commentaire"], (string)reader["IdMembreDe"], (string)reader["IdMembreA"]));
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


        ////////////////////////////  End of Haiqiang Xu   /////////////////////////////////////////////////////
        public static Evaluation getEvaluation(string id, string IdM)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT * FROM Evaluation WHERE IdMembreDe = '" + IdM + "' AND IdEnchere = '" + id + "'";

            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Evaluation ev = null;
                if (reader.Read())
                {
                    ev = new Evaluation((string)reader["Id"], (string)reader["IdEnchere"], (DateTime)reader["Date"], (int)reader["Cote"], (string)reader["Commentaire"], (string)reader["IdMembreDe"], (string)reader["IdMembreA"]);
                }
                reader.Close();
                return ev;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return null;

        }


        public static List<EvaluationMembre> getEvaluationMembre()
        {

            List<Evaluation> list = getEvaluations();
            List<EvaluationMembre> listObj = new List<EvaluationMembre>();
            foreach (Evaluation en in list)
            {
                int NbrTotalEv = getNbrEvaluationMembre(en.IdMembreA);
                Membre mbr = MembreRequette.GetUserByNumero(en.IdMembreA);
                EvaluationMembre model = new EvaluationMembre(mbr.Nom, mbr.Numero, mbr.Cote, NbrTotalEv);
                listObj.Add(model);
            }


            return listObj;

        }

        private static List<Evaluation> getEvaluations()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT DISTINCT * FROM Evaluation";

            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Evaluation> list = new List<Evaluation>();
                Evaluation ev = null;
                while (reader.Read())
                {
                    ev = new Evaluation((string)reader["Id"], (string)reader["IdEnchere"], (DateTime)reader["Date"], (int)reader["Cote"], (string)reader["Commentaire"], (string)reader["IdMembreDe"], (string)reader["IdMembreA"]);
                    list.Add(ev);
                }
                reader.Close();
                return list;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return null;
        }

        private static int getNbrEvaluationMembre(string idMembre)
        {
            int cmpt = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT Id FROM Evaluation WHERE IdMembreA = '" + idMembre + "'";

            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();


                while (reader.Read())
                {

                    cmpt++;
                }
                reader.Close();
                return cmpt;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return 0;
        }


        public static List<Commissions> getCommissions()
        {
            int annee = DateTime.Now.Year;
            String DebutAnnee = annee + "-01-01";
            String FinAnnee = annee + "-12-31";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT  * FROM Commissions WHERE DATE >'"+ DebutAnnee+"' AND DATE  <'"+ FinAnnee +"'";

            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                List<Commissions> list = new List<Commissions>();
                Commissions ev = null;
                while (reader.Read())
                {
                    ev = new Commissions((int)reader["Id"], (int)reader["Taux"], (Decimal)reader["Prix"], (string)reader["IdEnchere"], (DateTime)reader["Date"]);
                    list.Add(ev);
                }
                reader.Close();
                return list;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return null;
        }
    }



}