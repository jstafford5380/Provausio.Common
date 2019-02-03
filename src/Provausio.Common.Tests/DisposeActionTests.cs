using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Xunit;

namespace Provausio.Common.Tests
{
    [ExcludeFromCodeCoverage]
    public class DisposeActionTests
    {
        [Fact]
        public void Ctor_NullAction_Throws()
        {
            // arrange
            
            // act

            // assert
            Assert.Throws<ArgumentNullException>(() => new DisposeAction(null));
        }

        [Fact]
        public void Ctor_ValidAction_Initializes()
        {
            // arrange
            Action del = () => Trace.Write("");

            // act
            var disposable = new DisposeAction(del);

            // assert
            Assert.NotNull(disposable);
        }

        [Fact]
        public void Dispose_ActionWasRun()
        {
            // arrange
            var i = 0;
            Action del = () => i++;
            var disposable = new DisposeAction(del);

            // act
            disposable.Dispose();

            // assert
            Assert.Equal(1, i);
        }
    }
}
