(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : Render.fsi

    Title       : RENDER
    Description : Render

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
open History

module Render =
    type RGBColor = {
        red   : int
        green : int
        blue  : int
    }

    val reset : unit -> unit

    val foreground : RGBColor -> 'a -> string
    val background : RGBColor -> 'a -> string

    val hideCursor : unit -> unit
    val showCursor : unit -> unit

    val moveCursor : int -> int -> unit



    type Render = {
        upFace    : Face
        downFace  : Face
        frontFace : Face
        backFace  : Face
        leftFace  : Face
        rightFace : Face
    }

    type Direction =
        | Forward
        | Backward
        | Leftward
        | Rightward

    val init : unit -> Render

    val WhiteRGB  : RGBColor
    val YellowRGB : RGBColor
    val RedRGB    : RGBColor
    val OrangeRGB : RGBColor
    val GreenRGB  : RGBColor
    val BlueRGB   : RGBColor

    val flipHorizontal : Side -> Side
    val flipVertical   : Side -> Side

    val stickerForeground : Color -> ('a -> string)
    val stickerBackground : Color -> ('a -> string)

    val render2dSide : Side -> int -> int -> unit
    val render2d     : Cube -> int -> int -> unit

    // val render3dSide : Side -> Render -> unit
    // val render3d     : Cube -> Render -> unit
