//-----------------------------------------------------------------------
// <copyright file="CallAdapterArgsTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class CallAdapterArgsTest
    {
        [Fact]
        public void Constructor()
        {
            var userId = new UserIdentifier(default);
            var locator = new GroupCallLocator(default);
            var credential = new TokenCredential(default);

            var args = new CallAdapterArgs(userId, locator, credential);

            args.Credential.Should().BeSameAs(credential);
            args.DisplayName.Should().Be("Anonymous");
            args.Locator.Should().BeSameAs(locator);
            args.UserId.Should().BeSameAs(userId);
        }

        [Fact]
        public void DisplayName_ValueChanged()
        {
            var args = new CallAdapterArgs(default, default, default);

            args.DisplayName = "The display name";

            args.DisplayName.Should().Be("The display name");
        }

        [Fact]
        public void Serialization()
        {
            var args = new CallAdapterArgs(
                new UserIdentifier("The user id"),
                new GroupCallLocator("The group id"),
                new TokenCredential("The token id"))
            {
                DisplayName = "The display name",
            };

            args.Should().BeJsonSerializableInto(new
            {
                userId = new
                {
                    communicationUserId = "The user id",
                },
                displayName = "The display name",
                credential = new
                {
                    token = "The token id",
                },
                locator = new
                {
                    groupId = "The group id",
                },
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                userId = new
                {
                    communicationUserId = "The user id",
                },
                displayName = "The display name",
                credential = new
                {
                    token = "The token id",
                },
                locator = new
                {
                    groupId = "The group id",
                },
            };

            json.Should().BeJsonDeserializableInto(new CallAdapterArgs(
                new UserIdentifier("The user id"),
                new GroupCallLocator("The group id"),
                new TokenCredential("The token id"))
            {
                DisplayName = "The display name",
            });
        }
    }
}