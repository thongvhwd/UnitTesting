using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Chapter4.LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzer2Tests
    {
        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            FakeWebService stubService = new FakeWebService();
            stubService.ToThrow = new Exception("fake exception");
            FakeEmailService mockEmail = new FakeEmailService();

            LogAnalyzer2 log = new LogAnalyzer2(stubService, mockEmail);
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            EmailInfo expectedEmail = new EmailInfo
            {
                Body = "fake exception",
                To = "someone@somewhere.com",
                Subject = "can’t log"
            };
            Assert.AreSame(expectedEmail, mockEmail.email);
        }
    }
}
