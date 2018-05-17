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
            string request = "UPDATE Enchere SET IdAcheteur = '" + en.IdAcheteur + "', PrixAchat = '" + en.PrixAchat + "', Etat = " + en.Etat + " WHERE id = '" + en.Id + "'";

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

        public static void insertHistorique(Historique his) {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);

            string request = "INSERT INTO Historique (IdMembre, IdEnchere, Prix, Date) VALUES ('" + his.IdMembre + "', '" + his.IdEnchere + "', '" + his.Prix + "', '" + his.Date.ToString("yyyy-MM-dd") + "')";
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


        public static List<Encher> getEncheresRapport3()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "";
           
                request = "SELECT * FROM  Enchere WHERE Etat != 0 OR ( Etat = 0 AND DateFin = '" + DateTime.Now.AddDays(1) +"')";
            
            List<Encher> list = new List<Encher>();


            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    list.Add(new Encher((string)reader["Id"], (string)reader["IdObjet"], (string)reader["IdVendeur"], (string)reader["IdAcheteur"], (decimal)reader["PrixAchat"], (decimal)reader["PasDePrix"], (DateTime)reader["DateDepart"], (DateTime)reader["DateFin"], (int)reader["Etat"]));
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


        public static List<SyntheseVentes> getSynthese(List<Commissions> listComs)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "";
            Decimal TotalVente = 0;
            Decimal TotalCom = 0;
            
            List<SyntheseVentes> maListe =new List<SyntheseVentes>() ;
            
            foreach (Commissions s in listComs)
            {
                connection.Open();
                try
                {
                    request = "SELECT  o.Nom FROM Enchere e INNER JOIN Objet o ON o.Id = e.IdObjet WHERE e.Id= '" + s.IdEnchere + "' AND e.Etat !=0 AND e.Etat != 2";


                    SqlCommand command = new SqlCommand(request, connection);
                    SqlDataReader reader = command.ExecuteReader();

                    if (reader.Read())
                    {
                        TotalVente += s.Prix;
                        TotalCom += (s.Prix * s.Taux) / 100;
                        SyntheseVentes v = new SyntheseVentes((string)reader["Nom"], (decimal)s.Prix, s.Date, (decimal)(s.Prix * s.Taux) / 100, (decimal)TotalVente, (decimal)TotalCom);
                        maListe.Add(v);
                    }
                    reader.Close();
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
         
            
            return maListe;
        }
    }
}