using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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

            Assert.AreEqual(mapped.HasValue, true);
            Assert.AreEqual(mapped.Value, "1");
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

            Assert.AreEqual(mapped.HasValue, false);
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

            Assert.AreEqual(mapped.HasValue, true);

            var list = mapped.Value.ToList();

            Assert.AreEqual(list[0], "1");
            Assert.AreEqual(list[1], "2");
            Assert.AreEqual(list[2], "3");
        }
    }
}
