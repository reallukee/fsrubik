(*
    -------
    FsRubik
    -------

    F#, paradigma funzionale e cubo di Rubik

    File name   : Move.fs

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

    let rotateClockwise side =
        let {
            stickers = currentStickers
        } = side

        let stickers =
            [|
                currentStickers[6]; currentStickers[3]; currentStickers[0]
                currentStickers[7]; currentStickers[4]; currentStickers[1]
                currentStickers[8]; currentStickers[5]; currentStickers[2]
            |]

        { side with
            stickers = stickers
        }

    let rotateCounterClockwise side =
        let {
            stickers = currentStickers
        } = side

        let stickers =
            [|
                currentStickers[2]; currentStickers[5]; currentStickers[8]
                currentStickers[1]; currentStickers[4]; currentStickers[7]
                currentStickers[0]; currentStickers[3]; currentStickers[6]
            |]

        { side with
            stickers = stickers
        }



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



    let horizontalIndices layer =
        match layer with
        | Top    -> [| 0; 1; 2 |]
        | Middle -> [| 3; 4; 5 |]
        | Bottom -> [| 6; 7; 8 |]

    let verticalIndices layer =
        match layer with
        | Start  -> [| 0; 3; 6 |]
        | Center -> [| 1; 4; 7 |]
        | End    -> [| 2; 5; 8 |]

    let indices layer =
        match layer with
        | Horizontal layer -> horizontalIndices layer
        | Vertical   layer -> verticalIndices   layer



    let horizontalLayer side layer =
        let {
            stickers = stickers
        } = side

        let indices = horizontalIndices layer

        indices
        |> Array.map (fun index ->
            stickers[index]
        )

    let verticalLayer side layer =
        let {
            stickers = stickers
        } = side

        let indices = verticalIndices layer

        indices
        |> Array.map (fun index ->
            stickers[index]
        )

    let layer side layer =
        match layer with
        | Horizontal layer -> horizontalLayer side layer
        | Vertical   layer -> verticalLayer   side layer



    let updateHorizontalLayer sourceSide targetSide layer =
        let {
            stickers = targetStickers
        } = targetSide

        let indices = horizontalIndices layer

        let layer = horizontalLayer sourceSide layer

        let stickers =
            targetStickers
            |> Array.mapi (fun index item ->
                match Array.tryFindIndex ((=) index) indices with
                | Some index -> layer[index]
                | None -> item
            )

        { targetSide with
            stickers = stickers
        }

    let updateVerticalLayer sourceSide targetSide layer =
        let {
            stickers = targetStickers
        } = targetSide

        let indices = verticalIndices layer

        let layer = verticalLayer sourceSide layer

        let stickers =
            targetStickers
            |> Array.mapi (fun index item ->
                match Array.tryFindIndex ((=) index) indices with
                | Some index -> layer[index]
                | None -> item
            )

        { targetSide with
            stickers = stickers
        }

    let updateLayer targetSide targetLayer sourceSide sourceLayer =
        let {
            stickers = targetStickers
        } = targetSide

        let indices = indices targetLayer

        let layer = layer sourceSide sourceLayer

        let stickers =
            targetStickers
            |> Array.mapi (fun index item ->
                match Array.tryFindIndex ((=) index) indices with
                | Some index -> layer[index]
                | None -> item
            )

        { targetSide with
            stickers = stickers
        }



    let updateTopLayer sourceSide targetSide =
        updateHorizontalLayer sourceSide targetSide Top

    let updateMiddleLayer sourceSide targetSide =
        updateHorizontalLayer sourceSide targetSide Middle

    let updateBottomLayer sourceSide targetSide =
        updateHorizontalLayer sourceSide targetSide Bottom



    let updateStartLayer sourceSide targetSide =
        updateVerticalLayer sourceSide targetSide Start

    let updateCenterLayer sourceSide targetSide =
        updateVerticalLayer sourceSide targetSide Center

    let updateEndLayer sourceSide targetSide =
        updateVerticalLayer sourceSide targetSide End



    (*
        Implementa la mossa U
    *)
    let u cube =
        let {
            upSide    = upSide
            frontSide = frontSide
            backSide  = backSide
            leftSide  = leftSide
            rightSide = rightSide
        } = cube

        { cube with
            upSide    = rotateCounterClockwise upSide
            frontSide = updateTopLayer rightSide frontSide
            backSide  = updateTopLayer leftSide  backSide
            leftSide  = updateTopLayer frontSide leftSide
            rightSide = updateTopLayer backSide  rightSide
        }

    (*
        Implementa la mossa U'
    *)
    let u' cube =
        let {
            upSide    = upSide
            frontSide = frontSide
            backSide  = backSide
            leftSide  = leftSide
            rightSide = rightSide
        } = cube

        { cube with
            upSide    = rotateClockwise upSide
            frontSide = updateTopLayer leftSide  frontSide
            backSide  = updateTopLayer rightSide backSide
            leftSide  = updateTopLayer backSide  leftSide
            rightSide = updateTopLayer frontSide rightSide
        }

    (*
        Implementa la mossa D
    *)
    let d cube =
        let {
            downSide  = downSide
            frontSide = frontSide
            backSide  = backSide
            leftSide  = leftSide
            rightSide = rightSide
        } = cube

        { cube with
            downSide  = rotateClockwise downSide
            frontSide = updateBottomLayer leftSide  frontSide
            backSide  = updateBottomLayer rightSide backSide
            leftSide  = updateBottomLayer backSide  leftSide
            rightSide = updateBottomLayer frontSide rightSide
        }

    (*
        Implementa la mossa D'
    *)
    let d' cube =
        let {
            downSide  = downSide
            frontSide = frontSide
            backSide  = backSide
            leftSide  = leftSide
            rightSide = rightSide
        } = cube

        { cube with
            downSide  = rotateCounterClockwise downSide
            frontSide = updateBottomLayer rightSide frontSide
            backSide  = updateBottomLayer leftSide  backSide
            leftSide  = updateBottomLayer frontSide leftSide
            rightSide = updateBottomLayer backSide  rightSide
        }

    (*
        Implementa la mossa R
    *)
    let r cube =
        let {
            upSide    = upSide
            downSide  = downSide
            frontSide = frontSide
            backSide  = backSide
            rightSide = rightSide
        } = cube

        { cube with
            upSide    = updateEndLayer frontSide upSide
            downSide  = updateEndLayer backSide  downSide
            frontSide = updateEndLayer downSide  frontSide
            backSide  = updateEndLayer upSide    backSide
            rightSide = rotateClockwise rightSide
        }

    (*
        Implementa la mossa R'
    *)
    let r' cube =
        let {
            upSide    = upSide
            downSide  = downSide
            frontSide = frontSide
            backSide  = backSide
            rightSide = rightSide
        } = cube

        { cube with
            upSide    = updateEndLayer backSide  upSide
            downSide  = updateEndLayer frontSide downSide
            frontSide = updateEndLayer upSide    frontSide
            backSide  = updateEndLayer downSide  backSide
            rightSide = rotateCounterClockwise rightSide
        }

    (*
        Implementa la mossa L
    *)
    let l cube =
        let {
            upSide    = upSide
            downSide  = downSide
            frontSide = frontSide
            backSide  = backSide
            leftSide  = leftSide
        } = cube

        { cube with
            upSide    = updateStartLayer backSide  upSide
            downSide  = updateStartLayer frontSide downSide
            frontSide = updateStartLayer upSide    frontSide
            backSide  = updateStartLayer downSide  backSide
            leftSide  = rotateClockwise leftSide
        }

    (*
        Implementa la mossa L'
    *)
    let l' cube =
        let {
            upSide    = upSide
            downSide  = downSide
            frontSide = frontSide
            backSide  = backSide
            leftSide  = leftSide
        } = cube

        { cube with
            upSide    = updateStartLayer frontSide upSide
            downSide  = updateStartLayer backSide  downSide
            frontSide = updateStartLayer downSide  frontSide
            backSide  = updateStartLayer upSide    backSide
            leftSide  = rotateCounterClockwise leftSide
        }

    (*
        Implementa la mossa F
    *)
    let f cube =
        let {
            upSide    = upSide
            downSide  = downSide
            frontSide = frontSide
            leftSide  = leftSide
            rightSide = rightSide
        } = cube

        { cube with
            upSide    = updateLayer upSide   (Horizontal Bottom) leftSide  (Vertical End)
            downSide  = updateLayer downSide (Horizontal Top)   rightSide (Vertical Start)
            frontSide = rotateClockwise frontSide
            leftSide  = updateLayer leftSide  (Vertical End)   downSide (Horizontal Top)
            rightSide = updateLayer rightSide (Vertical Start) upSide   (Horizontal Bottom)
        }

    (*
        Implementa la mossa F'
    *)
    let f' cube =
        let {
            upSide    = upSide
            downSide  = downSide
            frontSide = frontSide
            leftSide  = leftSide
            rightSide = rightSide
        } = cube

        { cube with
            upSide    = updateLayer upSide   (Horizontal Bottom) rightSide (Vertical End)
            downSide  = updateLayer downSide (Horizontal Top)    leftSide  (Vertical Start)
            frontSide = rotateCounterClockwise frontSide
            leftSide  = updateLayer leftSide  (Vertical End)   upSide   (Horizontal Top)
            rightSide = updateLayer rightSide (Vertical Start) downSide (Horizontal Bottom)
        }

    (*
        Implementa la mossa B
    *)
    let b cube =
        let {
            upSide    = upSide
            downSide  = downSide
            backSide  = backSide
            leftSide  = leftSide
            rightSide = rightSide
        } = cube

        { cube with
            upSide    = updateLayer upSide   (Horizontal Top)   rightSide (Vertical End)
            downSide  = updateLayer downSide (Horizontal Bottom) leftSide (Vertical Start)
            backSide  = rotateClockwise backSide
            leftSide  = updateLayer leftSide  (Vertical Start) upSide   (Horizontal Top)
            rightSide = updateLayer rightSide (Vertical End)   downSide (Horizontal Bottom)
        }

    (*
        Implementa la mossa B'
    *)
    let b' cube =
        let {
            upSide    = upSide
            downSide  = downSide
            backSide  = backSide
            leftSide  = leftSide
            rightSide = rightSide
        } = cube

        { cube with
            upSide    = updateLayer upSide   (Horizontal Top)    leftSide  (Vertical End)
            downSide  = updateLayer downSide (Horizontal Bottom) rightSide (Vertical Start)
            backSide  = rotateClockwise backSide
            leftSide  = updateLayer leftSide  (Vertical Start) downSide (Horizontal Top)
            rightSide = updateLayer rightSide (Vertical End)   upSide   (Horizontal Bottom)
        }



    let applyMove cube move =
        match move with
        | U  -> u cube
        | U' -> u' cube
        | D  -> d cube
        | D' -> d' cube
        | R  -> r cube
        | R' -> r' cube
        | L  -> l cube
        | L' -> l' cube
        | F  -> f cube
        | F' -> f' cube
        | B  -> b cube
        | B' -> b' cube

    let randomMove () =
        let random = Random()

        let moves = [|
            U;
            U'
            D;
            D'
            R;
            R'
            L;
            L'
            F;
            F'
            B;
            B'
        |]

        let move = random.Next(Array.length moves)

        moves[move]
