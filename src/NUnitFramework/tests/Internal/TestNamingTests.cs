﻿// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

using System.Text;

namespace NUnit.Framework.Internal
{
    public abstract class TestNamingTests
    {
        protected const string OUTER_CLASS = "NUnit.Framework.Internal.TestNamingTests";

        protected abstract string FixtureName { get; }

        protected TestContext.TestAdapter CurrentTest => TestContext.CurrentContext.Test;

        [Test]
        public void SimpleTest()
        {
            CheckNames("SimpleTest", "SimpleTest", OUTER_CLASS);
        }

        [TestCase(5, 7, "ABC")]
        [TestCase(1, 2, "< left bracket")]
        [TestCase(1, 2, "> right bracket")]
        [TestCase(1, 2, "' single quote")]
        [TestCase(1, 2, "\" double quote")]
        [TestCase(1, 2, "& ampersand")]
        public void ParameterizedTest(int x, int y, string s)
        {
            CheckNames(
                $"ParameterizedTest({x},{y},\"{s.Replace("\"", "\\\"")}\")",
                "ParameterizedTest",
                OUTER_CLASS);
        }

        [TestCase("abcdefghijklmnopqrstuvwxyz")]
        public void TestCaseWithLongStringArgument(string s)
        {
            CheckNames("TestCaseWithLongStringArgument(\"abcdefghijklmnopqrstuvwxyz\")", "TestCaseWithLongStringArgument", OUTER_CLASS);
        }

        [TestCase(42)]
        public void GenericTest<T>(T arg)
        {
            CheckNames("GenericTest<Int32>(42)", "GenericTest", OUTER_CLASS);
        }

        [TestCase()]
        [TestCase(1)]
        [TestCase(1, 2)]
        [TestCase(1, 2, 3)]
        public void TestWithParamsArgument(params int[] array)
        {
            var sb = new StringBuilder("TestWithParamsArgument(");

            foreach (int n in array)
            {
                if (n > 1) sb.Append(",");
                sb.Append(n.ToString());
            }

            sb.Append(")");

            CheckNames(sb.ToString(), "TestWithParamsArgument", OUTER_CLASS);
        }

        private void CheckNames(string expectedTestName, string expectedMethodName, string expectedClassName)
        {
            Assert.That(CurrentTest.Name, Is.EqualTo(expectedTestName));
            Assert.That(CurrentTest.FullName, Is.EqualTo(OUTER_CLASS + "+" + FixtureName + "." + expectedTestName));
            Assert.That(CurrentTest.MethodName, Is.EqualTo(expectedMethodName));
            Assert.That(CurrentTest.ClassName, Is.EqualTo(expectedClassName));
        }

        public class SimpleFixture : TestNamingTests
        {
            protected const string CURRENT_CLASS = "SimpleFixture";

            [Test]
            public void SimpleTestInDerivedClass()
            {
                CheckNames("SimpleTestInDerivedClass", "SimpleTestInDerivedClass", OUTER_CLASS + "+" + CURRENT_CLASS);
            }

            protected override string FixtureName => "SimpleFixture";
        }

        [TestFixture(typeof(int))]
        public class GenericFixture<T> : TestNamingTests
        {
            protected const string CURRENT_CLASS = "GenericFixture`1";

            [Test]
            public void SimpleTestInDerivedClass()
            {
                CheckNames("SimpleTestInDerivedClass", "SimpleTestInDerivedClass", OUTER_CLASS + "+" + CURRENT_CLASS);
            }

            protected override string FixtureName => "GenericFixture<Int32>";
        }

        [TestFixture(42, "Forty-two")]
        public class ParameterizedFixture : TestNamingTests
        {
            protected const string CURRENT_CLASS = "ParameterizedFixture";

            [Test]
            public void SimpleTestInDerivedClass()
            {
                CheckNames("SimpleTestInDerivedClass", "SimpleTestInDerivedClass", OUTER_CLASS + "+" + CURRENT_CLASS);
            }

            public ParameterizedFixture(int x, string s) { }

            protected override string FixtureName => "ParameterizedFixture(42,\"Forty-two\")";
        }

        [TestFixture("This is really much too long to be used in the test name!")]
        public class ParameterizedFixtureWithLongStringArgument : TestNamingTests
        {
            protected const string CURRENT_CLASS = "ParameterizedFixtureWithLongStringArgument";

            [Test]
            public void SimpleTestInDerivedClass()
            {
                CheckNames("SimpleTestInDerivedClass", "SimpleTestInDerivedClass", OUTER_CLASS + "+" + CURRENT_CLASS);
            }

            public ParameterizedFixtureWithLongStringArgument(string s) { }

            protected override string FixtureName => "ParameterizedFixtureWithLongStringArgument(\"This is really much too long to be us...\")";
        }

        [TestFixture(typeof(int), typeof(string), 42, "Forty-two")]
        public class GenericParameterizedFixture<T1,T2> : TestNamingTests
        {
            protected const string CURRENT_CLASS = "GenericParameterizedFixture`2";

            [Test]
            public void SimpleTestInDerivedClass()
            {
                CheckNames("SimpleTestInDerivedClass", "SimpleTestInDerivedClass", OUTER_CLASS + "+" + CURRENT_CLASS);
            }

            public GenericParameterizedFixture(T1 x, T2 y) { }

            protected override string FixtureName => "GenericParameterizedFixture<Int32,String>(42,\"Forty-two\")";
        }
    }
}
