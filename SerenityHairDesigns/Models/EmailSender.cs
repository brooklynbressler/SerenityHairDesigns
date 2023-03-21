using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using SerenityHairDesigns.Models;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNetCore.Http;

namespace SerenityHairDesigns
{
	public class EmailSender
	{

		public string strSendingEmail = "tywetterich@gmail.com";
		public string strEmailPassword = "fjfqygtjsrkwppec";
		public int intPort = 587;
		public string strHost = "smtp.gmail.com";

		public void SendEmail(Resume Resume, IFormFile File)
		{
			try
			{

				MailMessage objMailMessage = new MailMessage();
				objMailMessage.From = new MailAddress(strSendingEmail);
				objMailMessage.To.Add("tywetterich@gmail.com");
				objMailMessage.Subject = "Resume";

				//var stream = File.OpenReadStream();
				//string FileName = File.FileName;
				//var attachment1 = new Attachment(stream, FileName);
				//objMailMessage.Attachments.Add(attachment1);

				objMailMessage.Body = "This is an application submitted from someone on the website. Here is their info and resume." +
					" <br/> <br/> First Name: "	+ Resume.strFirstName
					+ "<br/>" + "Last Name: " + Resume.strLastName 
					+ "<br/> Phone Number: " + Resume.strPhoneNumber 
					+ "<br/> Email Address: " + Resume.strEmailAddress;

				objMailMessage.IsBodyHtml = true;

				SmtpClient objsmtpClient = new SmtpClient();
				objsmtpClient.UseDefaultCredentials = false;
				objsmtpClient.Credentials = new System.Net.NetworkCredential(strSendingEmail, strEmailPassword);
				objsmtpClient.Port = intPort;
				objsmtpClient.Host = strHost;
				objsmtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
				objsmtpClient.EnableSsl = true;

				objsmtpClient.Send(objMailMessage);

			}
			catch (Exception ex)
			{

			}

		}

	}
}
