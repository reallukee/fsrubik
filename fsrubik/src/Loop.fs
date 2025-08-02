(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : Loop.fs

    Title       : LOOP
    Description : Loop

    Author      : Luca Pollicino
                  (https://github.com/reallukee)
    Version     : 1.0.0
    License     : MIT
*)

namespace Reallukee.FsRubik

open System

open Color
open Face
open Side
open Cube
open Move
open History
open Render

module Loop =
    let sideLoop cube face render =
        hideCursor ()

        let rec loop cube face render =
            let side =
                match face with
                | Up    -> cube.upSide
                | Down  -> cube.downSide
                | Front -> cube.frontSide
                | Back  -> cube.backSide
                | Left  -> cube.leftSide
                | Right -> cube.rightSide

            render2dSide side 4 2

            moveCursor 0 0

            let key = Console.ReadKey(true)

            let cube, render =
                match key.Modifiers, key.Key with
                | _, ConsoleKey.UpArrow    -> cube, render
                | _, ConsoleKey.DownArrow  -> cube, render
                | _, ConsoleKey.LeftArrow  -> cube, render
                | _, ConsoleKey.RightArrow -> cube, render

                | ConsoleModifiers.Control, ConsoleKey.U -> applyMove cube U', render
                | ConsoleModifiers.Control, ConsoleKey.D -> applyMove cube D', render
                | ConsoleModifiers.Control, ConsoleKey.R -> applyMove cube R', render
                | ConsoleModifiers.Control, ConsoleKey.L -> applyMove cube L', render
                | ConsoleModifiers.Control, ConsoleKey.F -> applyMove cube F', render
                | ConsoleModifiers.Control, ConsoleKey.B -> applyMove cube B', render

                | _, ConsoleKey.U -> applyMove cube U, render
                | _, ConsoleKey.D -> applyMove cube D, render
                | _, ConsoleKey.R -> applyMove cube R, render
                | _, ConsoleKey.L -> applyMove cube L, render
                | _, ConsoleKey.F -> applyMove cube F, render
                | _, ConsoleKey.B -> applyMove cube B, render

                | _, _ -> cube, render

            let face = render.upFace

            if key.Key <> ConsoleKey.Escape then
                loop cube face render

        loop cube face render

        showCursor ()

    let advancedSideLoop cube face render history =
        hideCursor ()

        let rec loop cube face render history =
            let side =
                match face with
                | Up    -> cube.upSide
                | Down  -> cube.downSide
                | Front -> cube.frontSide
                | Back  -> cube.backSide
                | Left  -> cube.leftSide
                | Right -> cube.rightSide

            render2dSide side 4 2

            moveCursor 0 0

            let key = Console.ReadKey(true)

            let cube, render, history =
                match key.Modifiers, key.Key with
                | ConsoleModifiers.Control, ConsoleKey.Z
                | ConsoleModifiers.Alt, ConsoleKey.Z when canUndo history ->
                    let cube, history = tryUndo cube history

                    cube, render, history
                | ConsoleModifiers.Control, ConsoleKey.Y
                | ConsoleModifiers.Alt, ConsoleKey.Y when canRedo history ->
                    let cube, history = tryRedo cube history

                    cube, render, history

                | ConsoleModifiers.Control, ConsoleKey.Backspace ->
                    let cube = Cube.init ()

                    cube, render, history
                | _, ConsoleKey.Backspace ->
                    let history = History.init cube

                    cube, render, history

                | _, ConsoleKey.UpArrow    -> cube, render, history
                | _, ConsoleKey.DownArrow  -> cube, render, history
                | _, ConsoleKey.LeftArrow  -> cube, render, history
                | _, ConsoleKey.RightArrow -> cube, render, history

                | _, _ ->
                    let move =
                        match key.Modifiers, key.Key with
                        | ConsoleModifiers.Control, ConsoleKey.U -> Some U'
                        | ConsoleModifiers.Control, ConsoleKey.D -> Some D'
                        | ConsoleModifiers.Control, ConsoleKey.R -> Some R'
                        | ConsoleModifiers.Control, ConsoleKey.L -> Some L'
                        | ConsoleModifiers.Control, ConsoleKey.F -> Some F'
                        | ConsoleModifiers.Control, ConsoleKey.B -> Some B'

                        | _, ConsoleKey.U -> Some U
                        | _, ConsoleKey.D -> Some D
                        | _, ConsoleKey.R -> Some R
                        | _, ConsoleKey.L -> Some L
                        | _, ConsoleKey.F -> Some F
                        | _, ConsoleKey.B -> Some B

                        | _, _ -> None

                    match move with
                    | Some move -> applyMove cube move, render, save cube history
                    | None -> cube, render, history

            let face = render.upFace

            if key.Key <> ConsoleKey.Escape then
                loop cube face render history

        loop cube face render history

        showCursor ()

    let cubeLoop cube =
        hideCursor ()

        let rec loop cube =
            render2d cube 4 2

            moveCursor 0 0

            let key = Console.ReadKey(true)

            let cube =
                match key.Modifiers, key.Key with
                | ConsoleModifiers.Control, ConsoleKey.U -> applyMove cube U'
                | ConsoleModifiers.Control, ConsoleKey.D -> applyMove cube D'
                | ConsoleModifiers.Control, ConsoleKey.R -> applyMove cube R'
                | ConsoleModifiers.Control, ConsoleKey.L -> applyMove cube L'
                | ConsoleModifiers.Control, ConsoleKey.F -> applyMove cube F'
                | ConsoleModifiers.Control, ConsoleKey.B -> applyMove cube B'

                | _, ConsoleKey.U -> applyMove cube U
                | _, ConsoleKey.D -> applyMove cube D
                | _, ConsoleKey.R -> applyMove cube R
                | _, ConsoleKey.L -> applyMove cube L
                | _, ConsoleKey.F -> applyMove cube F
                | _, ConsoleKey.B -> applyMove cube B

                | _, _ -> cube

            if key.Key <> ConsoleKey.Escape then
                loop cube

        loop cube

        showCursor ()

    let advancedCubeLoop cube history =
        hideCursor ()

        let rec loop cube history =
            render2d cube 4 2

            moveCursor 0 0

            let key = Console.ReadKey(true)

            let cube, history =
                match key.Modifiers, key.Key with
                | ConsoleModifiers.Control, ConsoleKey.Z
                | ConsoleModifiers.Alt, ConsoleKey.Z when canUndo history ->
                    tryUndo cube history
                | ConsoleModifiers.Control, ConsoleKey.Y
                | ConsoleModifiers.Alt, ConsoleKey.Y when canRedo history ->
                    tryRedo cube history

                | ConsoleModifiers.Control, ConsoleKey.Backspace ->
                    let cube = Cube.init ()

                    cube, history
                | _, ConsoleKey.Backspace ->
                    let history = History.init cube

                    cube, history

                | _, _ ->
                    let move =
                        match key.Modifiers, key.Key with
                        | ConsoleModifiers.Control, ConsoleKey.U -> Some U'
                        | ConsoleModifiers.Control, ConsoleKey.D -> Some D'
                        | ConsoleModifiers.Control, ConsoleKey.R -> Some R'
                        | ConsoleModifiers.Control, ConsoleKey.L -> Some L'
                        | ConsoleModifiers.Control, ConsoleKey.F -> Some F'
                        | ConsoleModifiers.Control, ConsoleKey.B -> Some B'

                        | _, ConsoleKey.U -> Some U
                        | _, ConsoleKey.D -> Some D
                        | _, ConsoleKey.R -> Some R
                        | _, ConsoleKey.L -> Some L
                        | _, ConsoleKey.F -> Some F
                        | _, ConsoleKey.B -> Some B

                        | _, _ -> None

                    match move with
                    | Some move -> applyMove cube move, save cube history
                    | None -> cube, history

            if key.Key <> ConsoleKey.Escape then
                loop cube history

        loop cube history

        showCursor ()
