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
    public class TaskTryQueryExpressionTests
    {
        private static Task<result<int>> GetLongRunningTask(int n)
        {
            return Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                return Result.Value(n);
            });
        }

        private static Task<result<int>> GetErrorTask()
        {
            return Task.Run(() =>
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                return Result.Error<int>(new Exception());
            });
        }

        #region Select

        [Test]
        public async Task FromSelect_ShouldMapInputValue()
        {
            var result = await
                         from x in GetLongRunningTask(2)
                         select x.Map(n => n * 3);

            result.HasValue.ShouldBeTrue();
            result.Value.ShouldBe(6);
        }

        [Test]
        public async Task FromSelect_ShouldPassInputError()
        {
            var result = await
                         from x in GetErrorTask()
                         select x.Map(n => n * 3);

            result.HasValue.ShouldBeFalse();
        }

        #endregion
    }
}
