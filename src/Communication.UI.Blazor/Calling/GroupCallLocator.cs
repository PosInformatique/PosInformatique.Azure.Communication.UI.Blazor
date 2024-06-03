//-----------------------------------------------------------------------
// <copyright file="GroupCallLocator.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json.Serialization;

    /// <summary>
    /// Locator used for joining a group call.
    /// </summary>
    public class GroupCallLocator : CallAdapterLocator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GroupCallLocator"/> class.
        /// </summary>
        /// <param name="groupId">The identifier of the group to join.</param>
        public GroupCallLocator(string groupId)
        {
            this.GroupId = groupId;
        }

        /// <summary>
        /// Gets the identifier of the group to join.
        /// </summary>
        [JsonPropertyName("groupId")]
        [JsonPropertyOrder(1)]
        public string GroupId { get; }
    }
}
