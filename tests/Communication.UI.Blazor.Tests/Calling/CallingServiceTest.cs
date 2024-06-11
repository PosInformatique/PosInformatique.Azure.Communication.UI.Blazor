//-----------------------------------------------------------------------
// <copyright file="CallingServiceTest.cs" company="P.O.S Informatique">
//     Copyright (c) P.O.S Informatique. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace PosInformatique.Azure.Communication.UI.Blazor.Tests
{
    using Microsoft.JSInterop;

    public class CallingServiceTest
    {
        [Fact]
        public async Task CreateAdapterAsync()
        {
            var args = new CallAdapterArgs(default, default, default);

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("createCallAdapter", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(3);
                    a[0].As<Guid>().Should().NotBeEmpty();
                    a[1].Should().BeSameAs(args);
                    a[1].Should().NotBeNull();
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var jsRuntime = new Mock<IJSRuntime>(MockBehavior.Strict);
            jsRuntime.Setup(j => j.InvokeAsync<IJSObjectReference>("import", It.Is<object[]>(args => (string)args[0] == "./_content/PosInformatique.Azure.Communication.UI.Blazor/Calling/CallComposite.razor.js")))
                .ReturnsAsync(module.Object);

            var callingService = new CallingService(jsRuntime.Object);

            var adapter = await callingService.CreateAdapterAsync(args);

            adapter.Should().NotBeNull();

            jsRuntime.VerifyAll();
            module.VerifyAll();
        }

        [Fact]
        public async Task DisposeAsync()
        {
            var args = new CallAdapterArgs(default, default, default);

            var module = new Mock<IJSObjectReference>(MockBehavior.Strict);
            module.Setup(m => m.DisposeAsync())
                .Returns(ValueTask.CompletedTask);
            module.Setup(m => m.InvokeAsync<Microsoft.JSInterop.Infrastructure.IJSVoidResult>("createCallAdapter", It.IsAny<object[]>()))
                .Callback((string _, object[] a) =>
                {
                    a.Should().HaveCount(3);
                    a[0].As<Guid>().Should().NotBeEmpty();
                    a[1].Should().BeSameAs(args);
                    a[1].Should().NotBeNull();
                })
                .ReturnsAsync((Microsoft.JSInterop.Infrastructure.IJSVoidResult)null);

            var jsRuntime = new Mock<IJSRuntime>(MockBehavior.Strict);
            jsRuntime.Setup(j => j.InvokeAsync<IJSObjectReference>("import", It.Is<object[]>(args => (string)args[0] == "./_content/PosInformatique.Azure.Communication.UI.Blazor/Calling/CallComposite.razor.js")))
                .ReturnsAsync(module.Object);

            var callingService = new CallingService(jsRuntime.Object);

            await callingService.CreateAdapterAsync(args);

            await callingService.DisposeAsync();

            jsRuntime.VerifyAll();
            module.VerifyAll();
        }
    }
}