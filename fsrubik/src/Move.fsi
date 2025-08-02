(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : Move.fsi

    Title       : MOVE
    Description : Move

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

module Move =
    type Move =
    | U
    | U'
    | D
    | D'
    | R
    | R'
    | L
    | L'
    | F
    | F'
    | B
    | B'

    val rotateClockwise        : Side -> Side
    val rotateCounterClockwise : Side -> Side

    type HorizontalLayer =
        | Top
        | Middle
        | Bottom

    type VerticalLayer =
        | Start
        | Center
        | End

    type Layer =
        | Horizontal of HorizontalLayer
        | Vertical of VerticalLayer

    val horizontalIndices : HorizontalLayer -> int array
    val verticalIndices   : VerticalLayer   -> int array
    val indices           : Layer           -> int array

    val horizontalLayer : Side -> HorizontalLayer -> Color array
    val verticalLayer   : Side -> VerticalLayer   -> Color array

    val layer : Side -> Layer -> Color array

    val updateHorizontalLayer : Side -> Side -> HorizontalLayer -> Side
    val updateVerticalLayer   : Side -> Side -> VerticalLayer   -> Side

    val updateLayer : Side -> Layer -> Side -> Layer -> Side

    val updateTopLayer    : Side -> Side -> Side
    val updateMiddleLayer : Side -> Side -> Side
    val updateBottomLayer : Side -> Side -> Side

    val updateStartLayer  : Side -> Side -> Side
    val updateCenterLayer : Side -> Side -> Side
    val updateEndLayer    : Side -> Side -> Side

    val u  : Cube -> Cube
    val u' : Cube -> Cube
    val d  : Cube -> Cube
    val d' : Cube -> Cube
    val r  : Cube -> Cube
    val r' : Cube -> Cube
    val l  : Cube -> Cube
    val l' : Cube -> Cube
    val f  : Cube -> Cube
    val f' : Cube -> Cube
    val b  : Cube -> Cube
    val b' : Cube -> Cube

    val applyMove : Cube -> Move -> Cube

    val randomMove : unit -> Move
