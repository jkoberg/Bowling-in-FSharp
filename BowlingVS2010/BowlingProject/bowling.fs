module bowling

type ThrowResult = Drop of int

type FrameType = Strike | Spare | Normal

type RollResult =
  | ClosedFrame of FrameType * int * list<Roll>
  | OpenFrame of list<Roll>

type Frame = Frame of int * FrameType * int

type GameResult =
  | FinishedGame of int * list<Frame>
  | UnfinishedGame of string * list<Frame>
  

let score throws =
  match throws with
  | Drop 10 :: (Drop b1 ::  Drop b2 :: _ as upcoming)                   -> ClosedFrame(Strike, 10+b1+b2, upcoming)
  | Drop r1 ::  Drop r2 :: (Drop b1 :: _ as upcoming)  when r1+r2 = 10  -> ClosedFrame(Spare,  r1+r2+b1, upcoming)
  | Drop r1 ::  Drop r2 :: (_ as upcoming)                              -> ClosedFrame(Normal, r1+r2,    upcoming)
  | (_ as upcoming)                                                     -> OpenFrame(upcoming)
  
let rec gameloop total framecount results throws =
  if framecount >= 10 
  then FinishedGame(total, List.rev results)
  else match (score throws) with
       | OpenFrame(_)
         -> UnfinishedGame("Ran out of throws", List.rev results)

       | ClosedFrame(typ, score, upcoming) as result
         -> let result = Frame(framecount, typ, score)
            gameloop (total+score) (framecount+1) (result::results) upcoming


let bowling_game = gameloop 0 0 []


