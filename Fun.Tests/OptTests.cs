using System;
using NUnit.Framework;
using Shouldly;

namespace Fun.Tests
{
    [TestFixture]
    public class OptTests
    {
        [Test]
        public void OptMapShouldReturnMappedValue()
        {
            var t = Opt.Some(1);
            var mapped = t.Map(n => n.ToString());

            mapped.HasValue.ShouldBeTrue();
            mapped.Value.ShouldBe("1");
        }


        [Test]
        public void OptMapShouldReturnInputNone()
        {
            var t = Opt.None<int>();
            var mapped = t.Map(n => n.ToString());

            mapped.HasValue.ShouldBeFalse();

            Assert.Throws<InvalidOperationException>(() =>
            {
                var x = mapped.Value;
            });
        }
    }
}
