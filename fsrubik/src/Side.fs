(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : Side.fs

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

    let init face stickers = {
        face     = face
        stickers = Array.create 9 stickers
    }
