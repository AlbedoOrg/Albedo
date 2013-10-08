using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class CompositeHierarchicalReflectionElementTest
    {
        [Fact]
        public void SutIsHierarchicalReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new CompositeHierarchicalReflectionElement();
            // Verify outcome
            Assert.IsAssignableFrom<IHierarchicalReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new CompositeHierarchicalReflectionElement();
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Accept((IHierarchicalReflectionVisitor<object>) null));
            // Teardown
        }

        [Fact]
        public void AcceptHierachicalCallsAcceptOnAllMultipleElementTypes()
        {
            // Fixture setup
            var observedElements = new List<IHierarchicalReflectionElement>();
            var elements = new IHierarchicalReflectionElement[]
            {
                new AssemblyElement(this.GetType().Assembly),
                new TypeElement(this.GetType()),
                new ConstructorInfoElement(this.GetType().GetConstructors().First()),
                new MethodInfoElement(typeof (MethodInfoElement).GetMethods().First()),
                new PropertyInfoElement(typeof (PropertyInfoElement).GetProperties().First()),
                new FieldInfoElement(typeof (string).GetFields().First()),
                new ParameterInfoElement(typeof (ParameterInfoElement).GetMethods().Skip(1).First().GetParameters()[0]),
            };

            var expectedElements = new List<IHierarchicalReflectionElement>(elements);

            var dummyVisitor = new DelegatingHierarchicalReflectionVisitor<int>
            {
                OnEnterAssemblyElement = observedElements.Add,
                OnEnterTypeElement = observedElements.Add,
                OnEnterConstructorInfoElement = observedElements.Add,
                OnVisitFieldInfoElement = observedElements.Add,
                OnEnterMethodInfoElement = observedElements.Add,
                OnVisitParameterInfoElement = observedElements.Add,
                OnVisitPropertyInfoElement = observedElements.Add,
            };

            var sut = new CompositeHierarchicalReflectionElement(elements);
            // Exercise system
            sut.Accept(dummyVisitor);
            // Verify outcome
            // TODO: do better than just 'contains'
            Assert.True(expectedElements.All(observedElements.Contains));
            // Teardown
        }

    }
}