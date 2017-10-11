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
            var t = Result.Value(1);
            var mapped = t.Map(n => n.ToString());

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
            var t = Result.Error<int>(new Exception());
            var mapped = t.Map(n => n.ToString());

            mapped.HasValue.ShouldBeFalse();

            Assert.Throws<InvalidOperationException>(() =>
            {
                var x = mapped.Value;
            });
        }

        [Test]
        public void TryMapEachShouldReturnMappedValues()
        {
            var t = Result.Value(new[] { 1, 2, 3 } as IEnumerable<int>);
            var mapped = t.MapEach(n => n.ToString());

            mapped.HasValue.ShouldBeTrue();

            var list = mapped.Value.ToList();

            list[0].ShouldBe("1");
            list[1].ShouldBe("2");
            list[2].ShouldBe("3");
        }
    }
}
