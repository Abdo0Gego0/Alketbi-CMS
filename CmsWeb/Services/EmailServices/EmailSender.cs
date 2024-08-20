using Azure.Core;
using Microsoft.AspNetCore.Identity.UI.Services;
using System.Net;
using System.Net.Mail;

namespace CmsWeb.Services.EmailServices
{
	public class EmailSender : IEmailSender
	{

		private readonly IConfiguration _config;

		public EmailSender(IConfiguration config)
		{
			_config = config;
		}

		public Task SendEmailAsync(string email, string subject, string htmlMessage)
		{

			MailMessage msg = null;


			SmtpClient client = new SmtpClient
			{
				Port = int.Parse(_config.GetSection("CmsMail:EmailPort").Value),
				Host = _config.GetSection("CmsMail:EmailHost").Value,
				//EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential(_config.GetSection("CmsMail:EmailUsername").Value, _config.GetSection("CmsMail:EmailPassword").Value)
			};



			msg = new MailMessage(_config.GetSection("CmsMail:EmailUsername").Value, email, subject, htmlMessage);

			msg.IsBodyHtml = true;

			return client.SendMailAsync(msg);

		}

		public Task SendEmailAsyncOLD(string email, string subject, string htmlMessage)
		{
			SmtpClient client = new SmtpClient
			{
				Port = 587,
				Host = "smtp.gmail.com", //or another email sender provider
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
				Credentials = new NetworkCredential("your email sender", "password")
			};

			return client.SendMailAsync("your email sender", email, subject, htmlMessage);
		}

	}
}
