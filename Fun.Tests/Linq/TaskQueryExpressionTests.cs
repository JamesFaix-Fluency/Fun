using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Fun.Linq;
using NUnit.Framework;
using Shouldly;

namespace Fun.Tests.Linq
{
    [TestFixture]
    public class TaskQueryExpressionTests
    {
        private static Task<int> GetLongRunningTask(int n)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                return n;
            });
        }

        #region Select

        [Test]
        public async Task FromSelect_ShouldMapResult()
        {
            var result = await
                         from x in GetLongRunningTask(1)
                         select x.ToString();

            result.ShouldBe("1");
        }

        [Test]
        public void FromSelect_ShouldNotBlock()
        {
            Assert.Fail();
        }

        #endregion

        #region SelectMany

        [Test]
        public async Task FromFromSelect_ShouldMapResults()
        {
            var result = await
                         from x in GetLongRunningTask(1)
                         from y in GetLongRunningTask(2)
                         select x + y;

            result.ShouldBe(3);
        }

        #endregion
    }
}
