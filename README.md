C# Corti Import API Client Sample application
================

This sample application demonstrates making HTTP requests to a Corti REST Import API in order to add additional information to cases and create/update custom events. 

Structure
------------

All API client specific code is located in the `client` folder. 
`entity` folder contains entities which you should extend with attributes you want to be imported to the Corti.
`repo` folder contains repositories for fetching entities. This should also be implemented manually depending on the underlying data storage.
`manager` folder contains import manager that is responsible for demo import flow.
`Program.cs` main application entry point which initiates demo import flow by calling `ImportManager.ImportAllCasesAndEvents()`

Import flow
------------

`ImportManager.ImportAllCasesAndEvents()` demonstrates how domain models like case and event can be fetched (now mocked), transformed and imported into Import API. 
It starts by fetching all cases with associated events. Next it iterates through each case and does the following:
1. Finding `caseId` that is used by Corti by searching triage sessions associated with `externalId`. Given it's a third-party ID, there may be instances where multiple cases have the same externalID (incident ID), so the endpoint may return multiple cases. In this example, the first session is picked but you can implement a different selection logic.
2. Converting internal case representation to external (or Corti specific) that will be used by Import API Client to import case properties. Note that `ConvertCaseToExternal` should be modified on `Case` entity structure change. 
3. Converting internal event representation to external (or Corti specific) that will be used by Import API Client to import custom event. Note that `ConvertEventToExternal` should be modified on `Event` entity structure change. 
Converted external representations are saved to separate lists which in the end are used to import them at once rather one by one. Note that import API is transactional so any error will result in all of provided entities not being saved. 

Build and Run
-------------

To build and run the sample, type the following two commands:

`dotnet restore`
`dotnet run`

`dotnet restore` restores the dependencies for this sample.
`dotnet run` builds the sample and runs the output assembly.

Examples
-------------

Connecting to https://api.beta.sfd.motocorti.io with a security token `2e3aa210-5aab-5c6a-bb35-8c8811d23e06`:
`dotnet run -- -h "api.beta.sfd.motocorti.io" -p 443 -t "2e3aa210-5aab-5c6a-bb35-8c8811d23e06"`
