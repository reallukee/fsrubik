(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : Cube.fsi

    Title       : CUBE
    Description : Cube

    Author      : Luca Pollicino
                  (https://github.com/reallukee)
    Version     : 1.0.0
    License     : MIT
*)

namespace Reallukee.FsRubik

open Color
open Face
open Side

module Cube =
    type Cube = {
        upSide    : Side
        downSide  : Side
        frontSide : Side
        backSide  : Side
        leftSide  : Side
        rightSide : Side
    }

    val init : unit -> Cube
