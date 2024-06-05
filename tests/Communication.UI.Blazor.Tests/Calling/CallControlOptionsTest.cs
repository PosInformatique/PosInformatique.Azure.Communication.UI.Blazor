//-----------------------------------------------------------------------
// <copyright file="CallControlOptionsTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class CallControlOptionsTest
    {
        [Fact]
        public void Constructor()
        {
            var options = new CallControlOptions();

            options.CameraButton.Should().BeTrue();
            options.DevicesButton.Should().BeTrue();
            options.EndCallButton.Should().BeTrue();
            options.MicrophoneButton.Should().BeTrue();
            options.MoreButton.Should().BeTrue();
            options.ParticipantsButton.Should().BeTrue();
            options.PeopleButton.Should().BeTrue();
            options.RaiseHandButton.Should().BeTrue();
            options.RaiseHandButton.Should().BeTrue();
        }

        [Fact]
        public void CameraButton_ValueChanged()
        {
            var options = new CallControlOptions();

            options.CameraButton = false;

            options.CameraButton.Should().BeFalse();
        }

        [Fact]
        public void DevicesButton_ValueChanged()
        {
            var options = new CallControlOptions();

            options.DevicesButton = false;

            options.DevicesButton.Should().BeFalse();
        }

        [Fact]
        public void EndCallButton_ValueChanged()
        {
            var options = new CallControlOptions();

            options.EndCallButton = false;

            options.EndCallButton.Should().BeFalse();
        }

        [Fact]
        public void MicrophoneButton_ValueChanged()
        {
            var options = new CallControlOptions();

            options.MicrophoneButton = false;

            options.MicrophoneButton.Should().BeFalse();
        }

        [Fact]
        public void MoreButton_ValueChanged()
        {
            var options = new CallControlOptions();

            options.MoreButton = false;

            options.MoreButton.Should().BeFalse();
        }

        [Fact]
        public void ParticipantsButton_ValueChanged()
        {
            var options = new CallControlOptions();

            options.ParticipantsButton = false;

            options.ParticipantsButton.Should().BeFalse();
        }

        [Fact]
        public void PeopleButton_ValueChanged()
        {
            var options = new CallControlOptions();

            options.PeopleButton = false;

            options.PeopleButton.Should().BeFalse();
        }

        [Fact]
        public void RaiseHandButton_ValueChanged()
        {
            var options = new CallControlOptions();

            options.RaiseHandButton = false;

            options.RaiseHandButton.Should().BeFalse();
        }

        [Fact]
        public void ScreenShareButton_ValueChanged()
        {
            var options = new CallControlOptions();

            options.ScreenShareButton = false;

            options.ScreenShareButton.Should().BeFalse();
        }

        [Fact]
        public void Serialization()
        {
            var options = new CallControlOptions()
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
            };

            options.Should().BeJsonSerializableInto(new
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
            });
        }

        [Fact]
        public void Deserialization()
        {
            var json = new
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
            };

            json.Should().BeJsonDeserializableInto(new CallControlOptions()
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
            });
        }
    }
}