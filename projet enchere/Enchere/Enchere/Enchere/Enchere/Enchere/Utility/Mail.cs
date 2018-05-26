using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Enchere.Utility {
    public class Mail {
        public static void SendEmail(string nom, decimal prix, String emetteur) {
            try {
                var senderemail = new MailAddress("findetude000@gmail.com", "Demo test");
                var receiverEmail = new MailAddress(emetteur, "recepteur");

                var password = "test201805";
                var sub = "Produit " + nom;
                var body = "Veuillez etre courant que votre prix pour le produit " + nom + " est depasse. Maintenant le prix est " + prix + "$.";

                var smtp = new SmtpClient {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new System.Net.NetworkCredential(senderemail.Address, password)
                };
                using (var mess = new MailMessage(senderemail, receiverEmail) {
                    Subject = sub,
                    Body = body
                }) {
                    smtp.Send(mess);
                }
            } catch (Exception ex) {
                // Status.Text = ex.Message;

            }
        }
    }
}