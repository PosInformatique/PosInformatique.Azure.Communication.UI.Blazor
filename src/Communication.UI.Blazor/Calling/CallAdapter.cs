//-----------------------------------------------------------------------
// <copyright file="CallAdapter.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using Microsoft.JSInterop;

    /// <summary>
    /// An adapter interface specific for Azure Communication identity which extends <see cref="CommonCallAdapter"/>.
    /// </summary>
    public class CallAdapter : CommonCallAdapter, ICallAdapter, IDisposable
    {
        private CallbackEvent? callbackEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallAdapter"/> class.
        /// </summary>
        internal CallAdapter(IJSObjectReference module)
        {
            this.Module = module;
            this.Id = Guid.NewGuid();

            this.callbackEvent = new CallbackEvent(this);
        }

        /// <inheritdoc />
        public event AsyncEventHandler<CallEndedEvent>? OnCallEnded;

        /// <inheritdoc />
        public event AsyncEventHandler<MicrophoneMuteChangedEvent>? OnMicrophoneMuteChanged;

        /// <inheritdoc />
        public event AsyncEventHandler<RemoteParticipantJoinedEvent>? OnParticipantJoined;

        /// <inheritdoc />
        public event AsyncEventHandler<RemoteParticipantLeftEvent>? OnParticipantLeft;

        internal Guid Id { get; }

        internal IJSObjectReference Module { get; }

        /// <inheritdoc />
        public async Task JoinCallAsync(JoinCallOptions options)
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterJoinCall", this.Id, options);
        }

        /// <inheritdoc />
        public async Task LeaveCallAsync(bool forEveryone)
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterLeaveCall", this.Id, forEveryone);
        }

        /// <inheritdoc />
        public async Task LowerHandAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterLowerHand", this.Id);
        }

        /// <inheritdoc />
        public async Task MuteAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterMute", this.Id);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<VideoDeviceInfo>> QueryCamerasAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            return await this.Module.InvokeAsync<IReadOnlyList<VideoDeviceInfo>>("adapterQueryCameras", this.Id);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<AudioDeviceInfo>> QueryMicrophonesAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            return await this.Module.InvokeAsync<IReadOnlyList<AudioDeviceInfo>>("adapterQueryMicrophones", this.Id);
        }

        /// <inheritdoc />
        public async Task<IReadOnlyList<AudioDeviceInfo>> QuerySpeakersAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            return await this.Module.InvokeAsync<IReadOnlyList<AudioDeviceInfo>>("adapterQuerySpeakers", this.Id);
        }

        /// <inheritdoc />
        public async Task RaiseHandAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterRaiseHand", this.Id);
        }

        /// <inheritdoc />
        public async Task StartScreenShareAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterStartScreenShare", this.Id);
        }

        /// <inheritdoc />
        public async Task StopScreenShareAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterStopScreenShare", this.Id);
        }

        /// <inheritdoc />
        public async Task UnmuteAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterUnmute", this.Id);
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            if (this.callbackEvent != null)
            {
                await this.Module.InvokeVoidAsync("dispose", this.Id);
            }

            this.Dispose();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            if (this.callbackEvent != null)
            {
                this.callbackEvent.Dispose();
                this.callbackEvent = null;
            }
        }

        internal async Task InitializeAsync(CallAdapterArgs args)
        {
            await this.Module.InvokeVoidAsync("createCallAdapter", this.Id, args, this.callbackEvent!.Reference);
        }

        private class CallbackEvent : IDisposable
        {
            private readonly CallAdapter owner;

            public CallbackEvent(CallAdapter owner)
            {
                this.owner = owner;
                this.Reference = DotNetObjectReference.Create(this);
            }

            public DotNetObjectReference<CallbackEvent>? Reference { get; private set; }

            public void Dispose()
            {
                if (this.Reference != null)
                {
                    this.Reference.Dispose();
                    this.Reference = null;
                }
            }

            [JSInvokable]
            public async Task OnCallEndedAsync(CallEndedEvent @event)
            {
                if (this.owner.OnCallEnded is not null)
                {
                    await this.owner.OnCallEnded(@event);
                }
            }

            [JSInvokable]
            public async Task OnMicrophoneMuteChangedAsync(MicrophoneMuteChangedEvent @event)
            {
                if (this.owner.OnMicrophoneMuteChanged is not null)
                {
                    await this.owner.OnMicrophoneMuteChanged(@event);
                }
            }

            [JSInvokable]
            public async Task OnParticipantsJoinedAsync(RemoteParticipant[] joined)
            {
                if (this.owner.OnParticipantJoined is not null)
                {
                    foreach (var participant in joined)
                    {
                        await this.owner.OnParticipantJoined(new RemoteParticipantJoinedEvent(participant));
                    }
                }
            }

            [JSInvokable]
            public async Task OnParticipantsLeftAsync(RemoteParticipant[] removed)
            {
                if (this.owner.OnParticipantLeft is not null)
                {
                    foreach (var participant in removed)
                    {
                        await this.owner.OnParticipantLeft(new RemoteParticipantLeftEvent(participant));
                    }
                }
            }
        }
    }
}
