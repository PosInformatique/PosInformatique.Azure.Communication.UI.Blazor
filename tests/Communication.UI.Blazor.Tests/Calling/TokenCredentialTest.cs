//-----------------------------------------------------------------------
// <copyright file="TokenCredentialTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class TokenCredentialTest
    {
        [Fact]
        public void Constructor()
        {
            var tokenCredentials = new TokenCredential("The token");

            tokenCredentials.Token.Should().Be("The token");
        }

        [Fact]
        public void Serialization()
        {
            var tokenCredentials = new TokenCredential("The token");

            tokenCredentials.Should().BeJsonSerializableInto(new
            {
                token = "The token",
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                token = "The token",
            };

            json.Should().BeJsonDeserializableInto(new TokenCredential("The token"));
        }
    }
}