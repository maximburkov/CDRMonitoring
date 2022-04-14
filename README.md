# Call Details Record Monitoring

CDR monitoring console application, it also could be extended with Web and/or Desktop UI.


## Available commands

Sample folder has self contained exe file that could be executed with following commands:

Show total info: `CDRMonitorig.Console.exe info "sample_data.csv"`

Show help: `CDRMonitorig.Console.exe -h`

Show help for check command: `CDRMonitorig.Console.exe check -h`

Check "dialled more than 5 times" rule: `CDRMonitorig.Console.exe check dialed-same-number "sample_data.csv"`

Check "using the same CLI more than 5 times" rule: `CDRMonitorig.Console.exe check from-same-number "sample_data.csv"`

It' also possible to run from Visula studio and set command line arguments

![image](https://user-images.githubusercontent.com/34780495/163286908-d0024f93-5a09-468d-8bef-daca92f287be.png)

CSV file with sample data added to binaries output.

## Project structure

Software implemented with Clean Architecture approach and domain logic doesn't depend on persistence mechanisms or application type.

![clean](https://user-images.githubusercontent.com/34780495/163287664-8f2133c3-7c8f-49b8-a21a-660fc10e449b.png)

- CDRMonitoring.Domain - project with Domain models, Value Objects and business rules. Doesn't not depend on Application and Infrastructure project. Doesn't contain repository implementation, only abstractions.
- CDRMonitoring.Infrastructure - has infrastructure specific things such as storage. Repositories implementation should be here.
- CDRMonitoring.Console - console application. Implemented with Cocona micro-framework. Could be easily replaced with Asp.net core minimal api endpoints.
- CDRMonitoring.Domain.Tests - unit tests for application domain (partially implemented).

## Used libraries

- [Cocona](https://github.com/mayuki/Cocona) - micro-framework for console apps.
- [CsvHelper](https://github.com/JoshClose/CsvHelper) - for reading csv files.
- [Serilog](https://github.com/serilog/serilog) - for logging.
- [xUnit](https://github.com/xunit/xunit) - testing framework.
- [Fluent Assertions](https://github.com/fluentassertions/fluentassertions) - for assertions in Unit tests.
- [Moq](https://github.com/moq/moq4) - for mocking in Unit tests.

