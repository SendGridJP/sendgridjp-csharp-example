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
            String apiKey = ConfigurationManager.AppSettings["API_KEY"];
            List<String> tos = new List<String>(ConfigurationManager.AppSettings["TOS"].Split(','));
            String from = ConfigurationManager.AppSettings["FROM"];

            var email = new SendGrid.SendGridMessage();
            email.AddTo(from);          // 実際にはこのアドレスには送信されない。エラー回避のため記載。
            email.Header.SetTo(tos);    // 宛先はこちらで指定したものが使用される
            email.From = new MailAddress(from, "送信者名");
            email.Subject = "[sendgrid-c#-example] フクロウのお名前はfullnameさん";
            email.Text = "familyname さんは何をしていますか？\r\n 彼はplaceにいます。";
            email.Html = "<strong> familyname さんは何をしていますか？</strong><br />彼はplaceにいます。";
            email.AddSubstitution("fullname", new List<String>() { "田中 太郎", "佐藤 次郎", "鈴木 三郎" });
            email.AddSubstitution("familyname", new List<String>() { "田中", "佐藤", "鈴木" });
            email.AddSubstitution("place", new List<String>() { "office", "home", "office" });
            email.AddSection("office", "中野");
            email.AddSection("home", "目黒");
            email.SetCategory("category1");
            email.AddAttachment(@"..\..\gif.gif");

            var web = new Web(apiKey);
            var task = web.DeliverAsync(email);
            task.Wait();
        }
    }
}
