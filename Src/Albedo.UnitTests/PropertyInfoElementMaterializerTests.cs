using Ploeh.Albedo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;
using Xunit.Extensions;

namespace Ploeh.Albedo.UnitTests
{
    public class PropertyInfoElementMaterializerTests
    {
        [Fact]
        public void SutIsReflectionElementMaterializer()
        {
            var sut = new PropertyInfoElementMaterializer<object>();
            Assert.IsAssignableFrom<IReflectionElementMaterializer<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void MaterializeObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new PropertyInfoElementMaterializer<object>();

            var actual = sut.Materialize(objects);

            var expected = objects
                .OfType<PropertyInfo>()
                .Select(mi => new PropertyInfoElement(mi))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

         [Fact]
         public void MaterializeNullSourceThrows()
         {
             var sut = new PropertyInfoElementMaterializer<object>();
             Assert.Throws<ArgumentNullException>(() => sut.Materialize(null));
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