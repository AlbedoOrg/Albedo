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

        public void TheMethod()
        {
        }

        public void TheOtherMethod()
        {
        }

        public object PropertyToConfirmExceptAccessors { get; set; }
    }
}