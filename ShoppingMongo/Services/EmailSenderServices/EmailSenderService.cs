using MailKit.Net.Smtp;
using MimeKit;
using Microsoft.Extensions.Options;
using ShoppingMongo.Settings;
using ShoppingMongo.Entities;

namespace ShoppingMongo.Services
{
    public class EmailSenderService
    {
        private readonly MailSetting _mailSettings;

        public EmailSenderService(IOptions<MailSetting> mailSettings)
        {
            _mailSettings = mailSettings.Value;
        }

        public async Task SendEmailAsync(string to, string subject, string body)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_mailSettings.Email));
            email.To.Add(MailboxAddress.Parse(to));
            email.Subject = subject;
            email.Body = new TextPart("plain") { Text = body };

            using var smtp = new SmtpClient();
            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }

        public async Task<string> SendDiscountEmailAsync(string to)
        {
            try
            {
                var random = new Random();
                var couponCode = random.Next(100000, 999999).ToString();

                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_mailSettings.Email));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = "%20 İndirim Kuponunuz!";
                email.Body = new TextPart("plain")
                {
                    Text = $"Merhaba,\n\nHesabınıza %20 indirim kuponu tanımlanmıştır.\nKupon Kodunuz: {couponCode}\n\nİyi alışverişler diler, size verdiğimiz değeri belirtmek isteriz.\n\nSaygılarımızla,\nShoppingMongo App"
                };


                using var smtp = new SmtpClient();
                await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, MailKit.Security.SecureSocketOptions.StartTls);
                await smtp.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);
                await smtp.SendAsync(email);
                await smtp.DisconnectAsync(true);

                return couponCode; // Başarılıysa kodu döndür
            }
            catch (Exception ex)
            {
                // Hata loglanabilir
                Console.WriteLine($"Mail gönderim hatası: {ex.Message}");
                return string.Empty; // veya null ya da özel bir hata mesajı
            }
        }


    }
}
