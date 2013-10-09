﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class CompositeReflectionElementTest
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new CompositeReflectionElement();
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new CompositeReflectionElement();
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                sut.Accept((IReflectionVisitor<object>) null));
            // Teardown
        }

        [Fact]
        public void AcceptCallsAcceptOnAllElementTypes()
        {
            // Fixture setup
            var observedElements = new List<IReflectionElement>();
            var elements = new IReflectionElement[]
            {
                new AssemblyElement(this.GetType().Assembly),
                new TypeElement(this.GetType()),
                new ConstructorInfoElement(this.GetType().GetConstructors().First()),
                new MethodInfoElement(typeof (MethodInfoElement).GetMethods().First()),
                new PropertyInfoElement(typeof (PropertyInfoElement).GetProperties().First()),
                new FieldInfoElement(typeof (string).GetFields().First()),
                new ParameterInfoElement(typeof (ParameterInfoElement).GetMethods().Skip(1).First().GetParameters()[0]),
            };

            var expectedElements = new List<IReflectionElement>(elements);

            var dummyVisitor = new DelegatingReflectionVisitor<int>
            {
                OnVisitAssemblyElement = observedElements.Add,
                OnVisitTypeElement = observedElements.Add,
                OnVisitConstructorInfoElement = observedElements.Add,
                OnVisitFieldInfoElement = observedElements.Add,
                OnVisitMethodInfoElement = observedElements.Add,
                OnVisitParameterInfoElement = observedElements.Add,
                OnVisitPropertyInfoElement = observedElements.Add,
            };

            var sut = new CompositeReflectionElement(elements);
            // Exercise system
            sut.Accept(dummyVisitor);
            // Verify outcome
            Assert.True(expectedElements.SequenceEqual(observedElements));
            // Teardown
        }
    }
}