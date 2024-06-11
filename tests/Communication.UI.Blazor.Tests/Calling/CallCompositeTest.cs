//-----------------------------------------------------------------------
// <copyright file="CallCompositeTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    public class CallCompositeTest
    {
        [Fact]
        public void Constructor()
        {
            var callComposite = new CallComposite();

            callComposite.Adapter.Should().BeNull();
            callComposite.CameraButton.Should().BeFalse();
            callComposite.DevicesButton.Should().BeFalse();
            callComposite.EndCallButton.Should().BeFalse();
            callComposite.MicrophoneButton.Should().BeFalse();
            callComposite.MoreButton.Should().BeFalse();
            callComposite.ParticipantsButton.Should().BeFalse();
            callComposite.PeopleButton.Should().BeFalse();
            callComposite.RaiseHandButton.Should().BeFalse();
            callComposite.ScreenShareButton.Should().BeFalse();
        }

        [Fact]
        public void CameraButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.CameraButton = true;

            callComposite.CameraButton.Should().BeTrue();
        }

        [Fact]
        public void DevicesButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.DevicesButton = true;

            callComposite.DevicesButton.Should().BeTrue();
        }

        [Fact]
        public void EndCallButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.EndCallButton = true;

            callComposite.EndCallButton.Should().BeTrue();
        }

        [Fact]
        public void MicrophoneButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.MicrophoneButton = true;

            callComposite.MicrophoneButton.Should().BeTrue();
        }

        [Fact]
        public void MoreButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.MoreButton = true;

            callComposite.MoreButton.Should().BeTrue();
        }

        [Fact]
        public void ParticipantsButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.ParticipantsButton = true;

            callComposite.ParticipantsButton.Should().BeTrue();
        }

        [Fact]
        public void PeopleButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.PeopleButton = true;

            callComposite.PeopleButton.Should().BeTrue();
        }

        [Fact]
        public void RaiseHandButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.RaiseHandButton = true;

            callComposite.RaiseHandButton.Should().BeTrue();
        }

        [Fact]
        public void ScreenShareButton_ValueChanged()
        {
            var callComposite = new CallComposite();

            callComposite.ScreenShareButton = true;

            callComposite.ScreenShareButton.Should().BeTrue();
        }
    }
}