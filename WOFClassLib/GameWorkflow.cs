using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOFClassLib
{
    public class GameWorkflow
    {

        public string HardcodedPuzzle = "";

        Phrase phrase = new Phrase();
        GameUI ui = new GameUI();
        List<Player> players = new List<Player>();
        int currentPlayer = 0;

        void TakeTurn(Player player, Puzzle puzzle, out bool puzzleIsSolved)
        {
            puzzleIsSolved = false;
            int spinAmount;
             
            while (true)
            {
                // TODO: spin the wheel
                spinAmount = 100;
                ui.DisplaySpinAmount(spinAmount);

                // player guesses a letter
                int matches = player.GuessLetter(ui.GetLetter(), puzzle, spinAmount);
                ui.DisplayGuessLetterResults(matches, puzzle, player); ;

                // guess did not match any letters, turn ends
                if (matches == 0) return;

                // after guessing a letter, the player can try to solve the puzzle
                if (ui.PlayerWantsToSolve())
                {
                    puzzleIsSolved = player.SolvePuzzle(ui.GetSolution(), puzzle);
                    ui.DisplaySolvePuzzleResults(puzzleIsSolved, puzzle);
                    return;
                }
            }
        }

        void PlayRound()
        {

            // grab a new puzzle
            var puzzle = new Puzzle((HardcodedPuzzle=="") ? phrase.GetPhrase(): HardcodedPuzzle);
            bool puzzleIsSolved;

            // intialize the player round money
            foreach (var player in players) player.NewRound();
            Player roundWinner = players[currentPlayer]; ;

            if (players.Count==1) ui.DisplayPuzzle(puzzle);

            // Each player takes turns trying to solve the puzzle until it is solved
            do
            {
                // for multi-player games, display the player name and use a different text color for each player
                if (players.Count > 1)
                {
                    ui.SetPlayerTextColor(currentPlayer);
                    ui.DisplayPlayersTurn(players[currentPlayer]);
                    ui.DisplayPuzzle(puzzle);
                }

                TakeTurn(players[currentPlayer], puzzle, out puzzleIsSolved);

                if (puzzleIsSolved) roundWinner = players[currentPlayer];

                // advance to the next player
                currentPlayer = (currentPlayer + 1) % players.Count;

            } while (!puzzleIsSolved);

            ui.DisplayRoundWinner(roundWinner, players.Count);
        }

        public void Start(int numberOfRounds=1)
        {
            List<string> playerNames = ui.GetPlayerNames();

            foreach (var name in playerNames) players.Add(new Player(name));

            for (int i = 0; i < numberOfRounds; i++)
            {
                PlayRound();
            }

            ui.ResetTextColor();
            ui.DisplayWinner(players);

            ui.GetExit();
        }


    }

}
