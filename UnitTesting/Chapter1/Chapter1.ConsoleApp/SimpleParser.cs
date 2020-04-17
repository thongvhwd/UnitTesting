using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Chapter1.ConsoleApp
{
    public class SimpleParser
    {
        public int ParseAndSum(string numbers)
        {
            if (numbers.Length == 0)
            {
                return 0;
            }
            if (!numbers.Contains(","))
            {
                return int.Parse(numbers);
            }
            else
            {
                throw new InvalidOperationException("I can only handle 0 or 1 numbers for now!");
            }
        }

    }
    public class SimpleParserTests
    {
        //public static void TestReturnsZeroWhenEmptyString()
        //{
        //    try
        //    {
        //        SimpleParser p = new SimpleParser();
        //        int result = p.ParseAndSum(string.Empty);
        //        if (result != 0)
        //        {

        //            Console.WriteLine(@"***SimpleParserTests.TestReturnsZeroWhenEmptyString:
        //            -------
        //            Parse and sum should have returned 0 on an empty string");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //    }
        //}
        public static void TestReturnsZeroWhenEmptyString()
        {
            //use .NET's reflection API to get the current
            // method's name
            // it's possible to hard code this,
            //but it’s a useful technique to know
            string testName = MethodBase.GetCurrentMethod().Name;
            try
            {
                SimpleParser p = new SimpleParser();
                int result = p.ParseAndSum(string.Empty);
                if (result != 0)
                {
                    //Calling the helper method
                    TestUtil.ShowProblem(testName,
                    "Parse and sum should have returned 0 on an empty string");
                }
            }
            catch (Exception e)
            {
                TestUtil.ShowProblem(testName, e.ToString());
            }
        }
    }

    public class TestUtil
    {
        public static void ShowProblem(string test, string message)
        {
            string msg = string.Format(@"
            ---{0}---
                   {1}
            --------------------
            ", test, message);
            Console.WriteLine(msg);
        }
        
    }
}
