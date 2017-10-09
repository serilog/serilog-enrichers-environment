# Serilog.Enrichers.Environment

Enriches Serilog events with information from the process environment.
 
[![Build status](https://ci.appveyor.com/api/projects/status/yfbvbdxd5vwh6955?svg=true)](https://ci.appveyor.com/project/serilog/serilog-enrichers-environment) [![NuGet Version](http://img.shields.io/nuget/v/Serilog.Enrichers.Environment.svg?style=flat)](https://www.nuget.org/packages/Serilog.Enrichers.Environment/)

To use the enricher, first install the NuGet package:

```powershell
Install-Package Serilog.Enrichers.Environment
```

Then, apply the enricher to you `LoggerConfiguration`:

```csharp
Log.Logger = new LoggerConfiguration()
    .Enrich.WithMachineName()
    // ...other configuration...
    .CreateLogger();
```

The `WithMachineName()` enricher will add a `MachineName` property to produced events.

### Included enrichers

The package includes:

 * `WithMachineName()` - adds `MachineName` based on either `%COMPUTERNAME%` (Windows) or `$HOSTNAME` (macOS, Linux)
 * `WithHashedMachineName()` -  same as `WithMachineName()`but hashes the value. Any `HashAlgorithm` derivative can be used. SHA256 is the default.
 * `WithEnvironmentUserName()` - adds `EnvironmentUserName` based on `USERNAME` and `USERDOMAIN` (if available)
 * `WithEnvironmentVariableValue(string envVarName)` - adds an  `Env_<envVarName>` property to the log event with the value for the specified enviroment variable.

Copyright &copy; 2016 Serilog Contributors - Provided under the [Apache License, Version 2.0](http://apache.org/licenses/LICENSE-2.0.html).
