module bowling

type Roll = Roll of int

type FrameType =
  | Strike
  | Spare
  | Normal

type ScoreResult =
  | Frame of FrameType * int * list<Roll>
  | RollAgain of list<Roll>

type FrameResult = FrameResult of int * ScoreResult

type GameResult =
  | FinishedGame of int * list<FrameResult>
  | UnfinishedGame of string * list<FrameResult>
  

let score nextrolls =
  match nextrolls with
  | Roll r1 :: (Roll b1 ::  Roll b2 :: _ as more)  when r1 = 10       -> Frame(Strike, 10 + b1 + b2, more)
  | Roll r1 ::  Roll r2 :: (Roll b1 :: _ as more)  when r1 + r2 = 10  -> Frame(Spare, r1 + r2 + b1, more)
  | Roll r1 ::  Roll r2 :: (_ as more)                                -> Frame(Normal, r1 + r2, more)
  | ( _ as more)                                                      -> RollAgain(more)
  

let rec gameloop totalscore framenumber resultlist rollsleft =
  let framenumber = framenumber + 1
  if framenumber > 10 
  then FinishedGame(totalscore, List.rev resultlist)
  else match score rollsleft with
       | RollAgain(unscored_rolls) -> UnfinishedGame("Ran out of rolls", List.rev resultlist)
       | Frame(_, framescore, remainder) as result ->
         let newscore = totalscore + framescore
         let newresults = FrameResult(framenumber, result) :: resultlist
         gameloop newscore framenumber newresults remainder


let bowling_game = gameloop 0 0 []


