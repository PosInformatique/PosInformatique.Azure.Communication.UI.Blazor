//-----------------------------------------------------------------------
// <copyright file="CallAdapterArgs.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Arguments for creating the Azure Communication Services implementation of CallAdapter.
    /// </summary>
    public class CallAdapterArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CallAdapterArgs"/> class.
        /// </summary>
        /// <param name="userId">Identifier of caller user.</param>
        /// <param name="locator">Locator of the call.</param>
        /// <param name="credential">Credentials of the call.</param>
        public CallAdapterArgs(UserIdentifier userId, GroupCallLocator locator, TokenCredential credential)
        {
            this.UserId = userId;
            this.DisplayName = "Anonymous";
            this.Locator = locator;
            this.Credential = credential;
            this.Options = new CallCompositeOptions();
        }

        /// <summary>
        /// Gets the user id of the caller.
        /// </summary>
        [JsonPropertyName("userId")]
        [JsonPropertyOrder(1)]
        public UserIdentifier UserId { get; }

        /// <summary>
        /// Gets or sets the display name of the caller.
        /// </summary>
        [JsonPropertyName("displayName")]
        [JsonPropertyOrder(2)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Gets the credential to connect on Azure Communication Services.
        /// </summary>
        [JsonPropertyName("credential")]
        [JsonPropertyOrder(3)]
        public TokenCredential Credential { get; }

        /// <summary>
        /// Gets the locator to call.
        /// </summary>
        [JsonPropertyName("locator")]
        [JsonPropertyOrder(4)]
        public GroupCallLocator Locator { get; }

        /// <summary>
        /// Gets the options of the <see cref="CallComposite"/>.
        /// </summary>
        [JsonPropertyName("options")]
        [JsonPropertyOrder(5)]
        public CallCompositeOptions Options { get; }
    }
}
