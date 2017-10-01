﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Albedo.Refraction;
using Xunit;

namespace Albedo.UnitTests.Refraction
{
    public class PropertyInfoElementRefractionTests
    {
        [Fact]
        public void SutIsReflectionElementRefraction()
        {
            var sut = new PropertyInfoElementRefraction<object>();
            Assert.IsAssignableFrom<IReflectionElementRefraction<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void RefractObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new PropertyInfoElementRefraction<object>();

            var actual = sut.Refract(objects);

            var expected = objects
                .OfType<PropertyInfo>()
                .Select(mi => new PropertyInfoElement(mi))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RefractNullSourceThrows()
        {
            var sut = new PropertyInfoElementRefraction<object>();
            Assert.Throws<ArgumentNullException>(() => sut.Refract(null));
        }

        private class SourceObjects : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[]
                {
                    new object[]
                    {
                        new Properties<Version>().Select(p => p.Major)
                    }
                };
                yield return new object[]
                {
                    new object[]
                    {
                        new Properties<Version>().Select(p => p.Major),
                        new Properties<Version>().Select(p => p.Minor)
                    }
                };
                yield return new object[]
                {
                    new object[]
                    {
                        new Properties<Version>().Select(p => p.Major),
                        "",
                        new Properties<Version>().Select(p => p.Minor)
                    }
                };
            }

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}