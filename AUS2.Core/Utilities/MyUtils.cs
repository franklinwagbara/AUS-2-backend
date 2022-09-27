using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Drawing;
using ErrorEventArgs = Newtonsoft.Json.Serialization.ErrorEventArgs;
using QRCoder;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using AUS2.Core.DBObjects;
using System.Threading;

namespace AUS2.Core.Utilities
{
    public static class MyUtils
    {
        public static string Stringify(this object any) => JsonConvert.SerializeObject(any);

        public static string GetValue(this Dictionary<string, string> dic, string key)
            => dic.FirstOrDefault(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase)).Value;

        public static object GetValue(this Dictionary<string, object> dic, string key)
            => dic.FirstOrDefault(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase)).Value;

        public static TOut Parse<TOut>(this string any) =>
            JsonConvert.DeserializeObject<TOut>(any, new JsonSerializerSettings
            {
                Error = delegate (object sender, ErrorEventArgs args)
                {
                    args.ErrorContext.Handled = true;
                },
                Converters = { new IsoDateTimeConverter() }
            });

        public static async Task<HttpResponseMessage> Send(string url, HttpRequestMessage message)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls
                                                   | SecurityProtocolType.Tls11
                                                   | SecurityProtocolType.Tls12;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(url);
                return await client.SendAsync(message);
            }
        }

        public static string GenerateSha512(this string inputString)
        {
            SHA512 sha512 = SHA512.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(inputString);
            byte[] hash = sha512.ComputeHash(bytes);
            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }

            return sb.ToString();
        }

        public static string RefrenceCode(object newlock)
        {
            lock (newlock)
            {
                Thread.Sleep(1000);
                ulong random = ulong.Parse(DateTime.Now.ToString("MMddyyHHmmss"));
                return $"541{random:D12}";
            }
            //var bytes = new byte[8];
            //var rng = RandomNumberGenerator.Create();
            //rng.GetBytes(bytes);
            //ulong random = BitConverter.ToUInt64(bytes, 0) % 1000000000000;
            //return $"541{random:D12}";
        }

        public static void SendMail(Dictionary<string, string> mailsettings, string toEmail, string subject, string body, string bcc = null)
        {
            var credentials = new NetworkCredential(mailsettings.GetValue("UserName"), mailsettings.GetValue("Password"));
            var smtp = new SmtpClient(mailsettings.GetValue("Host"), int.Parse(mailsettings.GetValue("Port")))
            {
                EnableSsl = bool.Parse(mailsettings.GetValue("UseSsl")),
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = credentials
            };


            var mail = new MailMessage { From = new MailAddress(mailsettings.GetValue("Sender")) };
            mail.To.Add(new MailAddress(toEmail));

            if (!string.IsNullOrEmpty(bcc))
            {
                var copies = bcc.Split(',');
                foreach (var email in copies)
                    mail.Bcc.Add(new MailAddress(email));
            }

            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            smtp.Send(mail);
        }

        public static string ReadTextFile(string webrootpath, string filename)
        {
            string body;
            using (var sr = new StreamReader($"{webrootpath}\\App_Data\\Templates\\{filename}"))
            {
                body = sr.ReadToEndAsync().Result;
            }
            return body;
        }

        private static Byte[] BitmapToBytes(Bitmap img)
        {
            using MemoryStream stream = new MemoryStream();
            img.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
            return stream.ToArray();
        }

        public static Byte[] GenerateQrCode(string url)
        {
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeData qrCodeData = qrGenerator.CreateQrCode(url, QRCodeGenerator.ECCLevel.Q);
            QRCode qrCode = new QRCode(qrCodeData);
            Bitmap qrCodeImage = qrCode.GetGraphic(20);
            var imageResult = BitmapToBytes(qrCodeImage);
            return imageResult;
        }

        public static string CreateRandomPasswordOld(int length = 8)
        {
            // Create a string of characters, numbers, special characters that allowed in the password  
            string validChars = "ABCDEFGHJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*?_-";
            Random random = new Random();

            // Select one random character at a time from the string  
            // and create an array of chars  
            char[] chars = new char[length];
            for (int i = 0; i < length; i++)
            {
                chars[i] = validChars[random.Next(0, validChars.Length)];
            }
            return new string(chars);
        }

        public static string CreateRandomPassword(UserManager<ApplicationUser> _userManager)
        {
            var options = _userManager.Options.Password;

            int length = options.RequiredLength;

            bool nonAlphanumeric = options.RequireNonAlphanumeric;
            bool digit = options.RequireDigit;
            bool lowercase = options.RequireLowercase;
            bool uppercase = options.RequireUppercase;

            StringBuilder password = new StringBuilder();
            Random random = new Random();

            while (password.Length < length)
            {
                char c = (char)random.Next(32, 126);

                password.Append(c);

                if (char.IsDigit(c))
                    digit = false;
                else if (char.IsLower(c))
                    lowercase = false;
                else if (char.IsUpper(c))
                    uppercase = false;
                else if (!char.IsLetterOrDigit(c))
                    nonAlphanumeric = false;
            }

            if (nonAlphanumeric)
                password.Append((char)random.Next(33, 48));
            if (digit)
                password.Append((char)random.Next(48, 58));
            if (lowercase)
                password.Append((char)random.Next(97, 123));
            if (uppercase)
                password.Append((char)random.Next(65, 91));

            return password.ToString();
        }
    }
}
