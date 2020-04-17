using System;
using NUnit.Framework;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Chapter3.LogAn.UnitTests")]
namespace Chapter3.LogAn.UnitTests
{
    #region Factory
    class LogAnalyzerUsingFactoryMethod
    {
        public bool IsValidLogFileName(string fileName)
        {
            return this.IsValid(fileName);
        }
        
        protected virtual bool IsValid(string fileName)
        {
            FileExtensionManager mgr = new FileExtensionManager();
            return mgr.IsValid(fileName);
        }
    }
    #endregion

    #region Extension Manager
    internal class FakeExtensionManager : IExtensionManager
    {
        public bool WillBeValid = false;
        public Exception WillThrow = null;

        public bool IsValid(string fileName)
        {
            if (WillThrow != null)
            { throw WillThrow; }
            return WillBeValid;
        }
    }

    internal class FileExtensionManager : IExtensionManager
    {
        public bool IsValid(string fileName)
        {
            //Read some file
            return true;
        }
    }
    #endregion

    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidFileName_NameSupportedExtension_ReturnsTrue()
        {
            FakeExtensionManager stub = new FakeExtensionManager();
            stub.WillBeValid = true;
            LogAnalyzer log =
            new LogAnalyzer(stub);
            bool result = log.IsValidLogFileName("short.ext");
            Assert.True(result);
        }

        [Test]
        public void IsValidFileName_ExtManagerThrowsException_ReturnsFalse()
        {
            FakeExtensionManager myFakeManager = new FakeExtensionManager();
            myFakeManager.WillThrow = new Exception("this is fake");
            LogAnalyzer log =
            new LogAnalyzer(myFakeManager);
            bool result = log.IsValidLogFileName("anything.anyextension");
            Assert.False(result);
        }

        [Test]
        public void OverrideTest()
        {            
            TestableLogAnalyzer logan = new TestableLogAnalyzer();
            logan.IsSupported = true;
            bool result = logan.IsValidLogFileName("file.ext");
            Assert.True(result);
        }
    }

    class TestableLogAnalyzer : LogAnalyzerUsingFactoryMethod
    {
        public bool IsSupported;
        protected override bool IsValid(string fileName)
        {
            return IsSupported;
        }
    }


}
