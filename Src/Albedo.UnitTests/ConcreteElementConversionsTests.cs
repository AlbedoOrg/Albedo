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
        public void ToElementForAssemblyReturnsTheCorrectAssembly()
        {
            Assembly expected = typeof (TypeWithAllElements).Assembly;
            AssemblyElement actual = expected.ToElement();
            Assert.Equal(new AssemblyElement(expected), actual);
        }

        [Fact]
        public void ToElementForConstructorInfoReturnsTheCorrectElement()
        {
            ConstructorInfo expected = typeof (TypeWithAllElements).GetConstructors().Single();
            ConstructorInfoElement actual = expected.ToElement();
            Assert.Equal(new ConstructorInfoElement(expected), actual);
        }

        [Fact]
        public void ToElementForEventInfoReturnsTheCorrectElement()
        {
            EventInfo expected = typeof(TypeWithAllElements).GetEvents().Single();
            EventInfoElement actual = expected.ToElement();
            Assert.Equal(new EventInfoElement(expected), actual);
        }

        [Fact]
        public void ToElementForFieldInfoReturnsTheCorrectElement()
        {
            FieldInfo expected = typeof(TypeWithAllElements).GetFields().Single();
            FieldInfoElement actual = expected.ToElement();
            Assert.Equal(new FieldInfoElement(expected), actual);
        }

        [Fact]
        public void ToElementForLocalVariableInfoReturnsTheCorrectElement()
        {
            LocalVariableInfo expected = TypeWithAllElements.LocalVariableInfo;
            LocalVariableInfoElement actual = expected.ToElement();
            Assert.Equal(new LocalVariableInfoElement(expected), actual);
        }

        [Fact]
        public void ToElementForMethodInfoReturnsTheCorrectElement()
        {
            MethodInfo expected = typeof (TypeWithAllElements).GetMethods()[0];
            MethodInfoElement actual = expected.ToElement();
            Assert.Equal(new MethodInfoElement(expected), actual);
        }

        [Fact]
        public void ToElementForParameterInfoReturnsTheCorrectElement()
        {
            ParameterInfo expected = TypeWithAllElements.ParameterInfo;
            ParameterInfoElement actual = expected.ToElement();
            Assert.Equal(new ParameterInfoElement(expected), actual);
        }

        [Fact]
        public void ToElementForPropertyInfoReturnsTheCorrectElement()
        {
            PropertyInfo expected = typeof (TypeWithAllElements).GetProperty("TheProperty");
            PropertyInfoElement actual = expected.ToElement();
            Assert.Equal(new PropertyInfoElement(expected), actual);
        }

        [Fact]
        public void ToElementForTypeReturnsTheCorrectElement()
        {
            Type expected = typeof (TypeWithAllElements);
            TypeElement actual = expected.ToElement();
            Assert.Equal(new TypeElement(expected), actual);
        }
        
        class TypeWithAllElements
        {
#pragma warning disable 67
            public event EventHandler TheOnlyEvent;
#pragma warning restore 67

#pragma warning disable 169
#pragma warning disable 649
            public int TheOnlyField;
#pragma warning restore 169
#pragma warning restore 649

            public static void MethodWithLocal(int parameter)
            {
                // This is required to prevent the compiler from
                // warning and optimising away the local variable.
                var local = 1;
                local = local + 1;
                local = local + 2;
            }

            public static LocalVariableInfo LocalVariableInfo
            {
                get
                {
                    return typeof (TypeWithAllElements)
                        .GetMethod("MethodWithLocal")
                        .GetMethodBody()
                        .LocalVariables[0];
                }
            }

            public static ParameterInfo ParameterInfo
            {
                get
                {
                    return typeof (TypeWithAllElements)
                        .GetMethod("MethodWithLocal")
                        .GetParameters()[0];
                }
            }

            public static int TheProperty { get; set; }
        }

    }
}
