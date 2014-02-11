using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    internal class TypeWithField
    {
        public static FieldInfo Field
        {
            get
            {
                return typeof(TypeWithField).GetField("TheField");
            }
        }

        public static FieldInfo OtherField
        {
            get
            {
                return typeof(TypeWithField).GetField("TheOtherField");
            }
        }

        public int TheField = 0;
        public int TheOtherField = 1;
    }
}