using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Shouldly;

namespace Fun.Tests
{
    [TestFixture]
    public class TryTests
    {
        [Test]
        public void TryMapShouldReturnMappedValue()
        {
            var t = Try.Some(1);
            var mapped = t.TryMap(n => n.ToString());

            mapped.HasValue.ShouldBeTrue();
            mapped.Value.ShouldBe("1");

            Assert.Throws<InvalidOperationException>(() =>
            {
                var x = mapped.Error;
            });
        }

        [Test]
        public void TryMapShouldReturnInputError()
        {
            var t = Try.Error<int>(new Exception());
            var mapped = t.TryMap(n => n.ToString());

            mapped.HasValue.ShouldBeFalse();

            Assert.Throws<InvalidOperationException>(() =>
            {
                var x = mapped.Value;
            });
        }

        [Test]
        public void TryMapEachShouldReturnMappedValues()
        {
            var t = Try.Some(new[] { 1, 2, 3 } as IEnumerable<int>);
            var mapped = t.TryMapEach(n => n.ToString());

            mapped.HasValue.ShouldBeTrue();

            var list = mapped.Value.ToList();

            list[0].ShouldBe("1");
            list[1].ShouldBe("2");
            list[2].ShouldBe("3");
        }
    }
}
