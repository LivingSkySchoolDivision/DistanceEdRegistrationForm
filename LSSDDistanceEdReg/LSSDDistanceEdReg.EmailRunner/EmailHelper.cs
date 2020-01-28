using LSSD.DistanceEdReg;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace LSSDDistanceEdReg.EmailRunner
{

    class EmailHelper
    {
        private string hostname;
        private string username;
        private string password;
        private string fromaddress;
        private string replyToAddress;
        private int smtpPort;

        public EmailHelper(string hostname, int smtpPort, string username, string password, string fromaddress, string replyToAddress)
        {
            this.hostname = hostname;
            this.username = username;
            this.password = password;
            this.fromaddress = fromaddress;
            this.smtpPort = smtpPort;
            this.replyToAddress = replyToAddress;
        }

        Queue<DistanceEdRequest> _mailQueue = new Queue<DistanceEdRequest>();

        public void NewMessage(DistanceEdRequest request)
        {
            this._mailQueue.Enqueue(request);
        }

        public void NewMessages(IEnumerable<DistanceEdRequest> requests)
        {
            foreach(DistanceEdRequest r in requests)
            {
                _mailQueue.Enqueue(r);
            }
        }
               
        /// <summary>
        /// Sends all enqueued mail messages
        /// </summary>
        /// <returns>ID numbers of successful notifications</returns>
        public List<int> SendAll()
        {
            List<int> successes = new List<int>();

            if (this._mailQueue.Count > 0)
            {
                using (SmtpClient smtpClient = new SmtpClient(this.hostname)
                {
                    Port = this.smtpPort,
                    UseDefaultCredentials = false,
                    EnableSsl = true,
                    Credentials = new NetworkCredential(this.username, this.password)
                })
                {
                    while (this._mailQueue.Count > 0)
                    {
                        DistanceEdRequest request = this._mailQueue.Dequeue();

                        try
                        {
                            // Send the mail message and log the results in the DB
                            MailMessage msg = new MailMessage();
                            msg.To.Add("mark.strendin@lskysd.ca");
                            msg.Body = "";
                            msg.Subject = "Distance Ed Request for " + request.StudentName + " for class " + request.ID;
                            msg.From = new MailAddress(this.fromaddress);
                            msg.ReplyToList.Add(new MailAddress(this.replyToAddress));
                            msg.IsBodyHtml = true;
                            smtpClient.Send(msg);

                            successes.Add(request.ID);
                        }
                        catch (Exception ex)
                        {
                            // Log a failure
                            throw ex;
                        }
                    }

                }
            }

            return successes;
        }

    }
}
