//-----------------------------------------------------------------------
// <copyright file="UserIdentifierTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class UserIdentifierTest
    {
        [Fact]
        public void Constructor()
        {
            var identifier = new UserIdentifier("The id");

            identifier.CommunicationUserId.Should().Be("The id");
        }

        [Fact]
        public void Serialization()
        {
            var identifier = new UserIdentifier("The id");

            identifier.Should().BeJsonSerializableInto(new
            {
                communicationUserId = "The id",
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                communicationUserId = "The id",
            };

            json.Should().BeJsonDeserializableInto(new UserIdentifier("The id"));
        }
    }
}