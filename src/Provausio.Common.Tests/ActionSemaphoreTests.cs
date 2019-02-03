using System;
using System.Threading.Tasks;
using Xunit;

namespace Provausio.Common.Tests
{
    public class ActionSemaphoreTests
    {
        [Fact]
        public async Task ExecuteAsync_NoSlotsAvailable_ThrowsTimeout()
        {
            // arrange
            var semaphore = new ActionSemaphore(1, TimeSpan.FromSeconds(1));
            // let this run in the background since we're really testing the semaphore
            semaphore.ExecuteAsync(() => Task.Delay(100000), this);

            // act
            await Assert.ThrowsAsync<TimeoutException>(() => semaphore.ExecuteAsync(() => Task.Delay(100000), this));
        }

        [Fact]
        public async Task ExecuteAsync_PreviousTaskCompletes_ReleasesSemaphore()
        {
            // arrange
            var initValue = 0;
            var semaphore = new ActionSemaphore(1, TimeSpan.FromSeconds(3));
            // let this run in the background since we're really testing the semaphore
            semaphore.ExecuteAsync(() => Task.Delay(2000), this);

            // act
            await semaphore.ExecuteAsync(() => Task.Run(() => initValue++), this);

            // assert
            Assert.Equal(1, initValue);
        }
    }
}
