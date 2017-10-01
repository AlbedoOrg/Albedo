using System.Reflection;

namespace Albedo.UnitTests
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

        public static int StaticField = 2;
#pragma warning disable 414
        private int privateField = 3;
#pragma warning restore 414
    }
}