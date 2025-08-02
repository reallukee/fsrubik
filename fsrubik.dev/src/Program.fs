namespace Reallukee.FsRubik

open System

open Reallukee.FsRubik

module Program =
    [<EntryPoint>]
    let main _ =
        Console.Title <- "FsRubik"

        (*
        let cube = Cube.init ()

        Loop.cubeLoop cube
        *)

        let cube = Cube.init ()
        let history = History.init cube

        Loop.advancedCubeLoop cube history

        do Console.ReadKey(true)
        |> ignore

        0
