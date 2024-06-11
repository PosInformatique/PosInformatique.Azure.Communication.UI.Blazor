//-----------------------------------------------------------------------
// <copyright file="MicrophoneMuteChangedEventTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class MicrophoneMuteChangedEventTest
    {
        [Fact]
        public void Constructor()
        {
            var participantId = new CommunicationUserKind(default);
            var @event = new MicrophoneMuteChangedEvent(participantId, true);

            @event.IsMuted.Should().BeTrue();
            @event.ParticipantId.Should().BeSameAs(participantId);
        }
    }
}