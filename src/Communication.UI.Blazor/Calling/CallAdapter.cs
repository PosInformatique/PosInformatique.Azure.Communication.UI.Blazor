﻿//-----------------------------------------------------------------------
// <copyright file="CallAdapter.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    /// <summary>
    /// An adapter interface specific for Azure Communication identity which extends <see cref="CommonCallAdapter"/>.
    /// </summary>
    public class CallAdapter : CommonCallAdapter, IDisposable, IAsyncDisposable
    {
        private readonly Guid referenceId;

        private readonly IJSObjectReference module;

        private CallbackEvent? callbackEvent;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallAdapter"/> class.
        /// </summary>
        internal CallAdapter(IJSObjectReference module)
        {
            this.module = module;
            this.referenceId = Guid.NewGuid();

            this.callbackEvent = new CallbackEvent(this);
        }

        /// <summary>
        /// Occurs when the call is ended.
        /// </summary>
        public event AsyncEventHandler<CallAdapterCallEndedEvent>? OnCallEnded;

        /// <summary>
        /// Occurs when a participant join the call.
        /// </summary>
        public event AsyncEventHandler<RemoteParticipantJoinedEvent>? OnParticipantJoined;

        /// <summary>
        /// Occurs when a participant leave the call.
        /// </summary>
        public event AsyncEventHandler<RemoteParticipantLeftEvent>? OnParticipantLeft;

        /// <summary>
        /// Join an existing call.
        /// </summary>
        /// <param name="options">Options of the call.</param>
        /// <returns>A <see cref="Task"/> of the asynchronous operation.</returns>
        /// <exception cref="InvalidOperationException">If the component has not been loaded.</exception>
        public async Task JoinCallAsync(JoinCallOptions options)
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.module.InvokeVoidAsync("adapterJoinCall", this.referenceId, options);
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            if (this.callbackEvent != null)
            {
                await this.module.InvokeVoidAsync("dispose", this.referenceId);
                await this.module.DisposeAsync();
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
            await this.module.InvokeVoidAsync("createCallAdapter", this.referenceId, args, this.callbackEvent!.Reference);
        }

        internal async Task InitializeControlAsync(ElementReference callContainer)
        {
            await this.module.InvokeVoidAsync("initializeControl", this.referenceId, callContainer);
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
            public async Task OnCallEndedAsync(CallAdapterCallEndedEvent @event)
            {
                if (this.owner.OnCallEnded is not null)
                {
                    await this.owner.OnCallEnded(@event);
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