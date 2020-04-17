using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter4.LogAn
{
    public interface IWebService
    {
        void LogError(string message);
    }
    public class FakeWebService : IWebService
    {
        public string LastError;
        public Exception ToThrow;

        public void LogError(string message)
        {
            LastError = message;
            if (ToThrow != null)
            {
                throw ToThrow;
            }
        }
    }
    public interface IEmailService
    {
        void SendEmail(EmailInfo emailInfo);
    }
    public class FakeEmailService : IEmailService
    {
        public EmailInfo email = null;
        public void SendEmail(EmailInfo emailInfo)
        {
            email = emailInfo;
        }
    }
    public class EmailInfo
    {
        public string Body;
        public string To;
        public string Subject;
    }
}
