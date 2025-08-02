(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : Render.fs

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
    let private CSI = "\u001B["

    type RGBColor = {
        red   : int
        green : int
        blue  : int
    }

    let reset () =
        printf "%s0m" CSI

    let foreground rgbColor =
        fun text ->
            let {
                red   = red
                green = green
                blue  = blue
            } = rgbColor

            $"{CSI}38;2;{red};{green};{blue}m{text}"

    let background rgbColor =
        fun text ->
            let {
                red   = red
                green = green
                blue  = blue
            } = rgbColor

            $"{CSI}48;2;{red};{green};{blue}m{text}"

    let hideCursor () =
        printf "%s?25l" CSI

    let showCursor () =
        printf "%s?25h" CSI

    let moveCursor col row =
        printf "%s%d;%dH" CSI (row + 1) (col + 1)



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

    let init () =
        {
            upFace    = Up
            downFace  = Down
            frontFace = Front
            backFace  = Back
            leftFace  = Left
            rightFace = Right
        }

    let WhiteRGB   = { red = 255; green = 255; blue = 255 }
    let YellowRGB  = { red = 255; green = 225; blue = 0   }
    let RedRGB     = { red = 255; green = 0;   blue = 0   }
    let OrangeRGB  = { red = 255; green = 128; blue = 0   }
    let GreenRGB   = { red = 0;   green = 200; blue = 0   }
    let BlueRGB    = { red = 0;   green = 0;   blue = 255 }

    let flipHorizontal side =
        let {
            stickers = currentStickers
        } = side

        let stickers =
            [|
                currentStickers[6]; currentStickers[7]; currentStickers[8]
                currentStickers[3]; currentStickers[4]; currentStickers[5]
                currentStickers[0]; currentStickers[1]; currentStickers[2]
            |]

        { side with
            stickers = stickers
        }

    let flipVertical side =
        let {
            stickers = currentStickers
        } = side

        let stickers =
            [|
                currentStickers[2]; currentStickers[1]; currentStickers[0]
                currentStickers[5]; currentStickers[4]; currentStickers[3]
                currentStickers[8]; currentStickers[7]; currentStickers[6]
            |]

        { side with
            stickers = stickers
        }

    let stickerForeground color =
        match color with
        | White  -> foreground WhiteRGB
        | Yellow -> foreground YellowRGB
        | Red    -> foreground RedRGB
        | Orange -> foreground OrangeRGB
        | Green  -> foreground GreenRGB
        | Blue   -> foreground BlueRGB

    let stickerBackground color =
        match color with
        | White  -> background WhiteRGB
        | Yellow -> background YellowRGB
        | Red    -> background RedRGB
        | Orange -> background OrangeRGB
        | Green  -> background GreenRGB
        | Blue   -> background BlueRGB

    let render2dSide side col row =
        let {
            stickers = stickers
        } = side

        moveCursor col row

        stickers
        |> Array.iteri(fun index item ->
            " "
            |> stickerForeground item
            |> stickerBackground item
            |> printf "%s"

            if (index + 1) % 3 = 0 then
                let col = col
                let row = row + (index + 1) / 3

                moveCursor col row
        )

    let render2d cube col row =
        let {
            upSide    = upSide
            downSide  = downSide
            frontSide = frontSide
            backSide  = backSide
            leftSide  = leftSide
            rightSide = rightSide
        } = cube

        render2dSide upSide    (col + 3) (row + 0)
        render2dSide downSide  (col + 3) (row + 6)
        render2dSide frontSide (col + 3) (row + 3)
        render2dSide backSide  (col + 9) (row + 3)
        render2dSide leftSide  (col + 0) (row + 3)
        render2dSide rightSide (col + 6) (row + 3)
