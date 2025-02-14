// Copyright (c) Charlie Poole, Rob Prouse and Contributors. MIT License - see LICENSE.txt

namespace NUnit.Framework.Interfaces
{
    /// <summary>
    /// Common interface supported by all representations
    /// of a test. Only includes informational fields.
    /// The Run method is specifically excluded to allow
    /// for data-only representations of a test.
    /// </summary>
    public interface ITest : IXmlNodeBuilder
    {
        /// <summary>
        /// Gets the id of the test
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Gets the name of the test
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the type of the test
        /// </summary>
        string TestType { get; }

        /// <summary>
        /// Gets the fully qualified name of the test
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Gets the name of the class containing this test. Returns
        /// null if the test is not associated with a class.
        /// </summary>
        string? ClassName { get; }

        /// <summary>
        /// Gets the name of the method implementing this test.
        /// Returns null if the test is not implemented as a method.
        /// </summary>
        string? MethodName { get; }

        /// <summary>
        /// Gets the Type of the test fixture, if applicable, or
        /// null if no fixture type is associated with this test.
        /// </summary>
        ITypeInfo? TypeInfo { get; }

        /// <summary>
        /// Gets the method which declares the test, or <see langword="null"/>
        /// if no method is associated with this test.
        /// </summary>
        IMethodInfo? Method { get; }

        /// <summary>
        /// Gets the RunState of the test, indicating whether it can be run.
        /// </summary>
        RunState RunState { get; }

        /// <summary>
        /// Count of the test cases ( 1 if this is a test case )
        /// </summary>
        int TestCaseCount { get; }

        /// <summary>
        /// Gets the properties of the test
        /// </summary>
        IPropertyBag Properties { get; }

        /// <summary>
        /// Gets the parent test, if any.
        /// </summary>
        /// <value>The parent test or null if none exists.</value>
        ITest? Parent { get; }

        /// <summary>
        /// Returns true if this is a test suite
        /// </summary>
        bool IsSuite { get; }

        /// <summary>
        /// Gets a bool indicating whether the current test
        /// has any descendant tests.
        /// </summary>
        bool HasChildren { get; }

        /// <summary>
        /// Gets this test's child tests
        /// </summary>
        /// <value>A list of child tests</value>
        System.Collections.Generic.IList<ITest> Tests { get; }

        /// <summary>
        /// Gets a fixture object for running this test.
        /// </summary>
        object? Fixture { get; }

        /// <summary>
        /// The arguments to use in creating the test or empty array if none are required.
        /// </summary>
        object?[] Arguments { get; }
    }
}

