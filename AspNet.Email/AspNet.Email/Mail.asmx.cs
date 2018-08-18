using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Services;

namespace AspNet.Email
{
    /// <summary>
    /// Summary description for Mail
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    [System.Web.Script.Services.ScriptService]
    public class Mail : System.Web.Services.WebService
    {
        [WebMethod]
        public string SendEmail(string to, string subject, string body)
        {
            try
            {
                var fromEmailAddress = ConfigurationManager.AppSettings["FromEmailAddress"].ToString();
                var fromEmailDisplayName = ConfigurationManager.AppSettings["FromEmailDisplayName"].ToString();
                var fromEmailPassword = ConfigurationManager.AppSettings["FromEmailPassword"].ToString();
                var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(to);
                mailMessage.From = new MailAddress(fromEmailAddress, fromEmailDisplayName);
                mailMessage.Subject = subject;
                mailMessage.Body = body;
                SmtpClient smtpClient = new SmtpClient(smtpHost, Convert.ToInt32(smtpPort));
                smtpClient.EnableSsl = true;
                smtpClient.Credentials = new System.Net.NetworkCredential(fromEmailAddress, fromEmailPassword);

                smtpClient.Send(mailMessage);
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
