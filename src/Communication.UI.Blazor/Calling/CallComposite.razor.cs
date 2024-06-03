//-----------------------------------------------------------------------
// <copyright file="CallComposite.razor.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;

    /// <summary>
    /// Blazor component used wrap the CallComposite of Microsoft Azure Communication Services UI library.
    /// </summary>
    public sealed partial class CallComposite : IAsyncDisposable, IDisposable
    {
        private IJSObjectReference? module;

        private CallbackEvent? callbackEvent;

        private ElementReference callContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallComposite"/> class.
        /// </summary>
        public CallComposite()
        {
            this.callbackEvent = new CallbackEvent(this);
        }

        /// <summary>
        /// <see cref="IJSRuntime"/> used to manage Microsoft CallComposite component.
        /// </summary>
        [Inject]
        public IJSRuntime JSRuntime { get; set; } = default!;

        /// <summary>
        /// Occurs when the call is ended.
        /// </summary>
        [Parameter]
        public EventCallback<CallAdapterCallEndedEvent> OnCallEnded { get; set; }

        /// <summary>
        /// Occurs when a participant join the call.
        /// </summary>
        [Parameter]
        public EventCallback<RemoteParticipantJoinedEvent> OnParticipantJoined { get; set; }

        /// <summary>
        /// Occurs when a participant leave the call.
        /// </summary>
        [Parameter]
        public EventCallback<RemoteParticipantLeftEvent> OnParticipantLeft { get; set; }

        public async Task LoadAsync(CallAdapterArgs args)
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.EnsureModuleLoadAsync();

            await this.module!.InvokeVoidAsync("initialize", this.callContainer, args, this.callbackEvent.Reference);
        }

        public async Task JoinCallAsync(JoinCallOptions options)
        {
            ObjectDisposedException.ThrowIf(this.callbackEvent is null, this);

            await this.EnsureModuleLoadAsync();

            await this.module!.InvokeVoidAsync("adapterJoinCall", this.callContainer, options);
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            if (this.module != null)
            {
                await this.module.InvokeVoidAsync("dispose", this.callContainer);
                await this.module.DisposeAsync();

                this.module = null;
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

        private async Task EnsureModuleLoadAsync()
        {
            if (this.module is null)
            {
                this.module = await this.JSRuntime.InvokeAsync<IJSObjectReference>(
                    "import",
                    "./_content/PosInformatique.Azure.Communication.UI.Blazor/Calling/CallComposite.razor.js");
            }
        }

        private class CallbackEvent : IDisposable
        {
            private readonly CallComposite owner;

            public CallbackEvent(CallComposite owner)
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
                await this.owner.OnCallEnded.InvokeAsync(@event);
            }

            [JSInvokable]
            public async Task OnParticipantsJoinedAsync(RemoteParticipant[] joined)
            {
                foreach (var participant in joined)
                {
                    await this.owner.OnParticipantJoined.InvokeAsync(new RemoteParticipantJoinedEvent(participant));
                }
            }

            [JSInvokable]
            public async Task OnParticipantsLeftAsync(RemoteParticipant[] removed)
            {
                foreach (var participant in removed)
                {
                    await this.owner.OnParticipantLeft.InvokeAsync(new RemoteParticipantLeftEvent(participant));
                }
            }
        }
    }
}
