using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Runtime.Serialization;

namespace RPSGame
{
    class Program
    {
        static void Main(string[] args)
        {
            var game = new RPSGame();

            dynamic moves = JsonConvert.DeserializeObject(@"
                [[""Armando"",""P""],[""Dave"",""S""]]
            ");
                        
            var result1 = game.rps_game_winner(moves);

            Console.WriteLine(result1);
            Console.WriteLine("");

            dynamic tournament = JsonConvert.DeserializeObject(@"
            [
                [
                    [[""Armando"", ""P""], [""Dave"", ""S""]],
                    [[""Richard"", ""R""], [""Michael"", ""S""]],
                ],
                [
                    [[""Allen"", ""S""], [""Omer"", ""P""]],
                    [[""David E."", ""R""], [""Richard X."", ""P""]]
                ]
            ]
            ");
            var result2 = game.rps_tournament_winner(tournament);
            Console.WriteLine("The champion is " + result2);

            Console.ReadKey();
        }


    }
}
