//-----------------------------------------------------------------------
// <copyright file="JsonCamelCaseStringEnumConverter.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor
{
    using System.Text.Json;
    using System.Text.Json.Serialization;

    internal class JsonCamelCaseStringEnumConverter : JsonStringEnumConverter
    {
        public JsonCamelCaseStringEnumConverter()
            : base(JsonNamingPolicy.CamelCase)
        {
        }
    }
}
