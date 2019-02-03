using System;
using System.Linq;
using System.Reflection;
using Provausio.Common.Ext;
using Xunit;

namespace Provausio.Common.Tests.Ext
{
    public class PropertyInfoTests
    {
        [Fact]
        public void CanBeNull_ExpectedResults()
        {
            // arrange
            var expectation = new[] {true, true, false, true, true};
            var properties = typeof(Test).GetTypeInfo().DeclaredProperties.ToArray();

            // act

            // assert
            for (var i = 0; i < properties.Length; i++)
            {
                Assert.Equal(expectation[i], properties[i].CanBeNull());
            }
        }

        private class Test
        {
            public int? Prop1 { get; set; }
            public string Prop2 { get; set; }
            public int Prop3 { get; set; }
            public Nullable<int> Prop4 { get; set; }
            public TestChild Prop5 { get; set; }
        }

        private class TestChild
        {
            public int MyProperty { get; set; }
        }
    }
}
