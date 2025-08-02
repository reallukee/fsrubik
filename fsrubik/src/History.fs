(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : History.fs

    Title       : HISTORY
    Description : History

    Author      : Luca Pollicino
                  (https://github.com/reallukee)
    Version     : 1.0.0
    License     : MIT
*)

namespace Reallukee.FsRubik

open Color
open Face
open Side
open Cube
open Move

module History =
    type History = {
        undo : Cube List
        redo : Cube List
        full : Cube List
    }

    let init cube =
        {
            undo = []
            redo = []
            full = [ cube ]
        }

    let save cube history =
        let {
            undo = currentUndo
            redo = currentRedo
            full = currentFull
        } = history

        { history with
            undo = cube :: currentUndo
            redo = []
            full = cube :: currentFull
        }

    let canUndo history =
        let {
            undo = undo
        } = history

        not (List.isEmpty undo)

    let canRedo history =
        let {
            redo = redo
        } = history

        not (List.isEmpty redo)

    let undo cube history =
        let {
            undo = currentUndo
            redo = currentRedo
            full = currentFull
        } = history

        match currentUndo with
        | last :: rest ->
            Some (
                last,
                {
                    undo = rest
                    redo = cube :: currentRedo
                    full = last :: currentFull
                }
            )
        | [] -> None

    let redo cube history =
        let {
            undo = currentUndo
            redo = currentRedo
            full = currentFull
        } = history

        match currentRedo with
        | last :: rest ->
            Some (
                last,
                {
                    undo = cube :: currentUndo
                    redo = rest
                    full = last :: currentFull
                }
            )
        | [] -> None

    let tryUndo cube history =
        undo cube history
        |> Option.defaultValue (cube, history)

    let tryRedo cube history =
        redo cube history
        |> Option.defaultValue (cube, history)
