using Enchere.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Enchere.Dal {
    public class EnchereRequette {
        public static void setEnchereEtat(string id, int etat) {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "UPDATE Enchere SET Etat = " + etat + " WHERE Id = '" + id + "'";
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

        public static Encher getEnchereById(string Id) {

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT * FROM Enchere WHERE Id = '" + Id.Trim() + "'";

            SqlCommand command = new SqlCommand(request, connection);

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Encher en = null;
                if (reader.Read()) {
                    en = new Encher((string)reader["Id"], (string)reader["IdObjet"], (string)reader["IdVendeur"], (string)reader["IdAcheteur"], (decimal)reader["PrixAchat"], (decimal)reader["PasDePrix"], (DateTime)reader["DateDepart"], (DateTime)reader["DateFin"], (int)reader["Etat"]);
                }
                reader.Close();
                return en;
            } catch (Exception e) {
                System.Console.WriteLine(e.Message);
            } finally {
                connection.Close();
            }
            return null;
        }

        public static List<Encher> getEncheresAcheteur(string numero, int etat) {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "";
            if (etat == 1) {
                request = "SELECT * FROM  Enchere WHERE IdAcheteur = '" + numero.Trim() + "' AND Etat != 0 AND Etat != 2";
            } else {
                request = "SELECT * FROM  Enchere WHERE IdAcheteur = '" + numero.Trim() + "' AND Etat = " + etat;
            }
            List<Encher> list = new List<Encher>();


            SqlCommand command = new SqlCommand(request, connection);

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    list.Add(new Encher((string)reader["Id"], (string)reader["IdObjet"], (string)reader["IdVendeur"], (string)reader["IdAcheteur"], (decimal)reader["PrixAchat"], (decimal)reader["PasDePrix"], (DateTime)reader["DateDepart"], (DateTime)reader["DateFin"], (int)reader["Etat"]));
                }
                reader.Close();
                return list;
            } catch (Exception e) {
                System.Console.WriteLine(e.Message);
            } finally {
                connection.Close();
            }
            return null;
        }

        public static List<Encher> getEncheresVendeur(string numero, int etat) {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "";
            if (etat == 1) {
                request = "SELECT * FROM  Enchere WHERE IdVendeur = '" + numero.Trim() + "' AND Etat != 0 AND Etat != 2";
            } else {
                request = "SELECT * FROM  Enchere WHERE IdVendeur = '" + numero.Trim() + "' AND Etat = " + etat;
            }

            List<Encher> list = new List<Encher>();


            SqlCommand command = new SqlCommand(request, connection);

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    list.Add(new Encher((string)reader["Id"], (string)reader["IdObjet"], (string)reader["IdVendeur"], (string)reader["IdAcheteur"], (decimal)reader["PrixAchat"], (decimal)reader["PasDePrix"], (DateTime)reader["DateDepart"], (DateTime)reader["DateFin"], (int)reader["Etat"]));
                }
                reader.Close();
                return list;
            } catch (Exception e) {
                System.Console.WriteLine(e.Message);
            } finally {
                connection.Close();
            }
            return null;
        }

        public static void insertEncher(Encher encher) {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            string request = "INSERT INTO Enchere VALUES ('" + encher.Id.Trim() + "','" + encher.IdObjet.Trim() + "', '" + encher.IdVendeur.Trim() + "', '" + encher.IdAcheteur.Trim() + "', '" + encher.PrixAchat + "', '" + encher.PasDePrix + "', '" + encher.DateDepart.ToString("yyyy-MM-dd") + "', '" + encher.DateFin.ToString("yyyy-MM-dd") + "', '" + encher.Etat + "')";
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

        public static void updateEnchere(Encher en) {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "UPDATE Enchere SET IdAcheteur = '" + en.IdAcheteur + "', PrixAchat = '" + en.PrixAchat + "' WHERE id = '" + en.Id + "'";

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
    }
}