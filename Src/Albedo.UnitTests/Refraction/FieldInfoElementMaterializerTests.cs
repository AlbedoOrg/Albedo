using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;
using Ploeh.Albedo;
using Xunit.Extensions;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    public class FieldInfoElementMaterializerTests
    {
        [Fact]
        public void SutIsReflectionElementMaterializer()
        {
            var sut = new FieldInfoElementMaterializer<object>();
            Assert.IsAssignableFrom<IReflectionElementMaterializer<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void MaterializeObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new FieldInfoElementMaterializer<object>();

            var actual = sut.Materialize(objects);

            var expected = objects
                .OfType<FieldInfo>()
                .Select(fi => new FieldInfoElement(fi))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void MaterializeNullSourceThrows()
        {
            var sut = new FieldInfoElementMaterializer<object>();
            Assert.Throws<ArgumentNullException>(() => sut.Materialize(null));
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
