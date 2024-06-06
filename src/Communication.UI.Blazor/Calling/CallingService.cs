//-----------------------------------------------------------------------
// <copyright file="CallingService.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using Microsoft.JSInterop;

    /// <summary>
    /// Implementation of the <see cref="ICallingService"/> used to create instances
    /// of the <see cref="CallComposite"/>.
    /// </summary>
    public sealed class CallingService : ICallingService, IAsyncDisposable
    {
        private readonly IJSRuntime jsRuntime;

        private IJSObjectReference? module;

        /// <summary>
        /// Initializes a new instance of the <see cref="CallingService"/> class.
        /// </summary>
        /// <param name="jsRuntime"><see cref="IJSRuntime"/> used to interop with JavaScript.</param>
        public CallingService(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        /// <inheritdoc />
        public async ValueTask DisposeAsync()
        {
            if (this.module != null)
            {
                await this.module.DisposeAsync();

                this.module = null;
            }
        }

        /// <inheritdoc />
        public async Task<ICallAdapter> CreateAdapterAsync(CallAdapterArgs args)
        {
            await this.EnsureModuleLoadAsync();

            var adapter = new CallAdapter(this.module!);

            await adapter.InitializeAsync(args);

            return adapter;
        }

        private async Task EnsureModuleLoadAsync()
        {
            if (this.module is null)
            {
                this.module = await this.jsRuntime.InvokeAsync<IJSObjectReference>(
                    "import",
                    "./_content/PosInformatique.Azure.Communication.UI.Blazor/Calling/CallComposite.razor.js");
            }
        }
    }
}
