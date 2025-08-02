(*
    F# Script

    dotnet fsi advanceCubeLoop.fsx
*)

// Necessary for F# Interactive
// dotnet build fsrubik --configuration Release

// #r @"../fsrubik/bin/Release/netstandard2.0/fsrubik.dll"

// Necessary for F# Interactive
// dotnet build fsrubik --configuration Release
// dotnet pack fsrubik --configuration Release

#r @"nuget: Reallukee.FsRubik, 1.0.0"

open System

open Reallukee.FsRubik

printfn "\u001B[0m"

do Console.Clear();

let cube = Cube.init ()
let history = History.init cube

Loop.advancedCubeLoop cube history

do Console.ReadKey(true)
|> ignore

printfn "\u001B[0m"

Console.CursorVisible <- true
