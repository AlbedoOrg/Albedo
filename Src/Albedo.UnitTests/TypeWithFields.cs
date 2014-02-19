using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    internal class TypeWithFields
    {
        public static FieldInfo Field
        {
            get
            {
                return typeof(TypeWithFields).GetField("TheField");
            }
        }

        public static FieldInfo OtherField
        {
            get
            {
                return typeof(TypeWithFields).GetField("TheOtherField");
            }
        }

        public int TheField = 0;
        public int TheOtherField = 1;
    }
}