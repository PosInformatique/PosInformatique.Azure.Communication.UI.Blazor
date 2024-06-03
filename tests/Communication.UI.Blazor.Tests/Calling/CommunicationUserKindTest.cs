//-----------------------------------------------------------------------
// <copyright file="CommunicationUserKindTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class CommunicationUserKindTest
    {
        [Fact]
        public void Constructor()
        {
            var user = new CommunicationUserKind("The id");

            user.CommunicationUserId.Should().Be("The id");
        }

        [Fact]
        public void Serialization()
        {
            var user = new CommunicationUserKind("The id");

            user.Should().BeJsonSerializableInto(new
            {
                kind = "communicationUser",
                communicationUserId = "The id",
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                kind = "communicationUser",
                communicationUserId = "The id",
            };

            json.Should().BeJsonDeserializableInto(new CommunicationUserKind("The id"));
        }
    }
}