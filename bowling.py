

def score_frame(remaining_throws):
	if (remaining_throws[0] == 10 and len(remaining_throws) >= 3):
                score = 10 + remaining_throws[1] + remaining_throws[2]
                return (score, remaining_throws[1:])
        
	if (len(remaining_throws) >= 3 and remaining_throws[0] + remaining_throws[1] == 10):
                score = 10 + remaining_throws[2]
                return (score, remaining_throws[2:])
        
	if len(remaining_throws) >= 2:
                score = remaining_throws[0] + remaining_throws[1]
                return (score, remaining_throws[2:])


def score_game(frame_number, throws):
        if frame_number == 10:
            return 0
        else:
            score, remaining_throws = score_frame(throws)
            return score + score_game(frame_number + 1, remaining_throws)


def bowl(throwlist):
        return score_game(0, throwlist)




