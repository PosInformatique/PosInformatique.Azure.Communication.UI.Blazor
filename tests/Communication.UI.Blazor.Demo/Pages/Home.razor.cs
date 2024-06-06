//-----------------------------------------------------------------------
// <copyright file="Home.razor.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Demo.Pages
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public partial class Home
    {
        private CallAdapter? callAdapter;

        private string userId;

        private string groupIdLocator;

        private string displayName;

        private bool cameraButton;
        private bool devicesButton;
        private bool endCallButton;
        private bool microphoneButton;
        private bool moreButton;
        private bool participantsButton;
        private bool peopleButton;
        private bool raiseHandButton;
        private bool screenShareButton;

        private bool leaveCallForEveryone;

        private List<string> log;

        public Home()
        {
            this.userId = string.Empty;
            this.groupIdLocator = "76FC6C87-2D4C-49C8-B909-7E3819A88621";
            this.displayName = "John Doe";

            this.cameraButton = true;
            this.devicesButton = true;
            this.endCallButton = true;
            this.microphoneButton = true;
            this.moreButton = true;
            this.participantsButton = true;
            this.peopleButton = true;
            this.raiseHandButton = true;
            this.screenShareButton = true;

            this.log = new List<string>();
        }

        public bool DisableLoad
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.userId))
                {
                    return true;
                }

                if (string.IsNullOrWhiteSpace(this.groupIdLocator))
                {
                    return true;
                }

                return false;
            }
        }

        public async ValueTask DisposeAsync()
        {
            if (this.callAdapter is not null)
            {
                await this.callAdapter.DisposeAsync();

                this.callAdapter = null;
            }
        }

        private async Task CreateUserAsync()
        {
            this.userId = await this.IdentityManager.CreateUserAsync();
        }

        private async Task LoadAsync()
        {
            var token = await this.IdentityManager.GetTokenAsync(this.userId);

            var args = new CallAdapterArgs(
                new UserIdentifier(this.userId),
                new GroupCallLocator(this.groupIdLocator),
                new TokenCredential(token))
            {
                DisplayName = this.displayName,
                Options =
                {
                    CallControls =
                    {
                        CameraButton = this.cameraButton,
                        DevicesButton = this.devicesButton,
                        EndCallButton = this.endCallButton,
                        MicrophoneButton = this.microphoneButton,
                        MoreButton = this.moreButton,
                        ParticipantsButton = this.participantsButton,
                        PeopleButton = this.peopleButton,
                        RaiseHandButton = this.raiseHandButton,
                        ScreenShareButton = this.screenShareButton,
                    },
                },
            };

            this.callAdapter = await this.CallingService.CreateAdapterAsync(args);

            this.callAdapter.OnCallEnded += this.OnCallEnded;
            this.callAdapter.OnParticipantJoined += this.OnParticipantJoined;
            this.callAdapter.OnParticipantLeft += this.OnParticipantLeft;
        }

        private async Task MuteAsync()
        {
            await this.callAdapter!.MuteAsync();
        }

        private async Task UnmuteAsync()
        {
            await this.callAdapter!.UnmuteAsync();
        }

        private async Task StartScreenShareAsync()
        {
            await this.callAdapter!.StartScreenShareAsync();
        }

        private async Task StopScreenShareAsync()
        {
            await this.callAdapter!.StopScreenShareAsync();
        }

        private async Task JoinCallAsync()
        {
            var options = new JoinCallOptions()
            {
                CameraOn = true,
                MicrophoneOn = true,
            };

            await this.callAdapter!.JoinCallAsync(options);
        }

        private async Task LeaveCallAsync()
        {
            await this.callAdapter!.LeaveCallAsync(this.leaveCallForEveryone);
        }

        private async Task OnCallEnded(CallAdapterCallEndedEvent @event)
        {
            this.Log($"Call ended (CallId: {@event.CallId})");

            await Task.CompletedTask;
        }

        private async Task OnParticipantJoined(RemoteParticipantJoinedEvent @event)
        {
            this.Log($"{@event.Participant.DisplayName} has join the call. (ID: {@event.Participant.Identifier.CommunicationUserId})");

            await Task.CompletedTask;
        }

        private async Task OnParticipantLeft(RemoteParticipantLeftEvent @event)
        {
            this.Log($"{@event.Participant.DisplayName} has left the call. (ID: {@event.Participant.Identifier.CommunicationUserId})");

            await Task.CompletedTask;
        }

        private void Log(string message)
        {
            this.log.Add(message);

            this.StateHasChanged();
        }
    }
}