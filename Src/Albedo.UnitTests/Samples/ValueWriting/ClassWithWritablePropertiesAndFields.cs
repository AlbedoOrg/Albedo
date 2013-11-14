using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ploeh.Albedo.UnitTests.Samples.ValueWriting
{
    public class ClassWithWritablePropertiesAndFields<T>
    {
        public T Field1;

        public T Field2;

        public T Property1 { get; set; }

        public T Property2 { get; set; }
    }
}
