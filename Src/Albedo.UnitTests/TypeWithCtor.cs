using System.Linq;
using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    internal class TypeWithCtor
    {
        public static ConstructorInfo Ctor
        {
            get
            {
                return typeof(TypeWithCtor).GetConstructors().Single(c => c.GetParameters().Length == 0);
            }
        }

        public static ConstructorInfo OtherCtor
        {
            get
            {
                return typeof(TypeWithCtor).GetConstructors().Single(c => c.GetParameters().Length == 1);
            }
        }

        public TypeWithCtor()
        {
        }

        public TypeWithCtor(object argument)
        {
        }
    }
}