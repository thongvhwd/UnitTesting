using NUnit.Framework;

namespace Chapter4.LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void Analyze_TooShortFileName_CallsWebService()
        {
            FakeWebService mockService = new FakeWebService();
            LogAnalyzer log = new LogAnalyzer(mockService);
            string tooShortFileName = "abc.ext";
            log.Analyze(tooShortFileName);
            StringAssert.Contains("Filename too short: "+tooShortFileName, mockService.LastError);
        }
    }
}
