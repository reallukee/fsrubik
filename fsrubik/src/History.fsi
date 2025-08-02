(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : History.fsi

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

    val init : Cube -> History

    val save : Cube -> History -> History

    val canUndo : History -> bool
    val canRedo : History -> bool

    val undo : Cube -> History -> (Cube * History) option
    val redo : Cube -> History -> (Cube * History) option

    val tryUndo : Cube -> History -> Cube * History
    val tryRedo : Cube -> History -> Cube * History
