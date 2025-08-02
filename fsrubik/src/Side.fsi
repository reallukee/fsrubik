(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : Side.fsi

    Title       : SIDE
    Description : Side

    Author      : Luca Pollicino
                  (https://github.com/reallukee)
    Version     : 1.0.0
    License     : MIT
*)

namespace Reallukee.FsRubik

open Color
open Face

module Side =
    type Side = {
        face     : Face
        stickers : Color array
    }

    val init : face : Face -> stickers : Color -> Side
