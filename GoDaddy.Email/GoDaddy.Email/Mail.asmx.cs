using System;
using System.Configuration;
using System.Net.Mail;
using System.Web.Services;

namespace GoDaddy.Email
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
        public string SendEmail(string email, string name, string body)
        {
            try
            {
                var toEmailAddress = ConfigurationManager.AppSettings["ToEmailAddress"].ToString();
                var smtpHost = ConfigurationManager.AppSettings["SMTPHost"].ToString();
                var smtpPort = ConfigurationManager.AppSettings["SMTPPort"].ToString();

                MailMessage mailMessage = new MailMessage();
                mailMessage.To.Add(toEmailAddress);
                mailMessage.From = new MailAddress(email, name);
                mailMessage.Subject = "Contact Us";
                mailMessage.Body = body;
                SmtpClient smtpClient = new SmtpClient(smtpHost, Convert.ToInt32(smtpPort));

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
