using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Albedo.Refraction;
using Xunit;

namespace Albedo.UnitTests.Refraction
{
    public class FieldInfoElementRefractionTests
    {
        [Fact]
        public void SutIsReflectionElementRefraction()
        {
            var sut = new FieldInfoElementRefraction<object>();
            Assert.IsAssignableFrom<IReflectionElementRefraction<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void rObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new FieldInfoElementRefraction<object>();

            var actual = sut.Refract(objects);

            var expected = objects
                .OfType<FieldInfo>()
                .Select(fi => new FieldInfoElement(fi))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void rNullSourceThrows()
        {
            var sut = new FieldInfoElementRefraction<object>();
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
                        typeof(int).GetFields(BindingFlags.Static | BindingFlags.Public).First()
                    }
                };
                yield return new[]
                {
                    typeof(int).GetFields(BindingFlags.Static | BindingFlags.Public).Take(2).ToArray()
                };
                yield return new[]
                {
                    new object[]
                    {
                        typeof(int).GetFields(BindingFlags.Static | BindingFlags.Public).First(),
                        "",
                        typeof(int).GetFields(BindingFlags.Static | BindingFlags.Public).Skip(1).First()
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
