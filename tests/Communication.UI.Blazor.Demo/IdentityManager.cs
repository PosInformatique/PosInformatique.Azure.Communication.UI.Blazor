//-----------------------------------------------------------------------
// <copyright file="IdentityManager.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Demo
{
    using global::Azure.Communication.Identity;
    using Microsoft.Extensions.Options;

    public class IdentityManager
    {
        private CommunicationIdentityClient client;

        public IdentityManager(IOptions<IdentityManagerOptions> options)
        {
            this.client = new CommunicationIdentityClient(options.Value.ConnectionString);
        }

        public async Task<string> CreateUserAsync()
        {
            var response = await this.client.CreateUserAsync();

            return response.Value.Id;
        }

        public async Task<string> GetTokenAsync(string userId)
        {
            var response = await this.client.GetTokenAsync(new global::Azure.Communication.CommunicationUserIdentifier(userId), [CommunicationTokenScope.VoIP]);

            return response.Value.Token;
        }
    }
}
