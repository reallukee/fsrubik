(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : Loop.fsi

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
    val sideLoop         : Cube -> Face -> Render -> unit
    val advancedSideLoop : Cube -> Face -> Render -> History -> unit

    val cubeLoop         : Cube -> unit
    val advancedCubeLoop : Cube -> History -> unit
