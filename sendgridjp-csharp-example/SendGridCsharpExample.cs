using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using SendGrid;

namespace sendgridjp_csharp_example
{
    class SendGridCsharpExample
    {
        static void Main(string[] args)
        {
            String sendGridUserName = ConfigurationManager.AppSettings["SENDGRID_USERNAME"];
            String sendGridPassword = ConfigurationManager.AppSettings["SENDGRID_PASSWORD"];
            List<String> tos = new List<String>(ConfigurationManager.AppSettings["TOS"].Split(','));
            String from = ConfigurationManager.AppSettings["FROM"];

            var smtpapi = new SendGrid.SmtpApi.Header();
            smtpapi.SetTo(tos);
            smtpapi.AddSubstitution("fullname", new List<String>() { "田中 太郎", "佐藤 次郎", "鈴木 三郎" });
            smtpapi.AddSubstitution("familyname", new List<String>() { "田中", "佐藤", "鈴木" });
            smtpapi.AddSubstitution("place", new List<String>() { "office", "home", "office" });
            smtpapi.AddSection("office", "中野");
            smtpapi.AddSection("home", "目黒");
            smtpapi.SetCategory("カテゴリ1");

            var email = new SendGrid.SendGridMessage();
            email.AddTo(from);  // SmtpapiのSetTo()を使用しているため、実際にはこのアドレスにはメールは送信されない
            email.From = new MailAddress(from, "送信者名");
            email.Subject = "[sendgrid-c#-example] フクロウのお名前はfullnameさん";
            email.Text = "familyname さんは何をしていますか？\r\n 彼はplaceにいます。";
            email.Html = "<strong> familyname さんは何をしていますか？</strong><br />彼はplaceにいます。";
            email.Headers.Add("X-Smtpapi", smtpapi.JsonString());
            email.AddAttachment(@"..\..\gif.gif");

            var credentials = new NetworkCredential(sendGridUserName, sendGridPassword);
            var web = new Web(credentials);
            web.Deliver(email);
        }
    }
}
