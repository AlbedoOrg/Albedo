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

        [Fact]
        public void SelectPropertyUsingInstanceSyntax()
        {
            PropertyInfo pi = new Properties<Version>().Select(v => v.Major);
            Assert.Equal("Major", pi.Name);
        }

        [Fact]
        public void SelectPropertyUsingLinqSyntax()
        {
            PropertyInfo pi = from v in new Properties<Version>()
                              select v.Major;
            Assert.Equal("Major", pi.Name);
        }
    }
}
