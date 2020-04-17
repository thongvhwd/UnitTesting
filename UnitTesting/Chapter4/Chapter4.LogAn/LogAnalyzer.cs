using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4.LogAn
{
    public class LogAnalyzer
    {
        private IWebService service;

        public LogAnalyzer(IWebService service)
        {
            this.service = service;
        }
        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                service.LogError("Filename too short: " + fileName);
            }
        }

    }
    public class LogAnalyzer2
    {
        public LogAnalyzer2(IWebService service, IEmailService email)
        {
            Email = email;
            Service = service;
        }
        public IWebService Service
        {
            get;
            set;
        }
        public IEmailService Email
        {
            get;
            set;
        }
        public void Analyze(string fileName)
        {
            if (fileName.Length < 8)
            {
                try
                {
                    Service.LogError("Filename too short:" + fileName);
                }
                catch (Exception e)
                {
                    Email.SendEmail(new EmailInfo
                    {
                        Body = "fake exception",
                        To = "someone@somewhere.com",
                        Subject = "can’t log"
                    });
                }
            }
        }
    }
}
