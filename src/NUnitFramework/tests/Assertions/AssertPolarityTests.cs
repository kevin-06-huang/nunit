﻿// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

using System;

namespace NUnit.Framework.Assertions
{
    [TestFixture]
    public class AssertPolarityTests
    {
        private readonly int i1 = 1;
        private readonly int i2 = -1;
        private readonly uint u1 = 123456789;
        private readonly long l1 = 123456789;
        private readonly long l2 = -12345879;
        private readonly ulong ul1 = 123456890;
        private readonly float f1 = 8.543F;
        private readonly float f2 = -8.543F;
        private readonly decimal de1 = 83.4M;
        private readonly decimal de2 = -83.4M;
        private readonly double d1 = 8.0;
        private readonly double d2 = -8.0;

        [Test]
        public void PositiveNumbersPassPositiveAssertion()
        {
            Assert.Positive(i1);
            Assert.Positive(l1);
            Assert.Positive(f1);
            Assert.Positive(de1);
            Assert.Positive(d1);
        }

        [Test]
        public void AssertNegativeNumbersFailPositiveAssertion()
        {
            Assert.Throws<AssertionException>(() => Assert.Positive(i2));
            Assert.Throws<AssertionException>(() => Assert.Positive(l2));
            Assert.Throws<AssertionException>(() => Assert.Positive(f2));
            Assert.Throws<AssertionException>(() => Assert.Positive(de2));
            Assert.Throws<AssertionException>(() => Assert.Positive(d2));
        }

        [Test]
        public void NegativeNumbersPassNegativeAssertion()
        {
            Assert.Negative(i2);
            Assert.Negative(l2);
            Assert.Negative(f2);
            Assert.Negative(de2);
            Assert.Negative(d2);
        }

        [Test]
        public void AssertPositiveNumbersFailNegativeAssertion()
        {
            Assert.Throws<AssertionException>(() => Assert.Negative(i1));
            Assert.Throws<AssertionException>(() => Assert.Negative(u1));
            Assert.Throws<AssertionException>(() => Assert.Negative(l1));
            Assert.Throws<AssertionException>(() => Assert.Negative(ul1));
            Assert.Throws<AssertionException>(() => Assert.Negative(f1));
            Assert.Throws<AssertionException>(() => Assert.Negative(de1));
            Assert.Throws<AssertionException>(() => Assert.Negative(d1));
        }

        [Test]
        public void IsNegativeWithMessageOverload()
        {
            Assert.That(
                () => Assert.Negative(1, "MESSAGE"),
                Throws.TypeOf<AssertionException>().With.Message.Contains("MESSAGE"));
        }

        [Test]
        public void IsPositiveWithMessageOverload()
        {
            Assert.That(
                () => Assert.Positive(-1, "MESSAGE"),
                Throws.TypeOf<AssertionException>().With.Message.Contains("MESSAGE"));
        }

        [Test]
        public void IsPositiveWithMessageOverloadPasses()
        {
            Assert.Positive(1, "Message");
        }

        [Test]
        public void IsNegativeWithMessageOverloadPasses()
        {
            Assert.Negative(-1, "Message");
        }

        [Test]
        public void ExpectedFailureMessageExistsForIsPositive()
        {
            var expectedMessage =
                "  Expected: greater than 0" + Environment.NewLine +
                "  But was:  -1" + Environment.NewLine;
            var ex = Assert.Throws<AssertionException>(() => Assert.Positive(i2));
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }

        [Test]
        public void ExpectedFailureMessageExistsForIsNegative()
        {
            var expectedMessage =
                "  Expected: less than 0" + Environment.NewLine +
                "  But was:  1" + Environment.NewLine;
            var ex = Assert.Throws<AssertionException>(() => Assert.Negative(i1));
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }
    }
}


