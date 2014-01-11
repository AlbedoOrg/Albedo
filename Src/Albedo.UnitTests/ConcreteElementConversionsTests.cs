using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class ConcreteElementConversionsTests
    {
        [Fact]
        public void ToElementForAssemblyThrowsOnNullAssembly()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ConcreteElementConversions.ToElement((Assembly) null));
        }

        [Fact]
        public void ToElementForConstructorInfoThrowsOnNullConstructorInfo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ConcreteElementConversions.ToElement((ConstructorInfo) null));
        }

        [Fact]
        public void ToElementForEventInfoThrowsOnNullEventInfo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ConcreteElementConversions.ToElement((EventInfo) null));
        }

        [Fact]
        public void ToElementForFieldInfoThrowsOnNullFieldInfo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ConcreteElementConversions.ToElement((FieldInfo) null));
        }

        [Fact]
        public void ToElementForLocalVariableInfoThrowsOnNullLocalVariableInfo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ConcreteElementConversions.ToElement((LocalVariableInfo) null));
        }

        [Fact]
        public void ToElementForMethodInfoThrowsOnNullMethodInfo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ConcreteElementConversions.ToElement((MethodInfo) null));
        }

        [Fact]
        public void ToElementForParameterInfoThrowsOnNullParameterInfo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ConcreteElementConversions.ToElement((ParameterInfo) null));
        }

        [Fact]
        public void ToElementForPropertyInfoThrowsOnNullPropertyInfo()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ConcreteElementConversions.ToElement((PropertyInfo) null));
        }

        [Fact]
        public void ToElementForTypeThrowsOnNullType()
        {
            Assert.Throws<ArgumentNullException>(() =>
                ConcreteElementConversions.ToElement((Type) null));
        }
    }
}
