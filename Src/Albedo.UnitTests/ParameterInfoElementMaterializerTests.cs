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
    public class ParameterInfoElementMaterializerTests
    {
        [Fact]
        public void SutIsReflectionElementMaterializer()
        {
            var sut = new ParameterInfoElementMaterializer<object>();
            Assert.IsAssignableFrom<IReflectionElementMaterializer<object>>(sut);
        }

        [Theory, ClassData(typeof(SourceObjects))]
        public void MaterializeObjectsReturnsCorrectResult(object[] objects)
        {
            var sut = new ParameterInfoElementMaterializer<object>();

            var actual = sut.Materialize(objects);

            var expected = objects
                .OfType<ParameterInfo>()
                .Select(mi => new ParameterInfoElement(mi))
                .Cast<IReflectionElement>();
            Assert.Equal(expected, actual);
        }

         [Fact]
         public void MaterializeNullSourceThrows()
         {
             var sut = new ParameterInfoElementMaterializer<object>();
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
                        new Methods<Version>().Select(v => v.Clone())
                    }
                };
                yield return new object[]
                {
                    new object[]
                    {
                        new Methods<Version>().Select(v => v.Clone()),
                        new Methods<Version>().Select(v => v.CompareTo(null))
                    }
                };
                yield return new object[]
                {
                    new object[]
                    {
                        new Methods<Version>().Select(v => v.Clone()),
                        "",
                        new Methods<Version>().Select(v => v.CompareTo(null))
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