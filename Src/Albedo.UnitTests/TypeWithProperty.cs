using System.Reflection;

namespace Ploeh.Albedo.UnitTests
{
    internal class TypeWithProperty
    {
        public static PropertyInfo Property
        {
            get
            {
                return typeof (TypeWithProperty).GetProperty("TheProperty");
            }
        }
        public static PropertyInfo OtherProperty
        {
            get
            {
                return typeof(TypeWithProperty).GetProperty("TheOtherProperty");
            }
        }

        public int TheProperty { get; set; }
        public int TheOtherProperty { get; set; }
    }
}