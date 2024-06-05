//-----------------------------------------------------------------------
// <copyright file="CallParticipantsLocatorTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    using System.Collections.ObjectModel;

    public class CallParticipantsLocatorTest
    {
        [Fact]
        public void Constructor()
        {
            var locator = new CallParticipantsLocator();

            locator.ParticipantIds.Should().BeEmpty();
        }

        [Fact]
        public void ParticipantIds_ValueChanged()
        {
            var locator = new CallParticipantsLocator();

            var participantIds = new Collection<string>();

            locator.ParticipantIds = participantIds;

            locator.ParticipantIds.Should().BeSameAs(participantIds);
        }

        [Fact]
        public void Serialization()
        {
            var locator = new CallParticipantsLocator()
            {
                ParticipantIds = new Collection<string>()
                {
                    "Id 1",
                    "Id 2",
                },
            };

            locator.Should().BeJsonSerializableInto(new
            {
                kind = "callParticipants",
                participantIds = new[]
                {
                    "Id 1",
                    "Id 2",
                },
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                kind = "callParticipants",
                participantIds = new[]
                {
                    "Id 1",
                    "Id 2",
                },
            };

            json.Should().BeJsonDeserializableInto(new CallParticipantsLocator()
            {
                ParticipantIds = new Collection<string>()
                {
                    "Id 1",
                    "Id 2",
                },
            });
        }
    }
}