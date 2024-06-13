//-----------------------------------------------------------------------
// <copyright file="StateChangedEventTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class StateChangedEventTest
    {
        [Fact]
        public void Constructor()
        {
            var state = new CallAdapterState(default);

            var @event = new StateChangedEvent(state);

            @event.State.Should().BeSameAs(state);
        }
    }
}