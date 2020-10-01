# FeedReader
## WEB UI (Blazor WASM): 
![alt text](https://github.com/melihercan/FeedReader/blob/master/doc/BlazorWasm.gif)

## WEB UI (Blazor server side): 
![alt text](https://github.com/melihercan/FeedReader/blob/master/doc/BlazorServerSide.gif)

## Desktop UI - Windows (ElectronNET):
![alt text](https://github.com/melihercan/FeedReader/blob/master/doc/DesktopWindows.gif)

## Mobile UI - Android (XamarinForms):
<img src=https://github.com/melihercan/FeedReader/blob/master/doc/Android.gif height="800">

## Mobile UI - iOS (XamarinForms):
<img src=https://github.com/melihercan/FeedReader/blob/master/doc/iOS.gif height="800">

## Console UI - Android (.NET Core worker):
![alt text](https://github.com/melihercan/FeedReader/blob/master/doc/Console.gif)

## Block diagram:
![alt text](https://github.com/melihercan/FeedReader/blob/master/doc/FeedReader.png)


Feed Reader is a sample project. The main goal of this application is to demonstrate the [Clean Architecture by Robert C. Martin aka Uncle Bob](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) principles. As Uncle Bob states the heart of the application - business logic is completely decoupled from the rest of modules. It is testable, independent of UIs, independent of databases and independent of any external agencies. Everything else other than the business logic (Core) is detail, a plugin and can be replaced without effecting the Core.

Different UI modules use the same Core through messaging (MediatR). All conveivable UI types have been implemented, namely WEB, Mobile, Desktop and Console. 

All service and database accesses to the Core are provided via interfaces. Each interface has been implemented as a plugin in Infrastructure layer and can be replaced in the future without effecting the rest of the system. Infrastructure modules are using Infrastructure server as backend.

Application employs the following concepts and frameworks:
- Clean Architecture
- .NET Core
- Blazor
- Xamarin
- WebAPI
- MediatR
- Identity Server
- Reactive Extensions
- OAUTH2/OIDC and social media authetication

### Core Layer (Domain and Application)

This is the core of the application. It contains:
- Entities
- Use Cases
- Interfaces

### Infrastructure

Implementation of the interface groups:
- Feed
- Data Repositrory
- Token Repository
- User

### UserInterfaces

This layer presents user interfaces:
- WEB UI
- Mobile UI
- Desktop UI
- Console UI

## License

This project is licensed with the [MIT license](LICENSE).
