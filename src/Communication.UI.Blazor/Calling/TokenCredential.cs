//-----------------------------------------------------------------------
// <copyright file="TokenCredential.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// The Azure Communication Services token credential.
    /// </summary>
    public class TokenCredential
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TokenCredential"/> class.
        /// </summary>
        /// <param name="token">The user token used to connect to the Azure Communication Service.</param>
        public TokenCredential(string token)
        {
            this.Token = token;
        }

        /// <summary>
        /// Gets the user token used to connect to the Azure Communication Service.
        /// </summary>
        [JsonPropertyName("token")]
        [JsonPropertyOrder(1)]
        public string Token { get; }
    }
}
