using System;
using Xunit;

namespace Provausio.Common.Tests
{
    public class ObjectPropertyCollectionTests
    {
        [Fact]
        public void Ctor_Count_ParsesExpectedFields()
        {
            // arrange
            var target = new TestClass();

            // act
            var coll = new ObjectPropertyCollection(target);

            // assert
            Assert.Equal(2, coll.Count);
        }

        [Fact]
        public void Ctor_ChildClass_ParsesInheritedFields()
        {
            // arrange
            var target = new TestClassChild();

            // act
            var coll = new ObjectPropertyCollection(target);

            // assert
            Assert.Equal(3, coll.Count);
        }

        [Fact]
        public void ToString_EmitsExpectedString()
        {
            // arrange
            var target = new TestClass {Prop1 = "foo", Prop2 = 22};
            var coll = new ObjectPropertyCollection(target);

            // act
            var asString = coll.ToString();

            // assert
            Assert.Equal("Prop1=foo&Prop2=22", asString);
        }

        [Fact]
        public void FromKvpString_ParsesExpectedProperties()
        {
            // arrange
            var target = new TestClass {Prop1 = "foo", Prop2 = 22};
            var coll1 = new ObjectPropertyCollection(target);
            var asString = coll1.ToString();

            // act
            var coll = ObjectPropertyCollection.FromKvpString(asString);

            // assert
            Assert.Equal(2, coll.Count);
            Assert.Equal("foo", coll["Prop1"]);
            Assert.Equal("22", coll["Prop2"]);
        }

        [Fact]
        public void Ctor_DateTimeOffset_IsEpoch()
        {
            // arrange
            var dt = DateTimeOffset.UtcNow;
            var expectedValue = dt.ToUnixTimeMilliseconds().ToString();
            var expectedString = $"Prop1={expectedValue}";
            var target = new TestClass2 {Prop1 = dt};
            var properties = new ObjectPropertyCollection(target);

            // act
            var asString = properties.ToString();

            // assert
            Assert.Equal(expectedString, asString);
        }

        [Fact]
        public void Ctor_WithTransform_Transforms()
        {
            // arrange
            var target = new TestClass {Prop1 = "foo"};
            var transform = new PropertyTransform("Prop1", s => $"{s}bar");
            var properties = new ObjectPropertyCollection(target, transform);

            // act
            var asString = properties.ToString();

            // assert
            Assert.Equal("Prop1=foobar&Prop2=0", asString);
        }

        [Fact]
        public void DO()
        {
            var input =
                "orderBy=createdDateTimeDesc&minDateTime=1900-01-01T01%3A01%3A01Z&pageSize=500&customerIdentifier=adda375b-9d60-a701-9e32-a24a81c13713&cursor=__cursor__%3AL2ludGVybmFsQXBpL3YyL3Jldmlldy9sb29rdXAvP21pbkRhdGVUaW1lPTE5MDAtMDEtMDFUMDElM0EwMSUzQTAxWiZyZXR1cm5WT2JqZWN0RmxhZz1GYWxzZSZwYWdlU2l6ZT01MDAmb3JkZXJCeUNyZWF0ZWREYXRlRGVzY2VuZGluZ0ZsYWc9VHJ1ZSZhZ2lkPUFHLVg5SzI5OUc4JmN1cnNvcj1DcHNCQ2c4S0FtTjBFZ2tJbnVhSmpaMmp6QUlTZ3dGcURuTi1jbVZ3WTI5eVpTMXdjbTlrY25FTEVnTlFVazhpRG1Gbk9rRkhMVmc1U3pJNU9VYzREQXNTQTB4SlV5SWtURWxUTFRNNE5qWXhNekE1T1RkRVJUUkNRek5CT1RKQlJrWXpSVU13UXpFMVJUSkNEQXNTQTFKV1Z5SWtVbFpYTFRsRU5ESTNSVVV4TkRFMk16UTBRVFJDTkRVMU5rVXlRalF6UVVZeE9UWTNEQmdBSUFFJTNEJm1heERhdGVUaW1lPTIwMTctMDQtMTJUMTIlM0EwMCUzQTAwWg%3D%3D&maxDateTime=2017-04-12T12%3A00%3A00Z";
            var o = ObjectPropertyCollection.FromKvpString(input);
        }

        private class TestClass
        {
            public string Prop1 { get; set; }

            public int Prop2 { get; set; }
        }

        private class TestClassChild : TestClass
        {
            public int Prop3 { get; set; }
        }

        private class TestClass2
        {
            public DateTimeOffset Prop1 { get; set; }
        }
    }
}
