using System;
using NUnit.Framework;

namespace LogAn.UnitTests
{
    [TestFixture]
    public class LogAnalyzerTests
    {
        [Test]
        public void IsValidLogFileName_BadExtension_ReturnsFalse()
        {
            LogAnalyzer analyzer = new LogAnalyzer();

            bool result = analyzer.IsValidLogFileName("filewithbadextension.foo");

            Assert.False(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionLowercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();

            bool result = analyzer.IsValidLogFileName("filewithgoodextension.slf");

            Assert.True(result);
        }

        [Test]
        public void IsValidLogFileName_GoodExtensionUppercase_ReturnsTrue()
        {
            LogAnalyzer analyzer = new LogAnalyzer();

            bool result = analyzer.IsValidLogFileName("filewithgoodextension.SLF");

            Assert.True(result);
        }

        [TestCase("filewithgoodextension.SLF")]
        [TestCase("filewithgoodextension.slf")]
        public void IsValidLogFileName_ValidExtensions_ReturnsTrue(string file)
        {
            LogAnalyzer analyzer = new LogAnalyzer();

            bool result = analyzer.IsValidLogFileName(file);

            Assert.True(result);
        }

        [TestCase("filewithgoodextension.SLF", true)]
        [TestCase("filewithgoodextension.slf", true)]
        [TestCase("filewithbadextension.foo", false)]
        public void IsValidLogFileName_ValidExtensions_ChecksThem(string file, bool expected)
        {
            LogAnalyzer analyzer = new LogAnalyzer();

            bool result = analyzer.IsValidLogFileName(file);

            Assert.AreEqual(expected, result);
        }

        //[Test]
        ////[ExpectedException(typeof(ArgumentException),
        ////      ExpectedMessage = "filename has to be provided")]
        //public void IsValidLogFileName_EmptyFileName_ThrowsException()
        //{
        //    LogAnalyzer la = MakeAnalyzer();
        //    la.IsValidLogFileName(string.Empty);
        //}

        private LogAnalyzer MakeAnalyzer()
        {
            return new LogAnalyzer();
        }

        [Test]
        public void IsValidLogFileName_EmptyFileName_Throws()
        {
            LogAnalyzer la = MakeAnalyzer();
            var ex = Assert.Throws<ArgumentException>(() => la.IsValidLogFileName(""));
            StringAssert.Contains("filename has to be provided", ex.Message);
        }

        [Test]
        public void IsValidFileName_EmptyFileName_ThrowsFluent()
        {
            LogAnalyzer la = MakeAnalyzer();
            var ex =
            Assert.Catch<ArgumentException>(() =>
            la.IsValidLogFileName(""));
            Assert.That(ex.Message, Is.SamePathOrUnder("filename has to be provided"));
        }

        [Test]
        [Category("Fast Tests")]
        public void IsValidFileName_ValidFile_ReturnsTrue()
        {
            /// ...
        }

        [Test]
        public void IsValidLogFileName_WhenCalled_ChangesWasLastFileNameValid()
        {
            LogAnalyzer la = MakeAnalyzer();

            la.IsValidLogFileName("badname.foo");

            Assert.IsFalse(la.WasLastFileNameValid);
        }

        [TestCase("badfile.foo", false)]
        [TestCase("goodfile.slf", true)]
        public void IsValidLogFileName_WhenCalled_ChangesWasLastFileNameValid(string file, bool expected)
        {
            LogAnalyzer la = MakeAnalyzer();

            la.IsValidLogFileName(file);

            Assert.AreEqual(expected, la.WasLastFileNameValid);
        }

        [Test]
        public void Sum_ByDefault_ReturnsZero()
        {
            MemCalculator calc = MakeCalc();
            int lastSum = calc.Sum();
            Assert.AreEqual(0, lastSum);
        }
        [Test]
        public void Add_WhenCalled_ChangesSum()
        {
            MemCalculator calc = MakeCalc();
            calc.Add(1);
            int sum = calc.Sum();
            Assert.AreEqual(1, sum);
        }
        private static MemCalculator MakeCalc()
        {
            return new MemCalculator();
        }
    }
}
