# Origin Takehome Assignment

### Summary
* [Setup & Run](#pre-requisites)
* [Design](#design)
* [Business Decisions](#business-decisions)
* [Tech Decisions](#tech-decisions)

### Pre-Requisites
* [.Net 5](https://dotnet.microsoft.com/en-us/download/dotnet/5.0)

### Running The Application
* Once you have .Net 5 installed, open a console and navigate to the 'Src' folder
* Execute the following command on the console:
```
dotnet run --project OriginTechDemo\OriginTechDemo.csproj
```
* The application will be listening on: [http://localhost:5000/swagger](http://localhost:5000/swagger)

### Running The Tests
* Once you have .Net 5 installed, open a console and navigate to the 'Src' folder
* Execute the following command on the console:
```
dotnet test OriginTechDemo.Tests\OriginTechDemo.Tests.csproj
```
* The execution result will be displayed on the console.

## Design

The projects inside the solution are based on the following model:

[![N|Solid](https://alexcodetuts.files.wordpress.com/2020/02/untitled-diagram.png?w=640)](https://nodesource.com/products/nsolid)

### Presentation
This layer is the entrance to the application. 
Here we have all the initial setup such as: 
* Initial configuration
* Configuring middlewares 
* Registering services for DI 
* Exposing controllers

### Application
This layer is the orchestrator.
Here we have all the artifacts that we need to interact with the domain layer in order to accomplish the requirements. It's important to keep this layer as agnostic as possible of domain rules, so its unique responsibility is to orchestrate the calls, collect the data and return it to the previous layer.
The main artifacts of this layer are:
* Services (orchestrators)
* MappingProfiles (isolated instructions to map one class data to another)
* Models (classes that help to accomplish the requirements but do not belong to the domain)
* ViewModels (request and response contracts to avoid exposing the domain entities to the world and decouples the endpoint's contract from the domain core)
* Validator (classes that contains the basic validation for each viewmodel)

### Domain
This is the core layer.
Here we have all the business logic. The domain should be always isolated from the other layers, in a way that it does not have any external concrete dependency.
The main artifacts of this layer are:
* Entities (domain classes that represents something meaningful to the business)
* Enums (items to keep track of a range of value possibilities to a specific field)
* Interfaces (responsible for communicating with other layers without high coupling)
* ScoreCalculator (classes that get the business rules and to apply them for each line of insurance)
* Rules (business logic rule to a specific line of insurance)

### Infrastructure
This is the external layer.
Everything that goes outside the application, should belong to this layer.
The main artifacts of this layer are:
* Services (concrete implementations of domain or application interfaces)

### Shared
This is the utilities layer.
Everything, built to support all the other layers, should belong to this layer.
The main artifacts of this layer are:
* Helpers (utilities classes)

## Business Decisions
#### Rule Isolation
I made every business rule into an isolated class, so I wouldn't worry to add or change one rule and break some other rule. Every rule has a property called IsuranceLine that tells what kind of insurance this rule belongs to. This makes the adding of a new rule pretty easy.
#### Rule Base Class
All rules must inherit from the same BaseScoreRule abstract class and implement the interface IScoreRule to be able for execution, so future rules would have a pretty solid direction to follow in terms of pattern.
#### Rule Toggle
I implemented the possibility to deactivate a specific rule without having to essentially redeploy the application. In this version, all the rules are always active, but with a simple implementation of the IExternalConfigurationService we could get this data from a database for example (with cache to avoid the latency, of course).
#### Base Calculator
For every line of insurance, I created a calculator, but all of them inherits from the same base class. The  BaseScoreCalculator is the one who carries the real logic, and the subclasses job is to basically set what kind of rules are going to be executed. Let's say we are talking about the life insurance, the LifeInsuranceCalculator tells its base class to apply only the rules that belongs to this line of insurance. If for some reason we have a specific validation to do, we could easily override the base class Calculate method to do so.
This model makes it real easy to create new lines of insurance or to modify the existing ones without creating too much difference between them.
### Tech Decisions
##### Interfaces Only
All the communication between layers are interface based except for the shared static classes.
##### Logs
I created a logging interface named ILoggingService and a simple implementation for it. We can see, on the console, every rule that was executed and what was the result of it.
##### No Exceptions
I avoided to use exceptions due to its potentially high memory consumption.
##### If an Exceptions Happen, We Can Handle It
I created a whole dedicated middleware to catch any exception that might occur and transform it into a less scary result.

