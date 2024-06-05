//-----------------------------------------------------------------------
// <copyright file="ReflectionTools.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    using System.Reflection;

    public static class ReflectionTools
    {
        public static void SetFieldValue(this object instance, string name, object value)
        {
            var field = instance.GetType().GetField(name, BindingFlags.NonPublic | BindingFlags.Instance);

            field.SetValue(instance, value);
        }

        public static object GetPropertyValue(this object instance, string name)
        {
            var property = instance.GetType().GetProperty(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            return property.GetValue(instance, null);
        }

        public static void Invoke(this object instance, string name, params object[] args)
        {
            var method = instance.GetType().GetMethod(name, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);

            method.Invoke(instance, args);
        }
    }
}
