
open System

let zeros = [0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;]

let score_frame = function
  | 10 :: (next :: nextnext :: _ as rest_of_game)
    -> 10 + next + nextnext, rest_of_game
  | thisthrow :: next :: (nextnext :: _ as rest_of_game)  when (thisthrow + next = 10)
    -> 10 + nextnext, rest_of_game
  | thisthrow :: next :: rest_of_game
    -> thisthrow + next, rest_of_game


let rec score_game frame_number throws_to_score =
  if frame_number = 10 then 0
  else
       let (n, rest_of_game) = (score_frame throws_to_score) in
        n + (score_game (frame_number + 1) rest_of_game)


let bowl throwlist = score_game 0 throwlist

;;


Console.WriteLine "Should be 0" ;

Console.WriteLine (bowl [0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0;0] );

Console.WriteLine "Should be 300" ;

Console.WriteLine  (bowl [10;10;10;10;10;10;10;10;10;10;10;10]);



Console.WriteLine "Should be 200" ;

Console.WriteLine  (bowl [5;5; 10; 5;5; 10; 5;5; 10; 5;5; 10; 5;5; 10; 5;5; 10;  5;5; 10; 5;5; 10;10;10;10;10]);

;;




Console.ReadLine ()


