(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : Face.fsi

    Title       : FACE
    Description : Face

    Author      : Luca Pollicino
                  (https://github.com/reallukee)
    Version     : 1.0.0
    License     : MIT
*)

namespace Reallukee.FsRubik

open Color

module Face =
    type Face =
        | Up
        | Down
        | Front
        | Back
        | Left
        | Right
