using System.Reflection;

namespace Albedo.UnitTests
{
    internal class TypeWithLocalVariables
    {
        public static LocalVariableInfo LocalVariable
        {
            get
            {
                return typeof(TypeWithLocalVariables)
                    .GetMethod("TheMethod")
                    .GetMethodBody()
                    .LocalVariables[0];
            }
        }

        public static LocalVariableInfo OtherLocalVariable
        {
            get
            {
                return typeof(TypeWithLocalVariables)
                    .GetMethod("TheOtherMethod")
                    .GetMethodBody()
                    .LocalVariables[0];
            }
        }

        public void TheMethod()
        {
            // This is required to prevent the compiler from
            // warning and optimising away the local variable.
            var local = 1;
            local = local + 1;
            local = local + 2;
        }

        public void TheOtherMethod()
        {
            // This is required to prevent the compiler from
            // warning and optimising away the local variable.
            var local = 1;
            local = local + 1;
            local = local + 2;
        }
    }
}