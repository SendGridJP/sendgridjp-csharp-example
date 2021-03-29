using System;
using System.Linq;
using System.Collections.Generic;
using SendGrid.Helpers.Mail;
using SendGrid;
using System.Threading.Tasks;
using System.IO;

namespace sendgridjp_csharp_example
{
    class SendGridCsharpExample
    {
        static async Task Main(string[] args)
        {
            DotNetEnv.Env.Load(".env");
            var apiKey = Environment.GetEnvironmentVariable("API_KEY");
            var tos = Environment.GetEnvironmentVariable("tos").Split(',').Select(to => new EmailAddress(to)).ToList();
            var from = new EmailAddress(Environment.GetEnvironmentVariable("from"), "送信者名");

            var subject = "[sendgrid-c#-example] フクロウのお名前はfullnameさん";
            var plainTextContent = "familyname さんは何をしていますか？\r\n 彼はplaceにいます。";
            var htmlContent = "<strong> familyname さんは何をしていますか？</strong><br />彼はplaceにいます。";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, htmlContent);
            msg.AddSubstitutions(new Dictionary<string, string> {{"fullname", "田中 太郎"}, {"familyname", "田中"}, {"place", "中野"}}, 0);
            msg.AddSubstitutions(new Dictionary<string, string> {{"fullname", "佐藤 次郎"}, {"familyname", "佐藤"}, {"place", "目黒"}}, 1);
            msg.AddSubstitutions(new Dictionary<string, string> {{"fullname", "鈴木 三郎"}, {"familyname", "鈴木"}, {"place", "中野"}}, 2);
            msg.AddCategory("category1");
            msg.AddHeader("X-Sent-Using", "SendGrid-API");
            var image = Convert.ToBase64String(File.ReadAllBytes("gif.gif"));
            msg.AddAttachment("owl.gif", image, "image/gif", "attachment");

            var client = new SendGridClient(apiKey);
            await client.SendEmailAsync(msg);
        }
    }
}
