using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class Scenario
    {
        [Fact]
        public void DumpAssemblyAndTypesAndMethods()
        {
            var assemblyElements = new CompositeReflectionElement(
                new AssemblyElement(this.GetType().Assembly),
                new AssemblyElement(typeof(AssemblyElement).Assembly),
                new TypeElement(this.GetType()),
                new TypeElement(typeof(TypeElement)),
                new MethodInfoElement((MethodInfo)MethodBase.GetCurrentMethod()),
                new MethodInfoElement(typeof(MethodInfoElement).GetMethods().First()));

            Console.WriteLine(assemblyElements
                .Accept(new AssemblyAndTypeAndMethodPrinter())
                .Value
                .Aggregate((x, y) => x + Environment.NewLine + y));
        }

        class AssemblyAndTypeAndMethodPrinter : ReflectionVisitor<IList<string>>
        {
            private readonly IList<string> observations = new List<string>();

            public override IList<string> Value
            {
                get { return this.observations; }
            }

            public override IReflectionVisitor<IList<string>> Visit(AssemblyElement assemblyElement)
            {
                observations.Add(AssemblyToString(assemblyElement.Assembly));
                return this;
            }

            public override IReflectionVisitor<IList<string>> Visit(TypeElement typeElement)
            {
                observations.Add(ClassToString(typeElement.Type));
                return this;
            }

            public override IReflectionVisitor<IList<string>> Visit(MethodInfoElement methodInfoElement)
            {
                observations.Add(MethodToString(methodInfoElement.MethodInfo));
                return this;
            }
        }

        private static string AssemblyToString(Assembly a)
        {
            return string.Format("Assembly: {0}", a);
        }

        private static string MethodToString(MethodInfo m)
        {
            return string.Format("{0}{1}{2}()",
                m.IsPublic ? "public " : (m.IsPrivate ? "private " : m.IsAssembly ? "internal " : " "),
                (m.ReturnType == typeof(void) ? "void" : m.ReturnType.Name) + " ",
                m.Name);
        }

        private static string ClassToString(Type t)
        {
            bool isNestedAssembly = ((t.Attributes & TypeAttributes.NestedAssembly) == TypeAttributes.NestedAssembly);
            return string.Format("{0}{1}{2}{3}.{4}",
                t.IsPublic ? "public " : isNestedAssembly ? "internal " : "private ",
                t.IsAbstract ? "abstract " : "",
                t.IsInterface ? "interface " : "class ",
                t.Namespace,
                t.Name);
        }

    }
}
