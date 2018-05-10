using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enchere.Models.ViewModel;
using Enchere.Models;
using Enchere.Utility;
using System.Configuration;
using System.Data.SqlClient;

namespace Enchere.Dal
{
    public class CategoriesRequette
    {
        public static bool Add(Categorie c)
        {
            bool TEST = true;
           
            string chConnexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string leId = IdGenerator.getCategId();

            string requete = "INSERT INTO Categorie(Id, Nom, Description) VALUES ('" 
            + leId + "', '" + c.Nom + "', '" + c.Description + "')";

            SqlConnection connexion = new SqlConnection(chConnexion);
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            try
            {
                connexion.Open();
                commande.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                string msg = e.Message;
                TEST = false;
            }
            finally
            {
                connexion.Close();
            }
            return TEST;
        }

        public static Categorie GetCategorieByNom(string nom)
        {
            Categorie c = null;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT * FROM Categorie WHERE Nom = '" + nom + "'";
            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    c = new Categorie
                    {
                        Id = (string)dr["Id"],
                        Nom = (string)dr["Nom"],
                        Description = (string)dr["Description"]

                    };
                }
                dr.Close();
                // connection.Close();//
                return c;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                // connection.Close();//
            }
            finally
            {
                connection.Close();
            }
            return null;
        }

        public static Categorie GetCategorieById(string nom)
        {
            Categorie c = null;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT * FROM Categorie WHERE Id = '" + nom + "'";
            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    c = new Categorie
                    {
                        Id = (string)dr["Id"],
                        Nom = (string)dr["Nom"],
                        Description = (string)dr["Description"]

                    };
                }
                dr.Close();
                // connection.Close();//
                return c;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
                // connection.Close();//
            }
            finally
            {
                connection.Close();
            }
            return null;
        }

        public static bool Update(Categorie c)
        {
            string cStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr))
            {
                 string requete = "UPDATE Categorie SET Nom = '" + c.Nom +
                 "', Description = '" + c.Description + "' WHERE Id = '" + c.Id + "'";

                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.Text;
                try
                {
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.Message);
                    return false;
                }
                finally
                {
                    cnx.Close();
                }

            }
        }

        public static List<Categorie> lesCategories(string order)
        {
            string chConnexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);

            string requete = "SELECT * FROM Categorie ORDER BY " + order;
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            List<Categorie> maListe = new List<Categorie>();
            try
            {
                connexion.Open();
                SqlDataReader dr = commande.ExecuteReader();
                while (dr.Read())
                {
                    Categorie c = new Categorie
                    {
                        Id = (string)dr["Id"],
                        Nom = (string)dr["Nom"],
                        Description = (string)dr["Description"]

                    };
                    maListe.Add(c);
                }

                dr.Close();
                return maListe;
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.Message);
            }
            finally
            {
                connexion.Close();
            }
            return null;
        }

        public static void Supprimer(Categorie c)
        {
            string chConnexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);
            string requete = "DELETE FROM Categorie WHERE Id = '" + c.Id + "'";
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            try
            {
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();
            }
            catch { }
        }

    }
}