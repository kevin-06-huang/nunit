// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

using System;
using System.Collections.Generic;
using NUnit.Framework.Interfaces;
using NUnit.TestData.TestMethodSignatureFixture;
using NUnit.TestUtilities;

namespace NUnit.Framework.Internal
{
    [TestFixture]
    public class TestMethodSignatureTests
    {
        private static readonly Type fixtureType = typeof(TestMethodSignatureFixture);

        [Test]
        public void InstanceTestMethodIsRunnable()
        {
            TestAssert.IsRunnable(fixtureType, nameof(TestMethodSignatureFixture.InstanceTestMethod) );
        }

        [Test]
        public void StaticTestMethodIsRunnable()
        {
            TestAssert.IsRunnable(fixtureType, nameof(TestMethodSignatureFixture.StaticTestMethod) );
        }

        [Test]
        public void TestMethodWithoutParametersWithArgumentsProvidedIsNotRunnable()
        {
            TestAssert.ChildNotRunnable(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithoutParametersWithArgumentsProvided));
        }

        [Test]
        public void TestMethodWithArgumentsNotProvidedIsNotRunnable()
        {
            TestAssert.IsNotRunnable(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithArgumentsNotProvided));
        }

        [Test]
        public void TestMethodHasAttributesAppliedCorrectlyEvenIfNotRunnable()
        {
            var test = TestBuilder.MakeTestCase(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithArgumentsNotProvidedAndExtraAttributes));
            Assert.Multiple(() =>
            {
                // NOTE: IgnoreAttribute has no effect, either on RunState or on the reason
                Assert.That(test.RunState, Is.EqualTo(RunState.NotRunnable));
                Assert.That(test.Properties.Get(PropertyNames.SkipReason), Is.EqualTo("No arguments were provided"));
                Assert.That(test.Properties.Get(PropertyNames.Description), Is.EqualTo("My test"));
                Assert.That(test.Properties.Get(PropertyNames.MaxTime), Is.EqualTo(47));
            });
        }

        [Test]
        public void TestMethodWithArgumentsProvidedIsRunnable()
        {
            TestAssert.IsRunnable(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithArgumentsProvided));
        }

        [Test]
        public void TestMethodWithWrongNumberOfArgumentsProvidedIsNotRunnable()
        {
            TestAssert.ChildNotRunnable(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithWrongNumberOfArgumentsProvided));
        }

        [Test]
        public void TestMethodWithWrongArgumentTypesProvidedGivesError()
        {
            TestAssert.IsRunnable(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithWrongArgumentTypesProvided), ResultState.Error);
        }

        [Test]
        public void StaticTestMethodWithArgumentsNotProvidedIsNotRunnable()
        {
            TestAssert.IsNotRunnable(fixtureType, nameof(TestMethodSignatureFixture.StaticTestMethodWithArgumentsNotProvided));
        }

        [Test]
        public void StaticTestMethodWithArgumentsProvidedIsRunnable()
        {
            TestAssert.IsRunnable(fixtureType, nameof(TestMethodSignatureFixture.StaticTestMethodWithArgumentsProvided));
        }

        [Test]
        public void StaticTestMethodWithWrongNumberOfArgumentsProvidedIsNotRunnable()
        {
            TestAssert.ChildNotRunnable(fixtureType, nameof(TestMethodSignatureFixture.StaticTestMethodWithWrongNumberOfArgumentsProvided));
        }

        [Test]
        public void StaticTestMethodWithWrongArgumentTypesProvidedGivesError()
        {
            TestAssert.IsRunnable(fixtureType, nameof(TestMethodSignatureFixture.StaticTestMethodWithWrongArgumentTypesProvided), ResultState.Error);
        }

        [Test]
        public void TestMethodWithConvertibleArgumentsIsRunnable()
        {
            TestAssert.IsRunnable(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithConvertibleArguments));
        }

        [Test]
        public void TestMethodWithNonConvertibleArgumentsGivesError()
        {
            TestAssert.IsRunnable(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithNonConvertibleArguments), ResultState.Error);
        }

        [Test]
        public void ProtectedTestMethodIsNotRunnable()
        {
            TestAssert.IsNotRunnable(fixtureType, "ProtectedTestMethod");
        }

        [Test]
        public void PrivateTestMethodIsNotRunnable()
        {
            TestAssert.IsNotRunnable(fixtureType, "PrivateTestMethod");
        }

        [Test]
        public void TestMethodWithReturnTypeIsNotRunnable()
        {
            TestAssert.IsNotRunnable(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithReturnValue_WithoutExpectedResult));
        }

        [Test]
        public void TestMethodWithExpectedReturnTypeIsRunnable()
        {
            TestAssert.IsRunnable(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithReturnValue_WithExpectedResult));
        }

        [Test]
        public void TestMethodWithExpectedReturnAndArgumentsIsNotRunnable()
        {
            TestAssert.IsNotRunnable(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithReturnValueAndArgs_WithExpectedResult));
        }

        [Test]
        public void TestMethodWithMultipleTestCasesExecutesMultipleTimes()
        {
            ITestResult result = TestBuilder.RunParameterizedMethodSuite(fixtureType, nameof(TestMethodSignatureFixture.TestMethodWithMultipleTestCases));

            Assert.That( result.ResultState, Is.EqualTo(ResultState.Success) );
            ResultSummary summary = new ResultSummary(result);
            Assert.That(summary.TestsRun, Is.EqualTo(3));
        }

        [Test]
        public void TestMethodWithMultipleTestCasesUsesCorrectNames()
        {
            string name = nameof(TestMethodSignatureFixture.TestMethodWithMultipleTestCases);
            string fullName = typeof (TestMethodSignatureFixture).FullName + "." + name;

            TestSuite suite = TestBuilder.MakeParameterizedMethodSuite(fixtureType, name);
            Assert.That(suite.TestCaseCount, Is.EqualTo(3));

            var names = new List<string>();
            var fullNames = new List<string>();

            foreach (Test test in suite.Tests)
            {
                names.Add(test.Name);
                fullNames.Add(test.FullName);
            }

            Assert.That(names, Has.Member(name + "(12,3,4)"));
            Assert.That(names, Has.Member(name + "(12,2,6)"));
            Assert.That(names, Has.Member(name + "(12,4,3)"));

            Assert.That(fullNames, Has.Member(fullName + "(12,3,4)"));
            Assert.That(fullNames, Has.Member(fullName + "(12,2,6)"));
            Assert.That(fullNames, Has.Member(fullName + "(12,4,3)"));
        }

        [Test]
        public void RunningTestsThroughFixtureGivesCorrectResults()
        {
            ITestResult result = TestBuilder.RunTestFixture(fixtureType);
            ResultSummary summary = new ResultSummary(result);

            Assert.That(
                summary.ResultCount,
                Is.EqualTo(TestMethodSignatureFixture.Tests));
            Assert.That(
                summary.TestsRun,
                Is.EqualTo(TestMethodSignatureFixture.Tests));
            Assert.That(
                summary.Failed,
                Is.EqualTo(TestMethodSignatureFixture.Failures + TestMethodSignatureFixture.Errors + TestMethodSignatureFixture.NotRunnable));
            Assert.That(
                summary.Skipped,
                Is.EqualTo(0));
        }
    }
}
