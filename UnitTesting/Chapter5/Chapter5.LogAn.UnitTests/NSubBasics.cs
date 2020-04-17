using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter5.LogAn.UnitTests
{
    [TestFixture]
    public class NSubBasics
    {
        [Test]
        public void Returns_ByDefault_WorksForHardCodedArgument()
        {
            IFileNameRules fakeRules = Substitute.For<IFileNameRules>();
            fakeRules.IsValidLogFileName(Arg.Any<String>()).Returns(true);
            Assert.IsTrue(fakeRules.IsValidLogFileName("strict.txt"));
        }

        [Test]
        public void Returns_ArgAny_Throws()
        {
            IFileNameRules fakeRules = Substitute.For<IFileNameRules>();
            fakeRules.When(x => x.IsValidLogFileName(Arg.Any<String>()))
                .Do(context => { throw new Exception("fake exception"); });
            Assert.Throws<Exception>(() => fakeRules.IsValidLogFileName(""));
        }

        [Test]
        public void Analyze_LoggerThrows_CallsWebService()
        {
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();
            stubLogger.When(logger => logger.LogError(Arg.Any<string>()))
                .Do(info => { throw new Exception("fake exception"); });
            var analyzer = new LogAnalyzer2(stubLogger, mockWebService);
            analyzer.MinNameLength = 10;
            analyzer.Analyze("Short.txt");
            mockWebService.Received().Write(Arg.Is<string>(s => s.Contains("fake exception")));
        }
        [Test]
        public void Analyze_LoggerThrows_CallsWebServiceWithNSubObject()
        {
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();
            stubLogger.When(
            logger => logger.LogError(Arg.Any<string>()))
            .Do(info => { throw new Exception("fake exception"); });
            var analyzer =
            new LogAnalyzer3(stubLogger, mockWebService);
            analyzer.MinNameLength = 10;
            analyzer.Analyze("Short.txt");
            mockWebService.Received().Write(Arg.Is<ErrorInfo>(info => info.Severity == 1000
            && info.Message.Contains("fake exception")));
        }
        [Test]
        public void Analyze_LoggerThrows_CallsWebServiceWithNSubObjectCompare()
        {
            var mockWebService = Substitute.For<IWebService>();
            var stubLogger = Substitute.For<ILogger>();
            stubLogger.When(
            logger => logger.LogError(Arg.Any<string>()))
            .Do(info => { throw new Exception("fake exception"); });
            var analyzer =
            new LogAnalyzer3(stubLogger, mockWebService);
            analyzer.MinNameLength = 10;
            analyzer.Analyze("Short.txt");
            var expected = new ErrorInfo(1000, "fake exception");

            mockWebService.Received().Write(expected);

        }
        [Test]
        public void RecursiveFakes_work()
        {
            IPerson p = Substitute.For<IPerson>();
            Assert.IsNotNull(p.GetManager());
            Assert.IsNotNull(p.GetManager().GetManager());
            Assert.IsNotNull(p.GetManager().GetManager().GetManager());
        }
    }
    public interface IPerson
    {
        IPerson GetManager();
    }
}
