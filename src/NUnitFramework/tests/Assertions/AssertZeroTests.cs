﻿// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

using System;

namespace NUnit.Framework.Assertions
{
    [TestFixture]
    public class AssertZeroTests
    {
        private readonly int i1 = 0;
        private readonly int i2 = 1234;
        private readonly uint u1 = 0;
        private readonly uint u2 = 12345879;
        private readonly long l1 = 0;
        private readonly long l2 = 12345879;
        private readonly ulong ul1 = 0;
        private readonly ulong ul2 = 12345879;
        private readonly float f1 = 0F;
        private readonly float f2 = 8.543F;
        private readonly decimal de1 = 0M;
        private readonly decimal de2 = 83.4M;
        private readonly double d1 = 0.0;
        private readonly double d2 = 8.0;

        [Test]
        public void ZeroIsZero()
        {
            Assert.Zero(i1);
            Assert.Zero(u1);
            Assert.Zero(l1);
            Assert.Zero(ul1);
            Assert.Zero(f1);
            Assert.Zero(de1);
            Assert.Zero(d1);
        }

        [Test]
        public void AssertZeroFailsWhenNumberIsNotAZero()
        {
            Assert.Throws<AssertionException>(() => Assert.Zero(i2));
            Assert.Throws<AssertionException>(() => Assert.Zero(u2));
            Assert.Throws<AssertionException>(() => Assert.Zero(l2));
            Assert.Throws<AssertionException>(() => Assert.Zero(ul2));
            Assert.Throws<AssertionException>(() => Assert.Zero(f2));
            Assert.Throws<AssertionException>(() => Assert.Zero(de2));
            Assert.Throws<AssertionException>(() => Assert.Zero(d2));
        }

        [Test]
        public void NotZeroIsNotZero()
        {
            Assert.NotZero(i2);
            Assert.NotZero(u2);
            Assert.NotZero(l2);
            Assert.NotZero(ul2);
            Assert.NotZero(f2);
            Assert.NotZero(de2);
            Assert.NotZero(d2);
        }

        [Test]
        public void AssertNotZeroFailsWhenNumberIsZero()
        {
            Assert.Throws<AssertionException>(() => Assert.NotZero(i1));
            Assert.Throws<AssertionException>(() => Assert.NotZero(u1));
            Assert.Throws<AssertionException>(() => Assert.NotZero(l1));
            Assert.Throws<AssertionException>(() => Assert.NotZero(ul1));
            Assert.Throws<AssertionException>(() => Assert.NotZero(f1));
            Assert.Throws<AssertionException>(() => Assert.NotZero(de1));
            Assert.Throws<AssertionException>(() => Assert.NotZero(d1));
        }

        [Test]
        public void ZeroWithMessagesOverload()
        {
            Assert.That(
                () => Assert.Zero(1, "MESSAGE"),
                Throws.TypeOf<AssertionException>().With.Message.Contains("MESSAGE"));
        }

        [Test]
        public void ZeroWithMessageOverloadPasses()
        {
            Assert.Zero(0, "Message");
        }

        [Test]
        public void ExpectedFailureMessageExists()
        {
            var expectedMessage =
                "  Expected: 0" + Environment.NewLine +
                "  But was:  1234" + Environment.NewLine;
            var ex = Assert.Throws<AssertionException>(() => Assert.Zero(i2));
            Assert.That(ex.Message, Is.EqualTo(expectedMessage));
        }
    }
}


