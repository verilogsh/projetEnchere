﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enchere.Models;
using Enchere.Models.ViewModel;
using System.Configuration;
using System.Data.SqlClient;


namespace Enchere.Dal {
    public class ObjetRequette {
        public static List<Categorie> getCategorie()
        {
            List<Categorie> ctg = new List<Categorie>();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT * FROM Categorie";
            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ctg.Add(new Categorie((string)reader["Id"], (string)reader["Nom"], (string)reader["Description"]));
                }
                reader.Close();
                return ctg;
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

        public static List<ObjetEnchereAff> getObjetEnVente(string idCategorie)
        {
            List<ObjetEnchereAff> obj = new List<ObjetEnchereAff>();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request;
            if (idCategorie == "0")
            {
                request = "SELECT o.Id, e.Id IdEnchere, o.Nom, o.Description, o.IdCategorie, o.Photo, o.Piece, o.IdMembre IdVendeur, e.IdAcheteur, o.PrixDepart, e.PrixAchat, e.DateDepart, e.DateFin, e.PasDePrix FROM Enchere e INNER JOIN Objet o ON o.Id = e.IdObjet WHERE e.Etat = 0";
            }
            else
            {
                request = "SELECT o.Id, e.Id IdEnchere, o.Nom, o.Description, o.IdCategorie, o.Photo, o.Piece, o.IdMembre IdVendeur, e.IdAcheteur, o.PrixDepart, e.PrixAchat, e.DateDepart, e.DateFin, e.PasDePrix FROM Enchere e INNER JOIN Objet o ON o.Id = e.IdObjet WHERE o.IdCategorie = '" + idCategorie + "' WHERE e.Etat = 0";
            }

            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    obj.Add(new ObjetEnchereAff((string)reader["Id"], (string)reader["IdEnchere"], (string)reader["Nom"], (string)reader["Description"], (string)reader["IdCategorie"], (string)reader["photo"], (string)reader["piece"], (string)reader["IdVendeur"], (string)reader["IdAcheteur"], (decimal)reader["PrixDepart"], (decimal)reader["PrixAchat"], (DateTime)reader["DateDepart"], (DateTime)reader["DateFin"], (decimal)reader["PasDePrix"]));
                }
                reader.Close();
                return obj;
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

        public static void insertObjet(Objet obj)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            int n, m;
            if (obj.Nouveau)
            {
                n = 1;
            }
            else
            {
                n = 0;
            }
            if (obj.EnVent)
            {
                m = 1;
            }
            else
            {
                m = 0;
            }

            string request = "INSERT INTO Objet VALUES ('" + obj.Id + "',' " + obj.Nom + "', '" + obj.Description + "', '" + obj.DateInscri.ToString("yyyy-MM-dd") + "', " + obj.PrixDepart + ", '" + obj.IdCategorie + "', '" + obj.IdMembre + "', '" + obj.Piece + "', '" + obj.Photo + "', " + n + ", " + m + ")";
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



        public static void updateObjet(ObjetViewModel model)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "";
            if (model.Photo == null && model.Piece == null)
            {
                request = "UPDATE objet SET Nom = '" + model.Nom + "', Description = '" + model.Description + "', PrixDepart = '" + model.PrixDepart + "', IdCategorie = '" + model.Categorie + "' WHERE Id = '" + model.Id + "'";
            }
            else if (model.Photo != null && model.Piece != null)
            {
                request = "UPDATE objet SET Nom = '" + model.Nom + "', Description = '" + model.Description + "', PrixDepart = '" + model.PrixDepart + "', IdCategorie = '" + model.Categorie + "', Photo = '" + model.Photo.FileName + "', Piece = '" + model.Piece.FileName + "' WHERE Id = '" + model.Id + "'";
                SavePhoto(model.Photo);
                SavePiece(model.Piece);
            }
            else if ((model.Photo != null && model.Piece == null))
            {
                request = "UPDATE objet SET Nom = '" + model.Nom + "', Description = '" + model.Description + "', PrixDepart = '" + model.PrixDepart + "', IdCategorie = '" + model.Categorie + "', Photo = '" + model.Photo.FileName + "' WHERE Id = '" + model.Id + "'";
                SavePhoto(model.Photo);
            }
            else
            {
                request = "UPDATE objet SET Nom = '" + model.Nom + "', Description = '" + model.Description + "', PrixDepart = '" + model.PrixDepart + "', IdCategorie = '" + model.Categorie + "', Piece = '" + model.Piece.FileName + "' WHERE Id = '" + model.Id + "'";
                SavePiece(model.Piece);
            }

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

        public static List<Objet> getObjetMembre(string courriel, string idCateg,  string ordre)
        {
            Membre mb = new Membre();
            mb = MembreRequette.GetUserByEmail(courriel);
            List<Objet> obj = new List<Objet>();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request;
            if (idCateg == "0") {
                request = "SELECT o.Id, o.Nom, o.Description, o.DateInscri, c.Nom Categorie, o.Photo, o.Piece, o.IdMembre, o.Nouveau, o.EnVente, o.PrixDepart FROM Objet o, Categorie c WHERE o.IdMembre = '" + mb.Numero.Trim() + "' AND c.Id = o.IdCategorie";
            } else {
                request = "SELECT o.Id, o.Nom, o.Description, o.DateInscri, c.Nom Categorie, o.Photo, o.Piece, o.IdMembre, o.Nouveau, o.EnVente, o.PrixDepart FROM Objet o, Categorie c WHERE o.IdMembre = '" + mb.Numero.Trim() + "' AND c.Id = o.IdCategorie AND c.Id = '" + idCateg;
            }


            SqlCommand command = new SqlCommand(request, connection);

            try {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read()) {
                    obj.Add(new Objet((string)reader["Id"], (string)reader["Nom"], (string)reader["Description"], (DateTime)reader["DateInscri"], (string)reader["Categorie"], (string)reader["photo"], (string)reader["piece"], (string)reader["IdMembre"], (bool)reader["Nouveau"], (bool)reader["EnVente"], (decimal)reader["PrixDepart"]));
                }
                reader.Close();
                return obj;
            } catch (Exception e) {
                System.Console.WriteLine(e.Message);
            } finally {
                connection.Close();
            }
            return null;
        }

        public static Objet getObjetById(string Id)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT * FROM Objet WHERE Id = '" + Id.Trim() + "'";


            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Objet obj = null;
                if (reader.Read())
                {
                    obj = new Objet((string)reader["Id"], (string)reader["Nom"], (string)reader["Description"], (DateTime)reader["DateInscri"], (string)reader["IdCategorie"], (string)reader["photo"], (string)reader["piece"], (string)reader["IdMembre"], (bool)reader["Nouveau"], (bool)reader["EnVente"], (decimal)reader["PrixDepart"]);
                }
                reader.Close();
                return obj;
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



        public static Objet deleteObjetById(string Id)
        {

            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "DELETE FROM Objet WHERE Id = '" + Id.Trim() + "'";

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
            return null;
        }

 

        public static void setObjetEnVente(string id, int op) {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "UPDATE objet SET EnVente = " + op + " WHERE Id ='" + id + "'";

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

        public static void setObjetOwner(string id, string IdMembre) {
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request = "UPDATE objet SET IdMembre = " + IdMembre + " WHERE Id ='" + id + "'";

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

        public static void SavePhoto(HttpPostedFileBase file)
        {
            if (file != null && !string.IsNullOrEmpty(file.FileName))
            {
                string fullPath = System.Web.Hosting.HostingEnvironment.MapPath("~/images/" + file.FileName);
                file.SaveAs(fullPath);
            }
        }

        public static void SavePiece(HttpPostedFileBase file)
        {
            if (file != null && !string.IsNullOrEmpty(file.FileName))
            {
                string fullPath = System.Web.Hosting.HostingEnvironment.MapPath("~/pieces/" + file.FileName);
                file.SaveAs(fullPath);
            }
        }

        ////////////////////////////////////////////////// End of Haiqiang XU  /////////////////////////////////////////////////////////////


        public static List<Objet> lesProduitsRecemmentInscrits(string order)
        {
            string chConnexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);
            //string idMmebre = "SELECT Id FROM Mmembre WHERE Courriel = '" + User.Identity.Name + "'";
            //string requete = "SELECT * FROM Objet WHERE IdMembre = '" + idMmebre + "'";


            //string requete = "select * from Objet where Id IN (select IdObjet from Enchere where IdMembre = '" + idMmebre + "')"
            //select IdObjet from Enchere where IdMembre = '" + idMmebre + "'";
            string requete = "SELECT * FROM Objet WHERE DateInscri>='" + DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd") + "' ORDER BY " + order;
            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            List<Objet> maListe = new List<Objet>();
            try
            {
                connexion.Open();
                SqlDataReader dr = commande.ExecuteReader();
                while (dr.Read())
                {
                    Objet o = new Objet
                    {
                        Id = (string)dr["Id"],
                        Nom = (string)dr["Nom"],
                        Description = (string)dr["Description"],
                        DateInscri = (DateTime)dr["DateInscri"],
                        IdCategorie = (string)dr["IdCategorie"],
                        Photo = (string)dr["Photo"],
                        Piece = (string)dr["Piece"],
                        IdMembre = (string)dr["IdMembre"],
                        Nouveau = (bool)dr["Nouveau"],
                        EnVent = (bool)dr["EnVente"],
                        PrixDepart = (decimal)dr["PrixDepart"]

                    };
                    maListe.Add(o);
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

        public static List<HistoEncheObjet> lesProduitsInteressants(string idMembre,string courriel)
        {
            string chConnexion = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connexion = new SqlConnection(chConnexion);

            string requete = "SELECT DISTINCT IdEnchere FROM Historique WHERE  IdMembre ='" + idMembre + "'";
               


            SqlCommand commande = new SqlCommand(requete, connexion);
            commande.CommandType = System.Data.CommandType.Text;
            List<Historique> maListe = new List<Historique>();
            try
            {
                connexion.Open();
                SqlDataReader dr = commande.ExecuteReader();
                while (dr.Read())
                {
                     
                    Historique o = new Historique(
                    
                        0,
                        (string)idMembre,
                         (string)dr["IdEnchere"],
                        10,
                        DateTime.Now
                 
                    );
                    maListe.Add(o);
                }

                dr.Close();
                List < HistoEncheObjet > listHisto= new List<HistoEncheObjet>();
                foreach (Historique en in maListe)
                {
                    listHisto.Add(getHistoEncherObjet(en.IdEnchere)); 
                  
                }




                return listHisto;
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


        public static HistoEncheObjet getHistoEncherObjet(string idEnchere)
        {
            List<HistoEncheObjet> obj = new List<HistoEncheObjet>();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request;
            
                request = "SELECT o.Id, e.Id IdEnchere, o.Nom, o.Description, o.IdCategorie, o.Photo, o.Piece, o.IdMembre IdVendeur, e.IdAcheteur, o.PrixDepart, e.PrixAchat, e.DateDepart, e.DateFin, e.Etat FROM Enchere e INNER JOIN Objet o ON o.Id = e.IdObjet WHERE e.Id= '"+idEnchere+"'";
            
            

            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
               if (reader.Read())
                {
                    return new HistoEncheObjet((string)reader["Id"], (string)reader["IdEnchere"], (string)reader["Nom"], (string)reader["Description"], (string)reader["IdCategorie"], (string)reader["photo"], (string)reader["piece"], (string)reader["IdVendeur"], (string)reader["IdAcheteur"], (decimal)reader["PrixDepart"], (decimal)reader["PrixAchat"], (DateTime)reader["DateDepart"], (DateTime)reader["DateFin"], (int)reader["Etat"]);
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
            return null;
        }

    }
}