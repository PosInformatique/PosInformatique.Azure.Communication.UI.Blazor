//-----------------------------------------------------------------------
// <copyright file="CallAdapterStateTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class CallAdapterStateTest
    {
        [Fact]
        public void Constructor()
        {
            var userId = new CommunicationUserKind(default);

            var state = new CallAdapterState(userId);

            state.CameraStatus.Should().BeNull();
            state.DisplayName.Should().BeNull();
            state.IsLocalPreviewMicrophoneEnabled.Should().BeFalse();
            state.IsRoomsCall.Should().BeFalse();
            state.IsTeamsCall.Should().BeFalse();
            state.Page.Should().Be(CallCompositePage.AccessDeniedTeamsMeeting);
            state.UserId.Should().BeSameAs(userId);
        }

        [Fact]
        public void Serialization()
        {
            var state = new CallAdapterState(new CommunicationUserKind("The user id"))
            {
                CameraStatus = CameraStatus.On,
                DisplayName = "The display name",
                Page = CallCompositePage.JoinCallFailedDueToNoNetwork,
                IsLocalPreviewMicrophoneEnabled = true,
                IsRoomsCall = true,
                IsTeamsCall = true,
            };

            state.Should().BeJsonSerializableInto(new
            {
                userId = new
                {
                    communicationUserId = "The user id",
                },
                displayName = "The display name",
                isTeamsCall = true,
                isRoomsCall = true,
                cameraStatus = "On",
                isLocalPreviewMicrophoneEnabled = true,
                page = "joinCallFailedDueToNoNetwork",
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
                isTeamsCall = true,
                isRoomsCall = true,
                cameraStatus = "On",
                isLocalPreviewMicrophoneEnabled = true,
                page = "joinCallFailedDueToNoNetwork",
            };

            json.Should().BeJsonDeserializableInto(new CallAdapterState(new CommunicationUserKind("The user id"))
            {
                CameraStatus = CameraStatus.On,
                DisplayName = "The display name",
                Page = CallCompositePage.JoinCallFailedDueToNoNetwork,
                IsLocalPreviewMicrophoneEnabled = true,
                IsRoomsCall = true,
                IsTeamsCall = true,
            });
        }
    }
}