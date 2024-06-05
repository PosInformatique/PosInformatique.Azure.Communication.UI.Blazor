//-----------------------------------------------------------------------
// <copyright file="RemoteParticipantTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class RemoteParticipantTest
    {
        [Fact]
        public void Constructor()
        {
            var user = new CommunicationUserKind("The id");

            var remoteParticipant = new RemoteParticipant(user, "The display name");

            remoteParticipant.DisplayName.Should().Be("The display name");
            remoteParticipant.Identifier.Should().BeSameAs(user);
        }

        [Fact]
        public void Serialization()
        {
            var remoteParticipant = new RemoteParticipant(new CommunicationUserKind("The id"), "The display name");

            remoteParticipant.Should().BeJsonSerializableInto(new
            {
                identifier = new
                {
                    kind = "communicationUser",
                    communicationUserId = "The id",
                },
                displayName = "The display name",
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                identifier = new
                {
                    kind = "communicationUser",
                    communicationUserId = "The id",
                },
                displayName = "The display name",
            };

            json.Should().BeJsonDeserializableInto(new RemoteParticipant(new CommunicationUserKind("The id"), "The display name"));
        }
    }
}