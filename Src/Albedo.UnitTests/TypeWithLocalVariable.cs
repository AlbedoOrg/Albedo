using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    internal class TypeWithLocalVariable
    {
        public static LocalVariableInfo LocalVariable
        {
            get
            {
                return typeof(TypeWithLocalVariable)
                    .GetMethod("TheMethod")
                    .GetMethodBody()
                    .LocalVariables[0];
            }
        }

        public static LocalVariableInfo OtherLocalVariable
        {
            get
            {
                return typeof(TypeWithLocalVariable)
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