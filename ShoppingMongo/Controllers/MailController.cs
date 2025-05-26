using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using ShoppingMongo.Services;

namespace ShoppingMongo.Controllers
{
    public class MailController : Controller
    {
        private readonly EmailSenderService _emailSender;

        public MailController(EmailSenderService emailSender)
        {
            _emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> SendMail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return BadRequest("Email boş olamaz.");

            var code = await _emailSender.SendDiscountEmailAsync(email);
            return Ok($"Kupon kodunuz başarıyla gönderildi! {email} adresinizi kontrol etmeyi unutmayın.");

        }



    }
}
