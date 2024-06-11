//-----------------------------------------------------------------------
// <copyright file="RemoteParticipantJoinedEventTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class RemoteParticipantJoinedEventTest
    {
        [Fact]
        public void Constructor()
        {
            var participant = new RemoteParticipant(default, default);

            var @event = new RemoteParticipantJoinedEvent(participant);

            @event.Participant.Should().BeSameAs(participant);
        }
    }
}