WORK IN PROGRESS...

# FeedReader

![alt text](https://github.com/melihercan/FeedReader/blob/master/FeedReader.png)


RSS Feed Reader sample project. It demonstrates the usage of the following concepts and frameworks:
- Clean Architecture
- Blazor
- Xamarin

### Domain

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.


### Application

This layer contains all application logic. It is dependent on the domain layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.


### Infrastructure

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the application layer.

### BlazorUi / XamarinUi

This layer presents user interface. This layer depends on both the Application and Infrastructure layers, however, the dependency on Infrastructure is only to support dependency injection. Therefore only *Startup.cs* should reference Infrastructure.

## License

This project is licensed with the [MIT license](LICENSE).
