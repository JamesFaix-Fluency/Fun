using System;
using System.Linq;
using Fun.Linq;
using NUnit.Framework;
using Shouldly;

namespace Fun.Tests
{
    [TestFixture]
    public class TryQueryExpressionTests
    {
        [Test]
        public void FromSelect_ShouldMapInputValue()
        {
            var result = from x in Try.Some(1)
                         select x.ToString();

            result.HasValue.ShouldBeTrue();
            result.Value.ShouldBe("1");
        }

        [Test]
        public void FromSelect_ShouldPassInputError()
        {
            var result = from x in Try.Error<int>(new Exception())
                         select x.ToString();

            result.HasValue.ShouldBeFalse();
        }

        [Test]
        public void FromFromSelect_ShouldMapInputValues()
        {
            var result = from x in Try.Some(1)
                         from y in Try.Some(2)
                         select x + y;

            result.HasValue.ShouldBeTrue();
            result.Value.ShouldBe(3);
        }

        [Test]
        public void FromFromSelect_ShouldPassFirstError()
        {
            var result = from x in Try.Error<int>(new Exception())
                         from y in Try.Some(2)
                         select x + y;

            result.HasValue.ShouldBeFalse();
        }

        [Test]
        public void FromFromSelect_ShouldPassSecondError()
        {
            var result = from x in Try.Some(1)
                         from y in Try.Error<int>(new Exception())
                         select x + y;

            result.HasValue.ShouldBeFalse();
        }
    }
}
