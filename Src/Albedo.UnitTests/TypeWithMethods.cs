using System;
using System.Collections.Generic;
using System.Reflection;

namespace Albedo.UnitTests
{
    internal class TypeWithMethods
    {
        public static MethodInfo Method
        {
            get
            {
                return typeof(TypeWithMethods).GetMethod("TheMethod");
            }
        }

        public static MethodInfo OtherMethod
        {
            get
            {
                return typeof(TypeWithMethods).GetMethod("TheOtherMethod");
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

        private object PrivatePropertyAccessorsToBeExcepted { get; set; }

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

        private void ThePrivateMethod()
        {
        }
    }
}