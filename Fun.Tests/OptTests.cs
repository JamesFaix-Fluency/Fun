using NUnit.Framework;
using System;

namespace Fun.Tests
{
    [TestFixture]
    public class OptTests
    {
        [Test]
        public void OptMapShouldReturnMappedValue()
        {
            var t = Opt.Some(1);
            var mapped = t.OptMap(n => n.ToString());

            Assert.AreEqual(mapped.HasValue, true);
            Assert.AreEqual(mapped.Value, "1");
        }


        [Test]
        public void OptMapShouldReturnInputNone()
        {
            var t = Opt.None<int>();
            var mapped = t.OptMap(n => n.ToString());

            Assert.AreEqual(mapped.HasValue, false);
            Assert.Throws<InvalidOperationException>(() =>
            {
                var x = mapped.Value;
            });
        }
    }
}
