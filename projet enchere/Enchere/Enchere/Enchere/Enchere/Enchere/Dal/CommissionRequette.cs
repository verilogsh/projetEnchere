using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Enchere.Models.ViewModel;
using Enchere.Models;
using Enchere.Utility;
using System.Configuration;
using System.Data.SqlClient;

namespace Enchere.Dal {
    public class CommissionRequette {
        public static bool ChangerCommission(string c) {
            int com = Convert.ToInt16(c);
            string cStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr)) {
                string requete = "UPDATE Commission SET Commission = " + com + " WHERE Id = " + 1;


                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                } catch (Exception e) {
                    System.Console.WriteLine(e.Message);
                    return false;
                } finally {
                    cnx.Close();
                }

            }
        }

        public static string ChercherCommission() {
            string c = null;
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection connection = new SqlConnection(connectionString);
            string request = "SELECT * FROM Commission WHERE Id = " + 1;
            SqlCommand command = new SqlCommand(request, connection);

            try {
                connection.Open();
                SqlDataReader dr = command.ExecuteReader();
                while (dr.Read()) {
                    Commission comision = new Commission {
                        Id = (int)dr["Id"],
                        Com = (int)dr["Commission"]

                    };
                    c = (comision.Com).ToString();
                }

                dr.Close();
                // connection.Close();//
                return c;
            } catch (Exception e) {
                System.Console.WriteLine(e.Message);
                // connection.Close();//
            } finally {
                connection.Close();
            }
            return null;
        }

        public static bool insererCommissions(Commissions commi) {
            string cStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection cnx = new SqlConnection(cStr)) {
                string requete = "INSERT INTO Commissions (Taux, Prix, IdEnchere, Date) VALUES (" + commi.Taux + ", '" + commi.Prix + "', '" + commi.IdEnchere + "', '" + commi.Date.ToString("yyyy-MM-dd") + "')";


                SqlCommand cmd = new SqlCommand(requete, cnx);
                cmd.CommandType = System.Data.CommandType.Text;
                try {
                    cnx.Open();
                    cmd.ExecuteNonQuery();
                    return true;

                } catch (Exception e) {
                    System.Console.WriteLine(e.Message);
                    return false;
                } finally {
                    cnx.Close();
                }

            }
        }
    }
}

    
