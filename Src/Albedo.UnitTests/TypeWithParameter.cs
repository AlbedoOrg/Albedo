using System.Linq;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    internal class TypeWithParameter
    {
        public static ParameterInfo Parameter
        {
            get
            {
                return typeof(TypeWithParameter)
                    .GetMethod("TheMethod")
                    .GetParameters()
                    .First();
            }
        }

        public static ParameterInfo OtherParameter
        {
            get
            {
                return typeof(TypeWithParameter)
                    .GetMethod("TheOtherMethod")
                    .GetParameters()
                    .First();
            }
        }

        public void TheMethod(int param1)
        {
        }

        public void TheOtherMethod(int param1)
        {
        }
    }
}