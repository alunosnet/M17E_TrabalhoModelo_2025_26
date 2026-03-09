using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography;
using System.Web;
using System.Web.SessionState;


public class Helper
{
    public static void enviarMail(string nomeDe, string passwordDe, string para, string assunto, string texto, string anexo = null)
    {
        //objetos mail
        System.Net.Mail.MailMessage mensagem = new System.Net.Mail.MailMessage();
        System.Net.NetworkCredential credenciais = new System.Net.NetworkCredential(nomeDe, passwordDe);
        System.Net.Mail.MailAddress dequem = new System.Net.Mail.MailAddress("exemplo@exemplo.com");// nomeDe);
        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();

        //mensagem
        mensagem.To.Add(para);
        mensagem.From = dequem;
        mensagem.Subject = assunto;
        mensagem.Body = texto;
        mensagem.IsBodyHtml = true;
        //servidor
        smtp.Host = "smtp.mailtrap.io";
        smtp.Port = 2525;
        smtp.EnableSsl = true;
        smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
        smtp.UseDefaultCredentials = false;
        smtp.Credentials = credenciais;

        //anexo
        if (anexo != null && anexo != "")
        {
            if (System.IO.File.Exists(anexo) == true)
            {
                System.Net.Mail.Attachment ficheiroAnexo = new System.Net.Mail.Attachment(anexo);
                mensagem.Attachments.Add(ficheiroAnexo);
            }
        }
        //enviar
        smtp.Send(mensagem);
    }

    // Devolve uma string random
    public static string GenerateAntiForgeryToken()
    {
        using (var rng = new RNGCryptoServiceProvider())
        {
            byte[] randomBytes = new byte[32]; // 256-bit token
            rng.GetBytes(randomBytes);
            return Convert.ToBase64String(randomBytes);
        }
    }
    public static bool ValidateAntiForgeryToken(HttpSessionState session,string requestToken)
    {
        // Retrieve token from session and request
        string sessionToken = session["AntiForgeryToken"] as string;

        // Validar
        if (string.IsNullOrEmpty(sessionToken) ||
            string.IsNullOrEmpty(requestToken) ||
            !sessionToken.Equals(requestToken, StringComparison.Ordinal))
        {
            return false;
        }

        // Gerar um novo
        string newToken = GenerateAntiForgeryToken();
        session["AntiForgeryToken"] = newToken;
        return true;
    }
}
