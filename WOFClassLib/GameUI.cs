using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace WOFClassLib
{
    public class GameUI
    {

        public void SetPlayerTextColor(int playerNumber)
        {
            switch (playerNumber % 4)
            {
                case 0: Console.ForegroundColor = ConsoleColor.Cyan; break;
                case 1: Console.ForegroundColor = ConsoleColor.Yellow; break;
                case 2: Console.ForegroundColor = ConsoleColor.Green; break;
                default: Console.ForegroundColor = ConsoleColor.Magenta; break;
            }
        }
        public void ResetTextColor()
        {
            Console.ForegroundColor = ConsoleColor.White;
        }

        public List<string> GetPlayerNames()
        {
            bool valid = false;
            int totalPlayers;
            List<string> names = new List<string>();

            do
            {
                Console.Write("\n \n Welcome to Wheel of Fortune \n   sponsored by Azure Disaster LLC. \n\n How many players would you like to begin with? ");
                string input = Console.ReadLine();
                valid = Int32.TryParse(input, out totalPlayers) && totalPlayers >= 1 ? Int32.TryParse(input, out totalPlayers) : false;
            } while (!valid);

            if (totalPlayers==1)
            {
                names.Add("Player");
                return names;
            }

            for (int i = 0; i < totalPlayers; i++)
            {
                Console.Write(" Hey player {0} What's your name? ", i + 1);
                string name = Console.ReadLine();
                names.Add((name=="") ? "Player"+i : name);
            }
            return names;
        }

        public char GetLetter()
        {
            bool validGuess = false;
            string guess = "";

            while (!validGuess)
            {
                Console.Write(" Guess a single letter: ");
                guess = Console.ReadLine();
                //Console.WriteLine("\nYou guessed: {0}! \n", guess);
                validGuess = Regex.IsMatch(guess, "^[a-zA-Z]") && guess.Length == 1;
            }
            return guess[0];

        }

        public string GetSolution()
        {
            bool validGuess = false;
            string guess = "";

            while (!validGuess)
            {
                Console.Write(" Enter your guess: ");
                guess = Console.ReadLine();
                //Console.WriteLine("\nYou guessed: {0}! \n", guess);
                validGuess = Regex.IsMatch(guess, "^[a-zA-Z ]+$");
            }
            return guess;
        }


        public bool PlayerWantsToSolve()
        {
            bool validAnswer = false;
            string answer = "";

            while (!validAnswer)
            {
                Console.Write(" Would you like to solve the puzzle? (Y/N): ");
                answer = Console.ReadLine().Trim();
                validAnswer = Regex.IsMatch(answer, "^[yYnN]$");
            }
            return answer=="Y" || answer=="y";

        }

        public void GetExit()
        {
            Console.Write("\n\n Press any key to exit: ");
            Console.ReadKey();
        }

        public void DisplaySpinAmount(int spinAmount)
        {
            Console.Write($" Your spin amount is ${spinAmount}...");
        }

        public void DisplayGuessLetterResults(int matches, Puzzle puzzle, Player player)
        {
            string were = (matches == 1) ? "was" : "were";
            string es = (matches == 1) ? "" : "es";

            Console.WriteLine($"   There {were} {matches} match{es}.");
            if (matches > 0)
            {
                Console.WriteLine($"   You now have ${player.RoundMoney} for the round.");
                DisplayPuzzle(puzzle);
            }
        }

        public void DisplaySolvePuzzleResults(bool solvedIt, Puzzle puzzle)
        {
            if (solvedIt)
            {
                Console.WriteLine("\n YAYYYY! You solved it!");
                DisplayPuzzle(puzzle);
            }
            else
            {
                Console.WriteLine("   Sorry, that is incorrect...\n");
            }
        }

        public void DisplayPuzzle(Puzzle puzzle)
        {
            Console.WriteLine("\n " + puzzle.GetPuzzleDisplay()+"\n");
        }

        public void DisplayPlayersTurn(Player player)
        {
            Console.WriteLine($"\n Hey {player.Name}! Now it's your turn...\n");
        }

        public void DisplayRoundWinner(Player player, int numPlayers)
        {
            if (numPlayers > 1)
                Console.WriteLine($"\n {player.Name} wins the round and ${player.RoundMoney}!\n");
            else
                Console.WriteLine($"\n You win the round and ${player.RoundMoney}!\n");
        }

        public void DisplayWinner(List<Player> players)
        {
            Console.WriteLine();
            Console.WriteLine();
            int maxMoney = players.Select(p => p.TotalMoney).Max();
            foreach (var player in players)
            {
                string winner = (player.TotalMoney == maxMoney) ? "(winner)" : "";
                Console.WriteLine($" {player.Name}'s final money is ${player.TotalMoney} {winner}");
            }
            
        }
    }
}
