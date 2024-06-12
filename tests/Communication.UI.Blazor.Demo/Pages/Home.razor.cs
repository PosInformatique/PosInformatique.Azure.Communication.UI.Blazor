//-----------------------------------------------------------------------
// <copyright file="Home.razor.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Demo.Pages
{
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Components;

    public partial class Home
    {
        private readonly List<string> log;

        private ICallAdapter? callAdapter;

        private IReadOnlyList<VideoDeviceInfo> cameras;

        private IReadOnlyList<AudioDeviceInfo> microphones;

        private IReadOnlyList<AudioDeviceInfo> speakers;

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

        public Home()
        {
            this.userId = string.Empty;
            this.groupIdLocator = "76FC6C87-2D4C-49C8-B909-7E3819A88621";
            this.displayName = "John Doe";

            this.cameras = [];
            this.microphones = [];
            this.speakers = [];

            this.cameraButton = true;
            this.devicesButton = true;
            this.endCallButton = true;
            this.microphoneButton = true;
            this.moreButton = true;
            this.participantsButton = true;
            this.peopleButton = true;
            this.raiseHandButton = true;
            this.screenShareButton = true;

            this.log = [];
        }

        [Inject]
        public IConfiguration Configuration { get; set; } = default!;

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

            GC.SuppressFinalize(this);
        }

        protected override void OnInitialized()
        {
            if (!string.IsNullOrWhiteSpace(this.Configuration["DEBUG_DEFAULT_USERID"]))
            {
                this.userId = this.Configuration["DEBUG_DEFAULT_USERID"]!;
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
            };

            if (this.callAdapter is not null)
            {
                await this.callAdapter.DisposeAsync();
                this.callAdapter = null;
            }

            this.callAdapter = await this.CallingService.CreateAdapterAsync(args);

            this.callAdapter.OnCallEnded += this.OnCallEnded;
            this.callAdapter.OnMicrophoneMuteChanged += this.OnMicrophoneMuteChanged;
            this.callAdapter.OnParticipantJoined += this.OnParticipantJoined;
            this.callAdapter.OnParticipantLeft += this.OnParticipantLeft;

            this.cameras = await this.callAdapter.QueryCamerasAsync();
            this.microphones = await this.callAdapter.QueryMicrophonesAsync();
            this.speakers = await this.callAdapter.QuerySpeakersAsync();
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

        private async Task OnCallEnded(CallEndedEvent @event)
        {
            this.Log("OnCallEnded", $"Call ended (CallId: {@event.CallId})");

            await Task.CompletedTask;
        }

        private async Task OnMicrophoneMuteChanged(MicrophoneMuteChangedEvent @event)
        {
            this.Log("OnMicrophoneMuteChanged", $"Microphone mute changed. (IsMuted: {@event.IsMuted}, ParticipantId: {@event.ParticipantId.CommunicationUserId})");  ;

            await Task.CompletedTask;
        }

        private async Task OnParticipantJoined(RemoteParticipantJoinedEvent @event)
        {
            this.Log("OnParticipantJoined", $"{@event.Participant.DisplayName} has join the call. (ID: {@event.Participant.Identifier.CommunicationUserId})");

            await Task.CompletedTask;
        }

        private async Task OnParticipantLeft(RemoteParticipantLeftEvent @event)
        {
            this.Log("OnParticipantLeft", $"{@event.Participant.DisplayName} has left the call. (ID: {@event.Participant.Identifier.CommunicationUserId})");

            await Task.CompletedTask;
        }

        private void Log(string @event, string message)
        {
            this.log.Add($"{@event}: {message}");

            this.StateHasChanged();
        }
    }
}