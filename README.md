# QUno

An Uno-like card game.

## Requirements

* [.NET 9 SDK](https://dotnet.microsoft.com/download)
* Your favorite editor (my favorite editor is [Visual Studio Code](https://code.visualstudio.com/))

## How To Play

`QUnoConsole` is a console application for testing the engine, or if you want to 
enjoy a relaxing game from your terminal. Use `--help` to see the instructions. For example:
```
QUnoConsole --computer-players=3 --human-players=1
```
Computer players will automatically play; as a human player, follow the prompts.

`QUnoFunctional` is also console application, but it is designed for computer play only. It can 
be used to stress-test the engine or see how different algorithms perform. For example:
```
QUnoFunctional 4 12
```
will add 4 computer players to each game and play 12 games in total.

## Developer Notes

This repository includes a straightforward .NET class library, unit test project, and 
console applications implemented in C# and F#. The original code has been in 
development since .NET Framework v2, but these projects have been updated for modern .NET 
and implemented as SDK-style projects. Thus you can use the standard `dotnet build`, 
`dotnet run`, `dotnet test` workflow on the individual projects, or you can use 
Visual Studio to open the full solution.

* `QUnoLibrary` contains the game engine. The `Mooville.QUno.Model` namespace can be used 
by any kind of client application. The `Mooville.QUno.ViewModel` namespace has types that 
support implementing a user interface that follows the Model-View-ViewModel pattern.
* `QUnoLibraryTest` contains unit tests for the game engine.
* `QUnoConsole` contains a C# console application for driving the game engine interactively.
* `QUnoFunctional` contains a F# console application driving the game engine programmatically.

The game engine and applications in this repository are intended to be completely cross-platform 
and require only the .NET runtime.
