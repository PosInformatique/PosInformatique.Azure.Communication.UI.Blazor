//-----------------------------------------------------------------------
// <copyright file="CallCompositeOptionsTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class CallCompositeOptionsTest
    {
        [Fact]
        public void Constructor()
        {
            var options = new CallCompositeOptions();

            options.CallControls.Should().NotBeNull();
        }

        [Fact]
        public void Serialization()
        {
            var options = new CallCompositeOptions()
            {
                CallControls =
                {
                    CameraButton = true,
                    EndCallButton = true,
                    MicrophoneButton = true,
                    DevicesButton = true,
                    MoreButton = true,
                    ParticipantsButton = true,
                    PeopleButton = true,
                    RaiseHandButton = true,
                    ScreenShareButton = true,
                },
            };

            options.Should().BeJsonSerializableInto(new
            {
                callControls = new
                {
                    cameraButton = true,
                    endCallButton = true,
                    microphoneButton = true,
                    devicesButton = true,
                    participantsButton = true,
                    screenShareButton = true,
                    moreButton = true,
                    raiseHandButton = true,
                    peopleButton = true,
                },
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
            {
                callControls = new
                {
                    cameraButton = true,
                    endCallButton = true,
                    microphoneButton = true,
                    devicesButton = true,
                    participantsButton = true,
                    screenShareButton = true,
                    moreButton = true,
                    raiseHandButton = true,
                    peopleButton = true,
                },
            };

            json.Should().BeJsonDeserializableInto(new CallCompositeOptions()
            {
                CallControls =
                {
                    CameraButton = true,
                    EndCallButton = true,
                    MicrophoneButton = true,
                    DevicesButton = true,
                    MoreButton = true,
                    ParticipantsButton = true,
                    PeopleButton = true,
                    RaiseHandButton = true,
                    ScreenShareButton = true,
                },
            });
        }
    }
}