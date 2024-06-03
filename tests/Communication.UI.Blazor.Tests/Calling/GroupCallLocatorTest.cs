//-----------------------------------------------------------------------
// <copyright file="GroupCallLocatorTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class GroupCallLocatorTest
    {
        [Fact]
        public void Constructor()
        {
            var user = new GroupCallLocator("The id");

            user.GroupId.Should().Be("The id");
        }

        [Fact]
        public void Serialization()
        {
            var user = new GroupCallLocator("The id");

            user.Should().BeJsonSerializableInto(new
            {
                kind = "groupCall",
                groupId = "The id",
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                kind = "groupCall",
                groupId = "The id",
            };

            json.Should().BeJsonDeserializableInto(new GroupCallLocator("The id"));
        }
    }
}