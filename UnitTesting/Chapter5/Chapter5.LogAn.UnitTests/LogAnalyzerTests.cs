using NSubstitute;
using NUnit.Framework;
using System;

namespace Chapter5.LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void Analyze_TooShortFileName_CallLogger()
        {
            ILogger logger = Substitute.For<ILogger>();
            
            LogAnalyzer analyzer = new LogAnalyzer(logger);
            
            analyzer.MinNameLength = 6;
            analyzer.Analyze("a.txt");

            logger.Received().LogError("Filename too short: a.txt");
        }
    }
}
