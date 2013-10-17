using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Xunit;

namespace Ploeh.Albedo.UnitTests
{
    public class Scenario
    {
        [Fact]
        public void SelectParameterLessMethodUsingInstanceSyntax()
        {
            MethodInfo mi = new Methods<Version>().Select(v => v.ToString());
            Assert.Equal("ToString", mi.Name);
        }

        [Fact]
        public void SelectParameterLessMethodUsingLinqSyntax()
        {
            MethodInfo mi = from v in new Methods<Version>()
                            select v.ToString();
            Assert.Equal("ToString", mi.Name);
        }
    }
}
