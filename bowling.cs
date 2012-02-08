
using System;
public class BowlingProgram {
	public static Tuple<int, List<int>> score_frame( List<int> remaining_throws ) {
		if( remaining_throws[0] == 10 && remaining_throws.Length >= 3) {
			var score = 10 + remaining_throws[1] + remaining_throws[2];
			return Tuple.Create(score, remaining_throws.Skip(1));
			}
		if( remaining_throws.Length >= 3 && remaining_throws[0]+remaining_throws[1] == 10) {
			var score = 10 + remaining_throws[2];
			return Tuple.Create(score, remaining_throws.Skip(2));
			}
		if( remaining_throws.Length >= 2) {
			var score = remaining_throws[0]+remaining_throws[1];
			return Tuple.Create(score, remaining_throws.Skip(2));
			}
		}

	public static int score_game(int frame_number, List<int> throws) {
		if(frame_number == 10) {
			return 0;
			}
		else {
			var score_tup = score_frame(throws);
			var score = score_tup.Item1;
			var remaining_throws = score_tup.Item2;
			return score + score_game(frame_number+1, remaining_throws);
			}
		}

	public static int bowl(List<int> throwlist) {
		var zeros = new List<int>() { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 };
		return score_game(0, throwlist.Concat(zeros));
		}
	}



