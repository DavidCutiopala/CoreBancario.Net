using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Configuration;
using VotoElectronico.Generico;
using VotoElectronico.Mailer.Recursos;

namespace VotoElectronico.Mailer
{
    public class EnvioEmail : IEnvioEmail
    {
        public DtoGenerico EnviarEmail(string destino, string asunto, string html)
        {
            var sender = ConfigurationManager.AppSettings["sender"];
            var apiKey = ConfigurationManager.AppSettings["SENDGRID_API_KEY"];
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress(sender, "EVOTE EPN");
            var subject = asunto;
            var to = new EmailAddress(destino);
            var htmlContent = html;
            var msg = MailHelper.CreateSingleEmail(from, to, subject, "", htmlContent);
            var response = client.SendEmailAsync(msg).Result;
            string resultadoPost = response.Body.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<DtoGenerico>(resultadoPost);
        }

        public string ActivarUsuarioGenerico(Dictionary<string, string> datos)
             => ParseTemplate(TemplatesVoto.UsuarioNuevo.ToString(), datos);
        public string CambioClaveUsuario(Dictionary<string, string> datos)
             => ParseTemplate(TemplatesVoto.UsuarioCambioClave.ToString(), datos);

        /// <summary>
        /// Parse Template
        /// </summary>
        /// <param name="aTemplatePath"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string ParseTemplate(string aTemplatePath, Dictionary<string, string> parameters)
        {
            try
            {
                foreach (KeyValuePair<string, string> entry in parameters)
                {
                    aTemplatePath = aTemplatePath.Replace("{" + entry.Key + "}", entry.Value);
                }
                return aTemplatePath;
            }
            catch (Exception e)
            {
                Console.Write(e.StackTrace);
                throw;
            }
        }
    }
}