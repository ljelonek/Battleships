# Battleships
Battleships game as an executable.

## How to build:
Navigate to root directory in this repository. Run command `dotnet test`.

## How to run:
Navigate to root directory in this repository. Run command `dotnet run --project ./src/Battleships.Game/Battleships.Game.csproj`.

## Game rules:
- Program creates 10x10 grid and places several ships at random places with predefined sizes.
- There are 2 Destroyers (4 squares) and one Battleship (5 squares).
- Player plays against Computer.
- The player enters or selects coordinates of the form "A5", where "A" is the column and "5" is the row, to specify a square to target.
- Shots result in hits, misses or sinks. The game ends when all ships are sunk.
- At the end of the game, total number of hits is displayed. Try to get the lowest possible number (13)!
- Duplicated entries do not increase the count of hits.