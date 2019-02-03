using System;
using System.Threading.Tasks;
using Provausio.Common.Ext;
using Xunit;

namespace Provausio.Common.Tests.Ext
{
    public class TaskExtTests
    {
        [Fact]
        public async Task WaitWhile_TimeoutOccurs_Throws()
        {
            // arrange

            // act
            
            // assert
            await Assert.ThrowsAsync<TimeoutException>(() => TaskExt.WaitWhile(() => true, 25, 25));
        }

        [Fact]
        public async Task WaitWhile_ConditionChanges_TaskReturns()
        {
            // arrange
            var value = "";
            var updateTask = new Task(async () =>
            {
                await Task.Delay(1000);
                value = "foo";
            });

            // act
            updateTask.Start();
            await TaskExt.WaitWhile(() => string.IsNullOrEmpty(value), timeout: 1500);

            // assert
            Assert.False(string.IsNullOrEmpty(value));
        }

        [Fact]
        public async Task WaitUntil_TimeoutOccurs_Throws()
        {
            // arrange

            // act

            // assert
            await Assert.ThrowsAsync<TimeoutException>(() => TaskExt.WaitUntil(() => false, 25, 25));
        }

        [Fact]
        public async Task WaitUntil_ConditionChanges_TaskReturns()
        {
            // arrange
            var value = "";
            var updateTask = new Task(async () =>
            {
                await Task.Delay(1000);
                value = "foo";
            });

            // act
            updateTask.Start();
            await TaskExt.WaitUntil(() => value.Equals("foo"), timeout: 1500);

            // assert
            Assert.False(string.IsNullOrEmpty(value));
        }
    }
}
