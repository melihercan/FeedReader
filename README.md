WORK IN PROGRESS...

# FeedReader

![alt text](https://github.com/melihercan/FeedReader/blob/master/FeedReader.png)


RSS Feed Reader sample project. It demonstrates the usage of the following concepts and frameworks:
- Clean Architecture
- Blazor
- Xamarin

### Entities

This will contain all entities, enums, exceptions, interfaces, types and logic specific to the domain layer.


### Interactors

This layer contains all application logic. It is dependent on the entities layer, but has no dependencies on any other layer or project. This layer defines interfaces that are implemented by outside layers. For example, if the application need to access a notification service, a new interface would be added to application and an implementation would be created within infrastructure.


### Gateways

This layer contains classes for accessing external resources such as file systems, web services, smtp, and so on. These classes should be based on interfaces defined within the interactors layer.

### UserInterfaces

This layer presents user interface. This layer depends on interactors layer.

## License

This project is licensed with the [MIT license](LICENSE).
