module bowling_tests

open bowling

type TestResult =
  | Success
  | Failure

let bowling_test  expected_score  rolls =
  let result = bowling_game rolls
  printfn "%A" result
  match result with
  | FinishedGame(expected_score, _) -> Success
  | _ -> Failure


let repeat items = seq { while true do yield! items }

let makeListOf count items = repeat items |> Seq.take count |> List.ofSeq

open NUnit.Framework


[<TestFixture>]
type BowlingTests() =

    member self.test(expected_score, roll_list) =
      let result = bowling_test expected_score roll_list
      assert (result = Success)

    [<Test>]
    member self.test_dutch() = self.test(200, makeListOf 17 [Roll 5; Roll 5; Roll 10])

    [<Test>]
    member self.test_perfect() = self.test(300, makeListOf 12 [Roll 10])
    
    [<Test>]
    member self.test_normal() = self.test(60, makeListOf 20 [Roll 3])
    
    [<Test>]
    member self.test_short() = self.test(100, makeListOf 5 [Roll 3])
    
    [<Test>]
    member self.test_gutter() = self.test(0, makeListOf 20 [Roll 0])