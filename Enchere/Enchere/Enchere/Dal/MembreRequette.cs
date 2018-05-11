using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using Enchere.Models.ViewModel;
using Enchere.Models;

namespace Enchere.Dal {
    public class MembreRequette {
        public static bool Add(Membre u) {
            bool TEST = true;
            byte[] hashPassword = new UTF8Encoding().GetBytes(u.MotDePasse.Trim());
            byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
            string hashString = BitConverter.ToString(hash);
            string chConnexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            string requete = "INSERT INTO Membre(Numero, Civilite, Nom, Prenom, Langage, Telephone, Adresse, Courriel, DateInscri, Cote,  MDP) VALUES ('"  //DateInscri,
            + u.Numero + "', '" + u.Civilite + "', '" + u.Nom + "', '" + u.Prenom + "', '" + u.Langue + "', '"
             + u.Telephone + "', '" + u.Adresse + "', '" + u.Courriel + "', '" + DateTime.Now.ToString("yyyy-MM-dd") +  //HH:mm:ss"
              "', 0" + ", '" + hashString + "')";
            SqlConnection connexion = new SqlConnection(chConnexion);
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            try {
                connexion.Open();
                commande.ExecuteNonQuery();
            }
            catch (Exception e) {
                string msg = e.Message;
                TEST = false;
            }
            finally {
                connexion.Close();
            }
            return TEST;
        }


        public static bool Authentifie(string login, string passwd) {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(connectionString)) {

                string requete = "SELECT * FROM Membre WHERE Courriel = '" + login + "'";

                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    cnx.Open();
                    SqlDataReader dataReader = cmd.ExecuteReader();
                    if (!dataReader.HasRows) {
                        dataReader.Close();
                        return false;
                    }
                    dataReader.Read();

                    var encodedPasswordOnServer = (string)dataReader["MDP"];

                    byte[] encodedPassword = new UTF8Encoding().GetBytes(passwd);
                    byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(encodedPassword);
                    string encodedPasswordSentToForm = BitConverter.ToString(hash);

                    dataReader.Close();
                    return encodedPasswordSentToForm.Trim() == encodedPasswordOnServer.Trim();
                    //return passwd.Trim() == encodedPasswordOnServer.Trim();
                }

                catch (Exception e) {
                    System.Console.WriteLine(e.Message);
                    return false;
                }

                finally {
                    cnx.Close();
                }
            }
        }

        public static Membre GetUserByEmail(string email) {
            Membre user = null;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT * FROM Membre WHERE Courriel = '" + email + "'";
            SqlCommand command = new SqlCommand(request, connection);

            try {
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read()) {
                    user = new Membre {
                        Numero = (string)dr["Numero"],
                        Civilite = (string)dr["Civilite"],
                        Nom = (string)dr["Nom"],
                        Prenom = (string)dr["Prenom"],
                        Langue = (string)dr["Langage"],
                        Telephone = (string)dr["Telephone"],
                        Adresse = (string)dr["Adresse"],
                        Courriel = (string)dr["Courriel"],
                        DateInscri = (DateTime)dr["DateInscri"],
                        Cote = (int)dr["Cote"],
                        MotDePasse = (string)dr["MDP"]

                    };
                }
                dr.Close();
                // connection.Close();//
                return user;
            }
            catch (Exception e) {
                System.Console.WriteLine(e.Message);
                // connection.Close();//
            }
            finally {
                connection.Close();
            }
            return null;
        }




       

        public static bool Update(Membre m) {
            string cStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr)) {
                byte[] hashPassword = new UTF8Encoding().GetBytes(m.MotDePasse.Trim());
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
                string hashString = BitConverter.ToString(hash);

                string requete = "UPDATE Membre SET Civilite = '" + m.Civilite +
                 "', Nom = '" + m.Nom + "', Prenom = '" + m.Prenom + "', Langage = '" + m.Langue
                 + "', Telephone = '" + m.Telephone + "', Adresse = '" + m.Adresse + "', Courriel = '" + m.Courriel
                   + "', MDP = '" + hashString
                 + "' WHERE Numero = '" + m.Numero + "'";

                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e) {
                    System.Console.WriteLine(e.Message);
                    return false;
                }
                finally {
                    cnx.Close();
                }

            }
        }

        public static bool UpdateFromAdmin(Membre m) {
            string cStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr)) {
                string requete = "UPDATE Membre SET Civilite = '" + m.Civilite +
                "', Nom = '" + m.Nom + "', Prenom = '" + m.Prenom + "', Langage = '" + m.Langue
                + "', Telephone = '" + m.Telephone + "', Adresse = '" + m.Adresse
                 + "', Cote = " + m.Cote
                + " WHERE Numero = '" + m.Numero + "'";

                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e) {
                    System.Console.WriteLine(e.Message);
                    return false;
                }
                finally {
                    cnx.Close();
                }

            }
        }



        public static bool UpdateMDP(MembreMDP u, Membre m) {
            string cStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr)) {
                byte[] hashPassword = new UTF8Encoding().GetBytes(u.MotDePasse.Trim());
                byte[] hash = ((HashAlgorithm)CryptoConfig.CreateFromName("MD5")).ComputeHash(hashPassword);
                string hashString = BitConverter.ToString(hash);

                string requete = "UPDATE Membre SET MDP = '" + hashString
                 + "' WHERE Numero = '" + m.Numero + "'";

                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                }
                catch (Exception e) {
                    System.Console.WriteLine(e.Message);
                    return false;
                }
                finally {
                    cnx.Close();
                }

            }
        }

        public static Membre GetUserByNumero(string numero)
        {
            Membre user = null;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT * FROM Membre WHERE Numero = '" + numero + "'";
            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    user = new Membre
                    {
                        Numero = (string)dr["Numero"],
                        Civilite = (string)dr["Civilite"],
                        Nom = (string)dr["Nom"],
                        Prenom = (string)dr["Prenom"],
                        Langue = (string)dr["Langage"],
                        Telephone = (string)dr["Telephone"],
                        Adresse = (string)dr["Adresse"],
                        Courriel = (string)dr["Courriel"],
                        DateInscri = (DateTime)dr["DateInscri"],
                        Cote = (int)dr["Cote"],
                        MotDePasse = (string)dr["MDP"]

                    };
                }
                dr.Close();
                return user;
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

        public static List<Membre> lesMembres(string order) {
            string chConnexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);
           
            string requete = "SELECT * FROM Membre ORDER BY " + order;
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            List<Membre> maListe = new List<Membre>();
            try {
                connexion.Open();
                SqlDataReader dr = commande.ExecuteReader();
                while (dr.Read()) {
                    Membre a = new Membre {
                        Numero = (string)dr["Numero"],
                        Civilite = (string)dr["Civilite"],
                        Nom = (string)dr["Nom"],
                        Prenom = (string)dr["Prenom"],
                        Langue = (string)dr["Langage"],
                        Telephone = (string)dr["Telephone"],
                        Adresse = (string)dr["Adresse"],
                        Courriel = (string)dr["Courriel"],
                        DateInscri = (DateTime)dr["DateInscri"],
                        Cote = (int)dr["Cote"],
                        MotDePasse = (string)dr["MDP"]

                    };
                    maListe.Add(a);
                }

                dr.Close();
                return maListe;
            }
            catch (Exception e) {
                System.Console.WriteLine(e.Message);
            }
            finally {
                connexion.Close();
            }
            return null;
        }

        public static void Supprimer(Membre m) {
            string chConnexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);
            string requete = "DELETE FROM Membre WHERE Numero = '" + m.Numero + "'";
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            try {
                connexion.Open();
                commande.ExecuteNonQuery();
                connexion.Close();
            }
            catch { }
        }

        public static List<Membre> lesMembresRecemmentInscrits(string order)
        {
            string chConnexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);

            string requete = "SELECT * FROM Membre WHERE DateInscri>='" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "' ORDER BY " + order;
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            List<Membre> maListe = new List<Membre>();
            try
            {
                connexion.Open();
                SqlDataReader dr = commande.ExecuteReader();
                while (dr.Read())
                {
                    Membre a = new Membre
                    {
                        Numero = (string)dr["Numero"],
                        Civilite = (string)dr["Civilite"],
                        Nom = (string)dr["Nom"],
                        Prenom = (string)dr["Prenom"],
                        Langue = (string)dr["Langage"],
                        Telephone = (string)dr["Telephone"],
                        Adresse = (string)dr["Adresse"],
                        Courriel = (string)dr["Courriel"],
                        DateInscri = (DateTime)dr["DateInscri"],
                        Cote = (int)dr["Cote"],
                        MotDePasse = (string)dr["MDP"]

                    };
                    maListe.Add(a);
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
    }
}