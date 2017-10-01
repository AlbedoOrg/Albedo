﻿using System;
using System.Linq;
using Albedo.Refraction;
using Xunit;

namespace Albedo.UnitTests.Refraction
{
    public class TypeElementRefractionTests
    {
        [Fact]
        public void SutIsReflectionElementRefraction()
        {
            var sut = new TypeElementRefraction<object>();
            Assert.IsAssignableFrom<IReflectionElementRefraction<object>>(sut);
        }

        [Theory]
        [InlineData(new object[] { new[] { typeof(Version) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElement) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElementTest) } })]
        [InlineData(new object[] { new[] { typeof(AssemblyElement), typeof(Version) } })]
        [InlineData(new object[] { new object[] { typeof(AssemblyElement), "", typeof(Version) } })]
        public void RefractTypesReturnsCorrectResult(object[] types)
        {
            var sut = new TypeElementRefraction<object>();

            var actual = sut.Refract(types);

            var expected = types
                .OfType<Type>()
                .Select(t => new TypeElement(t))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RefractNullSourceThrows()
        {
            var sut = new TypeElementRefraction<object>();
            Assert.Throws<ArgumentNullException>(() => sut.Refract(null));
        }
    }
}
