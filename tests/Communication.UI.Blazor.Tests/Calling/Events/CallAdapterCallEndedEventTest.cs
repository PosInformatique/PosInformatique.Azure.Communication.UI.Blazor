//-----------------------------------------------------------------------
// <copyright file="CallAdapterCallEndedEventTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class CallAdapterCallEndedEventTest
    {
        [Fact]
        public void Constructor()
        {
            var @event = new CallAdapterCallEndedEvent("The id");

            @event.CallId.Should().Be("The id");
        }

        [Fact]
        public void Serialization()
        {
            var @event = new CallAdapterCallEndedEvent("The id");

            @event.Should().BeJsonSerializableInto(new
            {
                callId = "The id",
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                callId = "The id",
            };

            json.Should().BeJsonDeserializableInto(new CallAdapterCallEndedEvent("The id"));
        }
    }
}