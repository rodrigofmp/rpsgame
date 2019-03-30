using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RPSGame
{
    public class RPSGame
    {
        private static string PAPER = "P";
        private static string ROCK = "R";
        private static string SCISSORS = "S";
        private static string DRAW = "D";
        private static string WON = "W";
        private static string LOSE = "L";

        [Serializable]
        public class NoSuchStrategyError : Exception { }

        [Serializable]
        public class WrongNumberOfPlayersError : Exception { }

        public dynamic rps_game_winner(dynamic moves)
        {
            // VALIDATE INPUT

            if (moves.Count != 2)
                throw new WrongNumberOfPlayersError();

            var player1 = moves[0][0];
            var strategy1 = (string)moves[0][1];
            strategy1 = strategy1.ToUpper();

            var player2 = moves[1][0];
            var strategy2 = (string)moves[1][1];
            strategy2 = strategy2.ToUpper();

            if (strategy1 != ROCK
                && strategy1 != PAPER
                && strategy1 != SCISSORS)
                throw new NoSuchStrategyError();

            if (strategy2 != ROCK
                && strategy2 != PAPER
                && strategy2 != SCISSORS)
                throw new NoSuchStrategyError();

            // PROCESS THE WINNER

            var resultMove1 = "";

            if (strategy1 == strategy2)
            {
                resultMove1 = DRAW;
            }
            else if (strategy1 == ROCK && strategy2 == SCISSORS)
            {
                resultMove1 = WON;
            }
            else if (strategy1 == SCISSORS && strategy2 == PAPER)
            {
                resultMove1 = WON;
            }
            else if (strategy1 == PAPER && strategy2 == ROCK)
            {
                resultMove1 = WON;
            }
            else
            {
                resultMove1 = LOSE;
            }

            // LOG RESULT

            var p1 = player1;
            var p2 = strategy1;
            var p3 = player2;
            var p4 = strategy2;
            var p5 = resultMove1 == LOSE ? player2 : player1;
            var p6 = resultMove1 == LOSE ? strategy2 : strategy1;
            var p7 = resultMove1 == DRAW ? "=" : ">";
            var p8 = resultMove1 == LOSE ? strategy1 : strategy2;

            Console.WriteLine(string.Format("[[\"{0}\", \"{1}\"], [\"{2}\", \"{3}\"]] # {4} would win since {5} {6} {7}", p1, p2, p3, p4, p5, p6, p7, p8));
            
            // RESULT

            if (resultMove1 == WON || resultMove1 == DRAW)
            {
                return moves[0];
            }
            else
            {
                return moves[1];
            }
        }

        public dynamic rps_tournament_winner(dynamic tournament)
        {
            var matches = new List<string>();

            // CONVERT AN INFINITY DEEP LIST IN SOMETHING MORE EASY TO UNDERSTAND
            // Armando
            // P
            // Dave
            // S
            // ETC.

            var level = tournament;
            while (level != null)
            {
                level = get_next_level(level, ref matches);
            }

            // PROCESS THE MATCHES AND RETURN NEW ROUND

            while (matches.Count > 2)
            {
                matches = get_next_round(matches);
            }

            var json = JsonConvert.SerializeObject(matches);

            return json;
        }

        private dynamic get_next_level(dynamic level, ref List<string> matches)
        {
            foreach (var item in level)
            {
                if (item.Type == Newtonsoft.Json.Linq.JTokenType.String)
                {
                    matches.Add((string)item);
                }
                else
                {
                    get_next_level(item, ref matches);
                }
            }            
            return null;
        }

        private List<string> get_next_round(List<string> matches)
        {
            var nextRound = new List<string>();

            for (int i = 0; i < matches.Count; i += 4)
            {
                Console.WriteLine(matches[i] + matches[i + 1] + " vs " + matches[i + 2] + matches[i + 3]);

                var move1 = new List<dynamic>();
                move1.Add(matches[i]);
                move1.Add(matches[i + 1]);

                var move2 = new List<dynamic>();
                move2.Add(matches[i + 2]);
                move2.Add(matches[i + 3]);

                var match = new List<dynamic>();
                match.Add(move1);
                match.Add(move2);

                var winner = rps_game_winner(match);

                nextRound.Add(winner[0]);
                nextRound.Add(winner[1]);
            }

            return nextRound;
        }
    }
}
