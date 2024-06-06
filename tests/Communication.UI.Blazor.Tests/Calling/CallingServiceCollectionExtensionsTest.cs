//-----------------------------------------------------------------------
// <copyright file="CallingServiceCollectionExtensionsTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.JSInterop;

    public class CallingServiceCollectionExtensionsTest
    {
        [Fact]
        public void AddCalling()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddSingleton(sp => Mock.Of<IJSRuntime>());
            serviceCollection.AddCalling().Should().BeSameAs(serviceCollection);

            var sp = serviceCollection.BuildServiceProvider();

            sp.GetRequiredService<ICallingService>().Should().NotBeNull();
            sp.GetRequiredService<ICallingService>().Should().BeSameAs(sp.GetRequiredService<ICallingService>());
        }
    }
}