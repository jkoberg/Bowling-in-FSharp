module bowling

type Throw = Drop of int

type FrameType = Strike | Spare | Open

type Frame = Frame of int * FrameType * int

type ThrowResult =
  | CompletedFrame of FrameType * int * list<Throw>
  | IncompleteFrame of list<Throw>

type GameResult =
  | FinishedGame of int * list<Frame>
  | UnfinishedGame of string * list<Frame>
  

let score throws =
  match throws with
  | Drop 10 :: (Drop b1 ::  Drop b2 :: _ as upcoming)                  -> CompletedFrame(Strike, 10+b1+b2, upcoming)
  | Drop r1 ::  Drop r2 :: (Drop b1 :: _ as upcoming) when r1+r2 = 10  -> CompletedFrame(Spare,  r1+r2+b1, upcoming)
  | Drop r1 ::  Drop r2 :: (_ as upcoming)                             -> CompletedFrame(Open, r1+r2,    upcoming)
  | (_ as upcoming)                                                    -> IncompleteFrame(upcoming)

let rec gameloop framecount results throws =
  if framecount > 10 then
    let total = Seq.sum <| seq { for Frame(_,_,score) in results -> score }
    FinishedGame(total, List.rev results)
  else
    match (score throws) with
       | IncompleteFrame(_)
         -> UnfinishedGame("Ran out of throws", List.rev results)
       | CompletedFrame(typ, score, upcoming)
         let result = Frame(framecount, typ, score)
         -> gameloop (framecount+1) (result::results) upcoming


let score_bowling_game = gameloop 1 []
