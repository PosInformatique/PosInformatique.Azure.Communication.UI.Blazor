# PosInformatique.Azure.Communication.UI.Blazor

PosInformatique.Azure.Communication.UI.Blazor is a C# wrapper Blazor library based on the Microsoft
[Azure Communication Services UI Library](https://azure.github.io/communication-ui-library/?path=/story/overview--page).

The [Azure Communication Services UI Library](https://azure.github.io/communication-ui-library/?path=/story/overview--page)
library contains some basic and composites components which use
[Azure Communication Services](https://azure.microsoft.com/fr-fr/products/communication-services) for Chat and Calling
features.

The API of the library try to match as much as possible the
[Azure Communication Services UI Library API](https://azure.github.io/communication-ui-library/?path=/story/overview--page)
with the concept of [composite adapters](https://azure.github.io/communication-ui-library/?path=/docs/composite-adapters--page).

## Demo project
Do not hesitate to run the [tests/Communication.UI.Blazor.Demo](./tests/Communication.UI.Blazor.Demo)
application which contains an example usage of the
[PosInformatique.Azure.Communication.UI.Blazor](https://github.com/PosInformatique/PosInformatique.Azure.Communication.UI.Blazor)
library (I use it for my integration tests ;-)).

### Start the demo application
To start the project:
- Creates and add a JSON file `appsettings.json` in the
[tests/Communication.UI.Blazor.Demo/wwwroot](./tests/Communication.UI.Blazor.Demo/wwwroot) folder:

```json
{
  "ACS_CONNECTION_STRING": "<Connection string>"
}
```

With:
- `<Connection string>`: The connection string of the Azure Communication Services ressources which can be retrieved
from the [Azure Portal](https://learn.microsoft.com/en-us/azure/communication-services/quickstarts/create-communication-resource?tabs=windows&pivots=platform-azp#access-your-connection-strings-and-service-endpoints).

### Create or using Azure Communication Services identity
The demo project required to use a Azure Communication Services identity. You can create an user directly
by clicking the `Create` button or by specifing the Communication User Identifier in the `UserID` textbox.

## Components

- [CallComposite](./docs/Components/CallComposite.md): Provides a calling experience that allows users to start or join a call

## Architecture

This library use natively the releases
[Azure Communication Services UI Library](https://azure.github.io/communication-ui-library/?path=/story/overview--page)
which are available in the official [GitHub project](https://github.com/Azure/communication-ui-library/releases).

To simplify dependencies, avoid usage of the composite JavaScript library and include React engine, the 
[src/Communication.UI.Blazor/AzureCommunicationReact](./src/Communication.UI.Blazor/AzureCommunicationReact)
folder contains a Webpack project to bundle some NPM dependencies and compile single module library
which is used by Blazor components.

The Webpack project is automatically compiled when compiling the [src/Communication.UI.Blazor](./src/Communication.UI.Blazor)
project and produce the `azure-communication-react-bundle.js` used by the Blazor library.

## Dependencies
This currently library use the last stable release version of the
[Azure Communication Services UI Library](https://azure.github.io/communication-ui-library/?path=/story/overview--page)
library. Currently it is based on the
[v1.14.0](https://github.com/Azure/communication-ui-library/releases/tag/PublicPreview%2F1.14.0)
library.

The library is based on the minimal ASP .NET Core Blazor version which is the 8.0.0 and
can be used for the application based on this version or higher.

## Ported APIs

The list of the ported API is available in the [Ported API](./docs/PortedApi.md) page.