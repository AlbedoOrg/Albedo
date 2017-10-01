using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Albedo.Refraction;
using Xunit;

namespace Albedo.UnitTests.Refraction
{
    public class MethodInfoElementRefractionTests
    {
        [Fact]
        public void SutIsReflectionElementRefraction()
        {
            var sut = new MethodInfoElementRefraction<object>();
            Assert.IsAssignableFrom<IReflectionElementRefraction<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void RefractObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new MethodInfoElementRefraction<object>();

            var actual = sut.Refract(objects);

            var expected = objects
                .OfType<MethodInfo>()
                .Select(mi => new MethodInfoElement(mi))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

         [Fact]
         public void RefractNullSourceThrows()
         {
             var sut = new MethodInfoElementRefraction<object>();
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
                        typeof(AppDomain).GetMethods().First()
                    }
                };
                yield return new[]
                {
                    typeof(AppDomain).GetMethods().Take(2).ToArray()
                };
                yield return new[]
                {
                    new object[]
                    {
                        typeof(AppDomain).GetMethods().First(),
                        "",
                        typeof(AppDomain).GetMethods().Skip(1).First(),
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