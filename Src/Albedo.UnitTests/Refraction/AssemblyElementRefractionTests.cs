using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Albedo.Refraction;
using Xunit;

namespace Albedo.UnitTests.Refraction
{
    public class AssemblyElementRefractionTests
    {
        [Fact]
        public void SutIsReflectionElementRefraction()
        {
            var sut = new AssemblyElementRefraction<object>();
            Assert.IsAssignableFrom<IReflectionElementRefraction<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void RefractAssembliesReturnsCorrectResult(
            object[] objects)
        {
            var sut = new AssemblyElementRefraction<object>();

            var actual = sut.Refract(objects);

            var expected = objects
                .OfType<Assembly>()
                .Select(a => new AssemblyElement(a))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void RefractNullSourceThrows()
        {
            var sut = new AssemblyElementRefraction<object>();
            Assert.Throws<ArgumentNullException>(() => sut.Refract(null));
        }

        private class SourceObjects : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new[]
                {
                    new object[]
                    {
                        typeof(Version).Assembly
                    } 
                };
                yield return new[]
                {
                    new object[]
                    {
                        typeof(AssemblyElement).Assembly
                    } 
                };
                yield return new[]
                {
                    new object[]
                    {
                        typeof(AssemblyElementTest).Assembly
                    } 
                };
                yield return new[]
                {
                    new object[]
                    {
                        typeof(AssemblyElementTest).Assembly,
                        typeof(Version).Assembly
                    } 
                };
                yield return new[]
                {
                    new object[]
                    {
                        typeof(AssemblyElementTest).Assembly,
                        2,
                        typeof(Version).Assembly
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
