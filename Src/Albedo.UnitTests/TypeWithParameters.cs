using System.Linq;
using System.Reflection;

namespace Albedo.UnitTests
{
    internal class TypeWithParameters
    {
        public static ParameterInfo Parameter
        {
            get
            {
                return typeof(TypeWithParameters)
                    .GetMethod("TheMethod")
                    .GetParameters()
                    .First();
            }
        }

        public static ParameterInfo OtherParameter
        {
            get
            {
                return typeof(TypeWithParameters)
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