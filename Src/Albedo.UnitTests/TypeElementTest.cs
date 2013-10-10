using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class TypeElementTest
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            var type = this.GetType();
            // Exercise system
            var sut = new TypeElement(type);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void TypeIsCorrect()
        {
            // Fixture setup
            var type = this.GetType();
            var sut = new TypeElement(type);
            // Exercise system
            var actual = sut.Type;
            // Verify outcome
            Assert.Equal(type, actual);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullTypeThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new TypeElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new TypeElement(this.GetType());
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Accept((IReflectionVisitor<object>)null));
            // Teardown
        }

        [Fact]
        public void AcceptCallsVisitOnceWithCorrectType()
        {
            // Fixture setup
            var observed = new List<TypeElement>();
            var dummyVisitor = new DelegatingReflectionVisitor<int> { OnVisitTypeElement = observed.Add };
            var sut = new TypeElement(this.GetType());
            // Exercise system
            sut.Accept(dummyVisitor);
            // Verify outcome
            Assert.True(new[] { sut }.SequenceEqual(observed));
            // Teardown
        }

        class TypeWithCtorMethodPropertyField
        {
            public void Method1(int methodParam1)
            {
            }

            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA1801:ReviewUnusedParameters", MessageId = "ctorParam1", Justification = "It's used via reflection.")]
            public TypeWithCtorMethodPropertyField(int ctorParam1)
            {
            }

            public int Property1 { get; set; }

#pragma warning disable 649
            [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields", Justification = "It's used via reflection.")]
            public int Field1;
#pragma warning restore 649
        }
    }
}
