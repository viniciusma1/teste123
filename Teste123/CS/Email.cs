using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace Teste123
{
    public static class Email
    {
        public static void EnviarEmail(string De, string Para, string Assunto, string Corpo)
        {
            if (De != string.Empty && Para != string.Empty)
            {
                MailMessage mail = new MailMessage();
                mail.IsBodyHtml = true;
                mail.From = new MailAddress(De);
                mail.To.Add(Para);
                mail.Subject = Assunto;
                mail.Body = Corpo;
                mail.Priority = MailPriority.High;
                SmtpClient smtp = new SmtpClient("hdipost", 25);
                smtp.Send(mail);
            }
        }

    }
}