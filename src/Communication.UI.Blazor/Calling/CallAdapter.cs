﻿//-----------------------------------------------------------------------
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
    public class CallAdapter : CommonCallAdapter, IDisposable, IAsyncDisposable
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

        /// <summary>
        /// Occurs when the call is ended.
        /// </summary>
        public event AsyncEventHandler<CallEndedEvent>? OnCallEnded;

        /// <summary>
        /// Occurs when the microphone is muted/unmuted on a participant.
        /// </summary>
        public event AsyncEventHandler<MicrophoneMuteChangedEvent>? OnMicrophoneMuteChanged;

        /// <summary>
        /// Occurs when a participant join the call.
        /// </summary>
        public event AsyncEventHandler<RemoteParticipantJoinedEvent>? OnParticipantJoined;

        /// <summary>
        /// Occurs when a participant leave the call.
        /// </summary>
        public event AsyncEventHandler<RemoteParticipantLeftEvent>? OnParticipantLeft;

        internal Guid Id { get; }

        internal IJSObjectReference Module { get; }

        /// <summary>
        /// Join an existing call.
        /// </summary>
        /// <param name="options">Options of the call.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="CallAdapter"/> has already been disposed.</exception>
        public async Task JoinCallAsync(JoinCallOptions options)
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterJoinCall", this.Id, options);
        }

        /// <summary>
        /// Leave the call.
        /// </summary>
        /// <param name="forEveryone">Whether to remove all participants when leaving.</param>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="CallAdapter"/> has already been disposed.</exception>
        public async Task LeaveCallAsync(bool forEveryone)
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterLeaveCall", this.Id, forEveryone);
        }

        /// <summary>
        /// Mute the current user during the call or disable microphone locally.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="CallAdapter"/> has already been disposed.</exception>
        public async Task MuteAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterMute", this.Id);
        }

        /// <summary>
        /// Unmute the current user during the call or enable microphone locally.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="CallAdapter"/> has already been disposed.</exception>
        public async Task UnmuteAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterUnmute", this.Id);
        }

        /// <summary>
        /// Start sharing the screen during a call.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="CallAdapter"/> has already been disposed.</exception>
        public async Task StartScreenShareAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterStartScreenShare", this.Id);
        }

        /// <summary>
        /// Stop sharing the screen.
        /// </summary>
        /// <returns>A <see cref="Task"/> that represents the asynchronous invocation.</returns>
        /// <exception cref="ObjectDisposedException">If the <see cref="CallAdapter"/> has already been disposed.</exception>
        public async Task StopScreenShareAsync()
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.Module.InvokeVoidAsync("adapterStopScreenShare", this.Id);
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
