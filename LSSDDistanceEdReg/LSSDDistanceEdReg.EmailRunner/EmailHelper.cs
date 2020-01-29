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
         
        public List<int> SendAll(string destination)
        {
            return SendAll(new List<string>() { destination });
        }

        /// <summary>
        /// Sends all enqueued mail messages
        /// </summary>
        /// <returns>ID numbers of successful notifications</returns>
        public List<int> SendAll(IEnumerable<string> destinations)
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

                        StringBuilder body = new StringBuilder();
                        body.Append("<html>");
                        body.Append("<body>");
                        body.Append("<h3>LSSD Virtual Distance Ed Request</h3>");
                        body.Append("<p>A new distance ed request has been submitted. Please register this student in the specified distance ed class.</p>");
                        body.Append("<p>");
                        body.Append("<b>Date Submitted</b>: " + request.DateRequested.ToLongDateString() + " " + request.DateRequested.ToShortTimeString() + "<br>");
                        body.Append("<b>Submitted By</b>: " + request.Requestor + "<br>");
                        body.Append("<b>Request ID</b>: " + request.ID + "<br>");
                        body.Append("</p>");
                        body.Append("<p>");
                        body.Append("<b>Student Name</b>: " + request.StudentName +"<br>");
                        body.Append("<b>Student Number</b>: " + request.StudentNumber +"<br>");
                        body.Append("<b>Student Base School</b>: " + request.StudentBaseSchool + "<br>");
                        body.Append("</p>");
                        body.Append("<p>");
                        body.Append("<b>Mentor Teacher Name</b>: " + request.MentorTeacherName + "<br>");
                        body.Append("</p>");                        
                        body.Append("<p>");
                        if (request.DistanceEdClass != null)
                        {
                            body.Append("<b>Course Name</b>: " + (request.DistanceEdClass.Name ?? "Unknown") + "<br>");
                            body.Append("<b>Course Blackboard ID</b>: " + (request.DistanceEdClass.BlackboardID ?? "Unknown") + "<br>");
                            body.Append("<b>Course Start Date</b>: " + (request.DistanceEdClass.Starts.ToLongDateString() ?? "Unknown") +"<br>");
                            body.Append("<b>Course End Date</b>: " + (request.DistanceEdClass.Ends.ToLongDateString() ?? "Unknown") +"<br>");
                        } else
                        {
                            body.Append("This registration is for course with database ID \"" + request.CourseID + "\", but a course with that ID was not found in the database. You may need to investigate.");
                        }                        
                        body.Append("</p>");
                        body.Append("<p>This message comes from an unmonitored email account. Do not reply to this message.</p>");
                        body.Append("</body>");
                        body.Append("</html>");

                        try
                        {
                            // Send the mail message and log the results in the DB
                            MailMessage msg = new MailMessage();
                            foreach(string dest in destinations)
                            {
                                msg.To.Add(dest);
                            }

                            msg.Body = body.ToString();
                            if (request.DistanceEdClass != null)
                            {
                                msg.Subject = "Distance Ed Request for " + request.StudentName + " for " + request.DistanceEdClass.Name;
                            } else
                            {
                                msg.Subject = "Distance Ed Request for " + request.StudentName + " for class with database ID \"" + request.ID + "\"";
                            }
                            msg.From = new MailAddress(this.fromaddress, "Distance Ed Registration Form");
                            msg.ReplyToList.Add(new MailAddress(this.replyToAddress, "Distance Ed Registration Form"));
                            msg.IsBodyHtml = true;
                            smtpClient.Send(msg);

                            successes.Add(request.ID);
                        }
                        catch (Exception ex)
                        {
                            // Log a failure
                            throw new Exception("Error sending email for request ID " + request.ID, ex);
                        }
                    }

                }
            }

            return successes;
        }

    }
}
