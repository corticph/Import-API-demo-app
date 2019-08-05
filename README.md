C# Corti Import API Client Sample application
================

This sample application demonstrates making HTTP requests to a Corti REST Import API in order to add additional information to cases and create/update custom events. 

Integration
------------

All API client specific code is located in the client folder. `ImportManager.ImportAllCasesAndEvents()` demonstrates how domain models like case and event can be fetched (now mocked), transformed and imported into Import API. 

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
