using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    internal class TypeWithMethod
    {
        public static MethodInfo Method
        {
            get
            {
                return typeof(TypeWithMethod).GetMethod("TheMethod");
            }
        }

        public static MethodInfo OtherMethod
        {
            get
            {
                return typeof(TypeWithMethod).GetMethod("TheOtherMethod");
            }
        }

        public static IList<LocalVariableInfo> LocalVariablesOfOtherMethod
        {
            get
            {
                if (OtherMethod.GetMethodBody().LocalVariables == null)
                    throw new NotImplementedException();
                return OtherMethod.GetMethodBody().LocalVariables;
            }
        }

        public void TheMethod()
        {
        }

        public void TheOtherMethod(object arg1, int arg2, string arg3)
        {
            // This is required to prevent the compiler from
            // warning and optimising away the local variable.
            int local1 = 1;
            string local2 = "2";
            local2 = local1 + local2;
        }
    }
}