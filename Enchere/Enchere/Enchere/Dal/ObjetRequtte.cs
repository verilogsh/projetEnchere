using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enchere.Models;
using Enchere.Models.ViewModel;
using System.Configuration;
using System.Data.SqlClient;


namespace Enchere.Dal {
    public class ObjetRequtte {
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

        public static List<ObjetEnchereAff> getObjetEnVente(int idCategorie)
        {
            List<ObjetEnchereAff> obj = new List<ObjetEnchereAff>();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request;
            if (idCategorie == 0)
            {
                request = "SELECT o.Id, e.Id IdEnchere, o.Nom, o.Description, o.IdCategorie, o.Photo, o.Piece, o.IdMembre IdVendeur, e.IdMembre IdAcheteur, o.PrixDepart, e.PrixAchat, e.DateDepart, e.DureeVente, e.PasDePrix FROM Enchere e INNER JOIN Objet o ON o.Id = e.IdObjet";
            }
            else
            {
                request = "SELECT o.Id, e.Id IdEnchere, o.Nom, o.Description, o.IdCategorie, o.Photo, o.Piece, o.IdMembre IdVendeur, e.IdMembre IdAcheteur, o.PrixDepart, e.PrixAchat, e.DateDepart, e.DureeVente, e.PasDePrix FROM Enchere e INNER JOIN Objet o ON o.Id = e.IdObjet WHERE o.IdCategorie = " + idCategorie;
            }

            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    obj.Add(new ObjetEnchereAff((string)reader["Id"], (string)reader["IdEnchere"], (string)reader["Nom"], (string)reader["Description"], (string)reader["IdCategorie"], (string)reader["photo"], (string)reader["piece"], (string)reader["IdVendeur"], (string)reader["IdAcheteur"], (decimal)reader["PrixDepart"], (decimal)reader["PrixAchat"], (DateTime)reader["DateDepart"], (int)reader["DureeVente"], (decimal)reader["PasDePrix"]));
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

            obj.IdCategorie = "kkk";
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

        public static List<Objet> getObjetMembre(string courriel, string ordre)
        {
            Membre mb = new Membre();
            mb = MembreRequette.GetUserByEmail(courriel);
            List<Objet> obj = new List<Objet>();
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection connection = new SqlConnection(connectionString);
            string request;
            if (ordre == "none")
            {
                request = "SELECT o.Id, o.Nom, o.Description, o.DateInscri, c.Nom Categorie, o.Photo, o.Piece, o.IdMembre, o.Nouveau, o.EnVente, o.PrixDepart FROM Objet o, Categorie c WHERE o.IdMembre = '" + mb.Numero.Trim() + "' AND c.Id = o.IdCategorie";
            }
            else
            {
                request = "SELECT o.Id, o.Nom, o.Description, o.DateInscri, c.Nom Categorie, o.Photo, o.Piece, o.IdMembre, o.Nouveau, o.EnVente, o.PrixDepart FROM Objet o, Categorie c WHERE o.IdMembre = '" + mb.Numero.Trim() + "' AND c.Id = o.IdCategorie ORDER BY " + ordre;
            }


            SqlCommand command = new SqlCommand(request, connection);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    obj.Add(new Objet((string)reader["Id"], (string)reader["Nom"], (string)reader["Description"], (DateTime)reader["DateInscri"], (string)reader["Categorie"], (string)reader["photo"], (string)reader["piece"], (string)reader["IdMembre"], (bool)reader["Nouveau"], (bool)reader["EnVente"], (decimal)reader["PrixDepart"]));
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
    }
}