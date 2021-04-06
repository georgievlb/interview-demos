# Materials for developer interviews for Quickbase

## Purpose
The purpose of this exercise is not to give a "gotcha" question or puzzle, but a straight-forward (albeit contrived)
example of the kind of requirement that might arise in a real project so that we have shared context for a technical 
conversation during the interview. We are interested in how you approach a project, so you should feel free to add new 
class files as well modify the files that are provided as you see fit. Use of your favorite libraries or frameworks is
encouraged, but not required. How you demonstrate the correctness of your implementation is up to you.

## Requirements
The project requirement is to aggregate data (in this case population statistics) from two disparate sources.
We've provided two classes to represent those sources. `SqliteDbManager.cs`, provides access to a SQL database containing population
data for cities.  Each city is in a state within a country.  You need to write a method to retrieve the total
population for each country.  The other class, `IStatService.cs`, returns a `List<Tuple<String, Integer>>` containing 
country population data. For the purposes of this exercise, we've provided a concrete class that just returns a 
hard-coded list, but in a real project, assume it would be calling an API.

The assignment is to implement a solution that consumes these two data sources and returns the combined list of
countries and their populations. In the event of duplicate population data for a given country, the data from
the sql database should be used. 

## Building and Running the code

This project assumes you're using Visual Studio 2013 or newer and depends on nuget.  It currently does not 
support .Net Core or VScode.  A windows dev environment is assumed.  

That said, feel free to challenge any of the current limitations with your demo.  Just keep the time limit in mind.

## Solution by Lachezar Georgiev

### Overview

This solution fulfils the requirements by implementing domain driven design principles.

### Prerequisites

**Mandatory:**

1. Windows OS
1. [.Net Framework v4.5](https://dotnet.microsoft.com/download/dotnet-framework/net45) installed
1. Visual Studio 2013 or later (the solution works with Visual Studio 2019)

**Optional**
1. [NUnit 3 Test Adapter Visual Studio Extension](https://marketplace.visualstudio.com/items?itemName=NUnitDevelopers.NUnit3TestAdapter) or the NUnit 3 Test Adapter nuget package to run the tests.
1. `DGML Editor` installed from Visual Studio installer to view the project `Architecture.dgml` diagram. The lightweight version of the editor is available with Visual Studio 2019 community edition. Visual Studio 2019 Enterprise edition includes a rich diagram editor.

### Nuget issues with Visual Studio 2019

There is a potential issue when using Visual Studio 2019 to restore the nuget packages. Sometimes VS keeps crashing when it tries to restore the packages. To remedy this, the packages can be restored manually:
1. Go to https://www.nuget.org/downloads and download the latest `nuget.exe`. 
2. Open a command prompt or powershell in the project root folder e.g. `.\interview-demos\c#\`.
3. Restore the packages using either powershell or command prompt:
   - powershell: `.\nuget.exe restore .\Backend.sln` 
   - cmd: `nuget.exe restore Backend.sln`

### Run the project

1. Restore the nuget packages by using either Visual Studio or `nuget.exe`.
1. Build the solution.
1. Ensure that project `Backend` is set as the startup project.
1. Run without debugging (ctrl + F5).

#### Unit and Integration tests

Run the tests using the Test Explorer in Visual Studio. 
### Project structure

For more details, please see the `Architecture.dgml` file. 

The solution contains three projects:
1. Backend
    - Domain -> innermost layer which contains domain entities
    - Application -> the layer on top of `Domain` which encapsulates the application business logic. One important detail is country names are mapped against a standard. The chosen standard is ISO 3166 and the value is the country's `short name` propertuy. The motivation behind this decision is that when aggregating the country data, a standard name for each country must be used. The ISO standard in question does not define country names, but is built on top of country information from United Nations sources, which is the de-facto standard for country names. The ISO Standard can be found [here](https://www.iso.org/iso-3166-country-codes.html)
    - Presentation (No folder, just `Program.cs`) -> the layer which handles the UI, which in this case is the console.
    - Persistence -> this layer deals with DB operations. It communicates with the `Application` layer using the `ICountryAggregateManager` interface.
    - Common -> Cross-cutting concerns.
2. Backend.UnitTests
   - Unit tests follow the main project structure.
3. Backend.IntegrationTests
   - Unit tests follow the main project structure.
   - Run against a Sqlite db included in the project.

### Logging

The project uses `NLog` to write logs. By default, the log file is called `applicationlogs.txt` and is created in the project root folder.