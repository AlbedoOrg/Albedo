using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Albedo.UnitTests
{
    internal class TypeWithCtors
    {
        public static ConstructorInfo Ctor
        {
            get
            {
                return typeof(TypeWithCtors).GetConstructors().Single(c => c.GetParameters().Length == 0);
            }
        }

        public static ConstructorInfo OtherCtor
        {
            get
            {
                return typeof(TypeWithCtors).GetConstructors().Single(c => c.GetParameters().Length == 3);
            }
        }

        public static IList<LocalVariableInfo> LocalVariablesOfOtherCtor
        {
            get
            {
                return OtherCtor.GetMethodBody().LocalVariables;
            }
        }

        public TypeWithCtors()
        {
        }

        public TypeWithCtors(object arg1, int arg2, string arg3)
        {
            // This is required to prevent the compiler from
            // warning and optimising away the local variable.
            int local1 = 1;
            string local2 = "2";
            local2 = local1 + local2;
        }

        private TypeWithCtors(object privateCtorArgument)
        {
        }
    }
}