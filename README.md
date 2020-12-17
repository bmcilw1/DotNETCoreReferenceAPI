# TodoAPI DotNETCoreReferenceAPI
A simple API on .NET Core with Entity Framework

The flow of control is as follows
```
Controller -> Service -> Repository
```

The controller handles the client connection. The Repository handles data i/o from the database. The Service includes any related business logic, cross-domain model mapping, etc.

Note, the only layer that is aware of Entity Framework is the Repository. Both the Controller and Service layers are testable in isolation of the final Repository layer.

See [here](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio-code) for an explanation of basic .NET Core Web API boilerplate, with a non-test-friendly version with all code directly in the controller.

## Getting Started
1. Install [dotnet core](https://dotnet.microsoft.com/download)
2. Restore packages with `dotnet restore`
3. Run project with `dotnet run` from within the TodoAPI directory
4. View swagger documentation at `https://localhost:<port>/swagger`. For example, `https://localhost:5001/swagger`
5. Interact with the TodosAPI at `https://localhost:<port>/api/Todo`. For example, `https://localhost:5001/api/Todo`

## Testing
* Run unit tests with `dotnet test` from within the TodoAPITests directory
* Run integration tests with `dotnet test` from within the TodoAPIIntegrationTests directory

## Final Thoughts
* Unit testing can be trivially added to the service layer as that layer acquires business logic.
* As written the integration tests share a common instance of a seeded in-memory database. This can easily lead to test leakage. If this becomes unmanageable, CRUD tests can be rewritten to load their own needed data in the in-memory database, or the db can be re-initialized between tests. Either change will increase the time to run tests, but reduce tests breaking due to test leakage from other tests.
* The API operations are currently written as async operations. However, with CRUD operations they are immediately awaited. This adds an overhead that increases overall latency. Consider if your use case warrants switching to synchronous operations.

## License
* MIT License