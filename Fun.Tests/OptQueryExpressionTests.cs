using System.Linq;
using Fun.Linq;
using NUnit.Framework;
using Shouldly;

namespace Fun.Tests
{
    [TestFixture]
    public class OptQueryExpressionTests
    {
        [Test]
        public void FromSelect_ShouldMapInputValue()
        {
            var result = from x in Opt.Some(1)
                         select x.ToString();

            result.HasValue.ShouldBeTrue();
            result.Value.ShouldBe("1");
        }

        [Test]
        public void FromSelect_ShouldPassInputNone()
        {
            var result = from x in Opt.None<int>()
                         select x.ToString();

            result.HasValue.ShouldBeFalse();
        }

        [Test]
        public void FromFromSelect_ShouldMapInputValues()
        {
            var result = from x in Opt.Some(1)
                         from y in Opt.Some(2)
                         select x + y;

            result.HasValue.ShouldBeTrue();
            result.Value.ShouldBe(3);
        }

        [Test]
        public void FromFromSelect_ShouldPassFirstNone()
        {
            var result = from x in Opt.None<int>()
                         from y in Opt.Some(2)
                         select x + y;

            result.HasValue.ShouldBeFalse();
        }

        [Test]
        public void FromFromSelect_ShouldPassSecondNone()
        {
            var result = from x in Opt.Some(1)
                         from y in Opt.None<int>()
                         select x + y;

            result.HasValue.ShouldBeFalse();
        }
    }
}
