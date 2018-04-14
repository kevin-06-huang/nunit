// ***********************************************************************
// Copyright (c) 2009-2015 Charlie Poole, Rob Prouse
//
// Permission is hereby granted, free of charge, to any person obtaining
// a copy of this software and associated documentation files (the
// "Software"), to deal in the Software without restriction, including
// without limitation the rights to use, copy, modify, merge, publish,
// distribute, sublicense, and/or sell copies of the Software, and to
// permit persons to whom the Software is furnished to do so, subject to
// the following conditions:
//
// The above copyright notice and this permission notice shall be
// included in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
// EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
// MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
// LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
// OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
// WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// ***********************************************************************

using System;
using System.Collections.Generic;
using System.Reflection;
using NUnit.Framework.Internal;
using NUnit.TestUtilities;

#if NETCOREAPP1_1
using System.Linq;
#endif

namespace NUnit.Framework.Attributes
{
    public partial class RangeAttributeTests
    {
        [Test]
        public void MultipleAttributes()
        {
            Test test = TestBuilder.MakeParameterizedMethodSuite(GetType(), nameof(MethodWithMultipleRanges));

            Assert.That(test.TestCaseCount, Is.EqualTo(6));
        }

        private void MethodWithMultipleRanges([Range(1, 3)] [Range(10, 12)] int x) { }

        #region Ints

        public static IEnumerable<TestCaseData> IntRangeCases() => new[]
        {
            new TestCaseData(typeof(sbyte), ExpectedOutcome.Values(new sbyte[] { 11, 12, 13, 14, 15 })),
            new TestCaseData(typeof(byte), ExpectedOutcome.Values(new byte[] { 11, 12, 13, 14, 15 })),
            new TestCaseData(typeof(short), ExpectedOutcome.Values(new short[] { 11, 12, 13, 14, 15 })),
            new TestCaseData(typeof(ushort), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(int), ExpectedOutcome.Values(new int[] { 11, 12, 13, 14, 15 })),
            new TestCaseData(typeof(uint), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(long), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(ulong), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(float), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(double), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(decimal), ExpectedOutcome.Values(new decimal[] { 11, 12, 13, 14, 15 }))
        };
        [TestCaseSource(nameof(IntRangeCases))]
        public static void IntRange(Type parameterType, ExpectedOutcome outcome)
        {
            outcome.Assert(new RangeAttribute(11, 15), parameterType);
        }

        public static IEnumerable<TestCaseData> IntRange_ReversedCases() => new[]
        {
            new TestCaseData(typeof(sbyte), ExpectedOutcome.Values(new sbyte[] { 15, 14, 13, 12, 11 })),
            new TestCaseData(typeof(byte), ExpectedOutcome.Values(new byte[] { 15, 14, 13, 12, 11 })),
            new TestCaseData(typeof(short), ExpectedOutcome.Values(new short[] { 15, 14, 13, 12, 11 })),
            new TestCaseData(typeof(ushort), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(int), ExpectedOutcome.Values(new int[] { 15, 14, 13, 12, 11 })),
            new TestCaseData(typeof(uint), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(long), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(ulong), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(float), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(double), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(decimal), ExpectedOutcome.Values(new decimal[] { 15, 14, 13, 12, 11 }))
        };
        [TestCaseSource(nameof(IntRange_ReversedCases))]
        public static void IntRange_Reversed(Type parameterType, ExpectedOutcome outcome)
        {
            outcome.Assert(new RangeAttribute(15, 11), parameterType);
        }

        public static IEnumerable<TestCaseData> IntRange_FromEqualsToCases() => new[]
        {
            new TestCaseData(typeof(sbyte), ExpectedOutcome.Values(new sbyte[] { 11 })),
            new TestCaseData(typeof(byte), ExpectedOutcome.Values(new byte[] { 11 })),
            new TestCaseData(typeof(short), ExpectedOutcome.Values(new short[] { 11 })),
            new TestCaseData(typeof(ushort), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(int), ExpectedOutcome.Values(new int[] { 11 })),
            new TestCaseData(typeof(uint), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(long), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(ulong), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(float), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(double), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(decimal), ExpectedOutcome.Values(new decimal[] { 11 }))
        };
        [TestCaseSource(nameof(IntRange_FromEqualsToCases))]
        public static void IntRange_FromEqualsTo(Type parameterType, ExpectedOutcome outcome)
        {
            outcome.Assert(new RangeAttribute(11, 11), parameterType);
        }

        public static IEnumerable<TestCaseData> IntRangeAndStepCases() => new[]
        {
            new TestCaseData(typeof(sbyte), ExpectedOutcome.Values(new sbyte[] { 11, 13, 15 })),
            new TestCaseData(typeof(byte), ExpectedOutcome.Values(new byte[] { 11, 13, 15 })),
            new TestCaseData(typeof(short), ExpectedOutcome.Values(new short[] { 11, 13, 15 })),
            new TestCaseData(typeof(ushort), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(int), ExpectedOutcome.Values(new int[] { 11, 13, 15 })),
            new TestCaseData(typeof(uint), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(long), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(ulong), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(float), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(double), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(decimal), ExpectedOutcome.Values(new decimal[] { 11, 13, 15 }))
        };
        [TestCaseSource(nameof(IntRangeAndStepCases))]
        public static void IntRangeAndStep(Type parameterType, ExpectedOutcome outcome)
        {
            outcome.Assert(new RangeAttribute(11, 15, 2), parameterType);
        }

        [Test]
        public static void IntRangeAndZeroStep()
        {
            Assert.That(() => new RangeAttribute(11, 15, 0), Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        public static void IntRangeAndStep_Reversed()
        {
            Assert.That(() => new RangeAttribute(15, 11, 2), Throws.InstanceOf<ArgumentException>());
        }

        [Test]
        public static void IntRangeAndNegativeStep()
        {
            Assert.That(() => new RangeAttribute(11, 15, -2), Throws.InstanceOf<ArgumentException>());
        }

        public static IEnumerable<TestCaseData> IntRangeAndNegativeStep_ReversedCases() => new[]
        {
            new TestCaseData(typeof(sbyte), ExpectedOutcome.Values(new sbyte[] { 15, 13, 11 })),
            new TestCaseData(typeof(byte), ExpectedOutcome.Values(new byte[] { 15, 13, 11 })),
            new TestCaseData(typeof(short), ExpectedOutcome.Values(new short[] { 15, 13, 11 })),
            new TestCaseData(typeof(ushort), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(int), ExpectedOutcome.Values(new int[] { 15, 13, 11 })),
            new TestCaseData(typeof(uint), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(long), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(ulong), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(float), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(double), ExpectedOutcome.CoercionError),
            new TestCaseData(typeof(decimal), ExpectedOutcome.Values(new decimal[] { 15, 13, 11 }))
        };
        [TestCaseSource(nameof(IntRangeAndNegativeStep_ReversedCases))]
        public static void IntRangeAndNegativeStep_Reversed(Type parameterType, ExpectedOutcome outcome)
        {
            outcome.Assert(new RangeAttribute(15, 11, -2), parameterType);
        }

        #endregion

        #region Unsigned Ints

        [Test]
        public void UintRange()
        {
            CheckValues(nameof(MethodWithUintRange), 11u, 12u, 13u, 14u, 15u);
        }

        private void MethodWithUintRange([Range(11u, 15u)] uint x) { }

        [Test]
        public void UintRange_Reversed()
        {
            Assert.Throws<ArgumentException>(() => CheckValues(nameof(MethodWithUintRange_Reversed), 15u, 14u, 13u, 12u, 11u));
        }

        private void MethodWithUintRange_Reversed([Range(15u, 11u)] uint x) { }

        [Test]
        public void UintRange_FromEqualsTo()
        {
            CheckValues(nameof(MethodWithUintRange_FromEqualsTo), 11u);
        }

        private void MethodWithUintRange_FromEqualsTo([Range(11u, 11u)] uint x) { }

        [Test]
        public void UintRangeAndStep()
        {
            CheckValues(nameof(MethodWithUintRangeAndStep), 11u, 13u, 15u);
        }

        private void MethodWithUintRangeAndStep([Range(11u, 15u, 2u)] uint x) { }

        [Test]
        public void UintRangeAndZeroStep()
        {
            Assert.Throws<ArgumentException>(() => CheckValues(nameof(MethodWithUintRangeAndZeroStep), 11u, 12u, 13u));
        }

        private void MethodWithUintRangeAndZeroStep([Range(11u, 15u, 0u)] uint x) { }

        [Test]
        public void UintRangeAndStep_Reversed()
        {
            Assert.Throws<ArgumentException>(() => CheckValues(nameof(MethodWithUintRangeAndStep_Reversed), 11u, 13u, 15u));
        }

        private void MethodWithUintRangeAndStep_Reversed([Range(15u, 11u, 2u)] uint x) { }

        #endregion

        #region Longs

        [Test]
        public void LongRange()
        {
            CheckValues(nameof(MethodWithLongRange), 11L, 12L, 13L, 14L, 15L);
        }

        private void MethodWithLongRange([Range(11L, 15L)] long x) { }

        [Test]
        public void LongRange_Reversed()
        {
            CheckValues(nameof(MethodWithLongRange_Reversed), 15L, 14L, 13L, 12L, 11L);
        }

        private void MethodWithLongRange_Reversed([Range(15L, 11L)] long x) { }

        [Test]
        public void LongRange_FromEqualsTo()
        {
            CheckValues(nameof(MethodWithLongRange_FromEqualsTo), 11L);
        }

        private void MethodWithLongRange_FromEqualsTo([Range(11L, 11L)] long x) { }

        [Test]
        public void LongRangeAndStep()
        {
            CheckValues(nameof(MethodWithLongRangeAndStep), 11L, 13L, 15L);
        }

        private void MethodWithLongRangeAndStep([Range(11L, 15L, 2)] long x) { }

        [Test]
        public void LongRangeAndZeroStep()
        {
            Assert.Throws<ArgumentException>(() => CheckValues(nameof(MethodWithLongRangeAndZeroStep), 11L, 12L, 13L));
        }

        private void MethodWithLongRangeAndZeroStep([Range(11L, 15L, 0L)] long x) { }

        [Test]
        public void LongRangeAndStep_Reversed()
        {
            Assert.Throws<ArgumentException>(() => CheckValues(nameof(MethodWithLongRangeAndStep_Reversed), 11L, 13L, 15L));
        }

        private void MethodWithLongRangeAndStep_Reversed([Range(15L, 11L, 2L)] long x) { }

        [Test]
        public void LongRangeAndNegativeStep()
        {
            Assert.Throws<ArgumentException>(() => CheckValues(nameof(MethodWithLongRangeAndNegativeStep), 11L, 13L, 15L));
        }

        private void MethodWithLongRangeAndNegativeStep([Range(11L, 15L, -2L)] long x) { }

        [Test]
        public void LongRangeAndNegativeStep_Reversed()
        {
            CheckValues(nameof(MethodWithLongRangeAndNegativeStep_Reversed), 15L, 13L, 11L);
        }

        private void MethodWithLongRangeAndNegativeStep_Reversed([Range(15L, 11L, -2L)] long x) { }

        #endregion

        #region Unsigned Longs

        [Test]
        public void UlongRange()
        {
            CheckValues(nameof(MethodWithUlongRange), 11ul, 12ul, 13ul, 14ul, 15ul);
        }

        private void MethodWithUlongRange([Range(11ul, 15ul)] ulong x) { }

        [Test]
        public void UlongRange_Reversed()
        {
            Assert.Throws<ArgumentException>(() => CheckValues(nameof(MethodWithUlongRange_Reversed), 15ul, 14ul, 13ul, 12ul, 11ul));
        }

        private void MethodWithUlongRange_Reversed([Range(15ul, 11ul)] ulong x) { }

        [Test]
        public void UlongRange_FromEqualsTo()
        {
            CheckValues(nameof(MethodWithUlongRange_FromEqualsTo), 11ul);
        }

        private void MethodWithUlongRange_FromEqualsTo([Range(11ul, 11ul)] ulong x) { }

        [Test]
        public void UlongRangeAndStep()
        {
            CheckValues(nameof(MethodWithUlongRangeAndStep), 11ul, 13ul, 15ul);
        }

        private void MethodWithUlongRangeAndStep([Range(11ul, 15ul, 2ul)] ulong x) { }

        [Test]
        public void UlongRangeAndZeroStep()
        {
            Assert.Throws<ArgumentException>(() => CheckValues(nameof(MethodWithUlongRangeAndZeroStep), 11ul, 12ul, 13ul));
        }

        private void MethodWithUlongRangeAndZeroStep([Range(11ul, 15ul, 0ul)] ulong x) { }

        [Test]
        public void UlongRangeAndStep_Reversed()
        {
            Assert.Throws<ArgumentException>(() => CheckValues(nameof(MethodWithUlongRangeAndStep_Reversed), 11ul, 13ul, 15ul));
        }

        private void MethodWithUlongRangeAndStep_Reversed([Range(15ul, 11ul, 2ul)] ulong x) { }

        #endregion

        #region Doubles

        [Test]
        public void DoubleRangeAndStep()
        {
            CheckValuesWithinTolerance(nameof(MethodWithDoubleRangeAndStep), 0.7, 0.9, 1.1);
        }

        private void MethodWithDoubleRangeAndStep([Range(0.7, 1.2, 0.2)] double x) { }

        [Test]
        public void DoubleRangeAndZeroStep()
        {
            Assert.Throws<ArgumentException>(() => CheckValuesWithinTolerance(nameof(MethodWithDoubleRangeAndZeroStep), 0.7, 0.9, 1.1));
        }

        private void MethodWithDoubleRangeAndZeroStep([Range(0.7, 1.2, 0.0)] double x) { }

        [Test]
        public void DoubleRangeAndStep_Reversed()
        {
            Assert.Throws<ArgumentException>(() => CheckValuesWithinTolerance(nameof(MethodWithDoubleRangeAndStep_Reversed), 0.7, 0.9, 1.1));
        }

        private void MethodWithDoubleRangeAndStep_Reversed([Range(1.2, 0.7, 0.2)] double x) { }

        [Test]
        public void DoubleRangeAndNegativeStep()
        {
            Assert.Throws<ArgumentException>(() => CheckValuesWithinTolerance(nameof(MethodWithDoubleRangeAndNegativeStep), 0.7, 0.9, 1.1));
        }

        private void MethodWithDoubleRangeAndNegativeStep([Range(0.7, 1.2, -0.2)] double x) { }

        [Test]
        public void DoubleRangeAndNegativeStep_Reversed()
        {
            CheckValuesWithinTolerance(nameof(MethodWithDoubleRangeAndNegativeStep_Reversed), 1.2, 1.0, 0.8);
        }

        private void MethodWithDoubleRangeAndNegativeStep_Reversed([Range(1.2, 0.7, -0.2)] double x) { }

        #endregion

        #region Floats

        [Test]
        public void FloatRangeAndStep()
        {
            CheckValuesWithinTolerance(nameof(MethodWithFloatRangeAndStep), 0.7f, 0.9f, 1.1f);
        }

        private void MethodWithFloatRangeAndStep([Range(0.7f, 1.2f, 0.2f)] float x) { }

        [Test]
        public void FloatRangeAndZeroStep()
        {
            Assert.Throws<ArgumentException>(() => CheckValuesWithinTolerance(nameof(MethodWithFloatRangeAndZeroStep), 0.7f, 0.9f, 1.1f));
        }

        private void MethodWithFloatRangeAndZeroStep([Range(0.7f, 1.2f, 0.0f)] float x) { }

        [Test]
        public void FloatRangeAndStep_Reversed()
        {
            Assert.Throws<ArgumentException>(() => CheckValuesWithinTolerance(nameof(MethodWithFloatRangeAndStep_Reversed), 0.7f, 0.9f, 1.1f));
        }

        private void MethodWithFloatRangeAndStep_Reversed([Range(1.2f, 0.7, 0.2f)] float x) { }

        [Test]
        public void FloatRangeAndNegativeStep()
        {
            Assert.Throws<ArgumentException>(() => CheckValuesWithinTolerance(nameof(MethodWithFloatRangeAndNegativeStep), 0.7f, 0.9f, 1.1f));
        }

        private void MethodWithFloatRangeAndNegativeStep([Range(0.7f, 1.2f, -0.2f)] float x) { }

        [Test]
        public void FloatRangeAndNegativeStep_Reversed()
        {
            CheckValuesWithinTolerance(nameof(MethodWithFloatRangeAndNegativeStep_Reversed), 1.2f, 1.0f, 0.8f);
        }

        private void MethodWithFloatRangeAndNegativeStep_Reversed([Range(1.2f, 0.7f, -0.2f)] float x) { }

        #endregion

        #region Conversions

        [Test]
        public void CanConvertDoubleRangeToDecimal()
        {
            CheckValues(nameof(MethodWithDoubleRangeAndDecimalArgument), 1.0M, 1.1M, 1.2M);
        }

        // Use max of 1.21 rather than 1.3 so rounding won't give an extra value
        private void MethodWithDoubleRangeAndDecimalArgument([Range(1.0, 1.21, 0.1)] decimal x) { }

        #endregion

        #region Helper Methods

        private void CheckValues(string methodName, params object[] expected)
        {
            var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            var param = method.GetParameters()[0];
#if NETCOREAPP1_1
            var attr = param.GetCustomAttributes(typeof(RangeAttribute), false).First() as RangeAttribute;
#else
            var attr = param.GetCustomAttributes(typeof(RangeAttribute), false)[0] as RangeAttribute;
#endif
            Assert.That(attr.GetData(GetType(), param), Is.EqualTo(expected));
        }

        private void CheckValuesWithinTolerance(string methodName, params object[] expected)
        {
            var method = GetType().GetMethod(methodName, BindingFlags.NonPublic | BindingFlags.Instance);
            var param = method.GetParameters()[0];
#if NETCOREAPP1_1
            var attr = param.GetCustomAttributes(typeof(RangeAttribute), false).First() as RangeAttribute;
#else
            var attr = param.GetCustomAttributes(typeof(RangeAttribute), false)[0] as RangeAttribute;
#endif
            Assert.That(attr.GetData(GetType(), param),
                Is.EqualTo(expected).Within(0.000001));
        }

        #endregion
    }
}
