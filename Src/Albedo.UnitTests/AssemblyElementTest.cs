﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class AssemblyElementTest
    {
        [Fact]
        public void SutIsReflectionElement()
        {
            // Fixture setup
            // Exercise system
            var sut = new AssemblyElement(this.GetType().Assembly);
            // Verify outcome
            Assert.IsAssignableFrom<IReflectionElement>(sut);
            // Teardown
        }

        [Fact]
        public void AssemblyIsCorrect()
        {
            // Fixture setup
            var expectedAssembly = this.GetType().Assembly;
            var sut = new AssemblyElement(expectedAssembly);
            // Exercise system
            Assembly actualAssembly = sut.Assembly;
            // Verify outcome
            Assert.Equal(expectedAssembly, actualAssembly);
            // Teardown
        }

        [Fact]
        public void ConstructWithNullAssemblyThrows()
        {
            // Fixture setup
            // Exercise system
            // Verify outcome
            Assert.Throws<ArgumentNullException>(() =>
                new AssemblyElement(null));
            // Teardown
        }

        [Fact]
        public void AcceptNullVisitorThrows()
        {
            // Fixture setup
            var sut = new AssemblyElement(this.GetType().Assembly);
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
            var observed = new List<AssemblyElement>();
            var dummyVisitor = new DelegatingReflectionVisitor<int> { OnVisitAssemblyElement = observed.Add };
            var sut = new AssemblyElement(GetType().Assembly);
            // Exercise system
            sut.Accept(dummyVisitor);
            // Verify outcome
            Assert.True(new[] { sut }.SequenceEqual(observed));
            // Teardown
        }

    }
}
