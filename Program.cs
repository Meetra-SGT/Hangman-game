using System;

namespace Hangman
{
    class Program
    {
        static void Main(string[] args)
        {
            Launch launch = new Launch();
            launch.PrintGreeting(); //printing game description
            Console.WriteLine(); //break
            launch.ChoosingCategory();  //choosing category
            launch.GetRandomWord(); //choosing the word to guess
            Game game = new Game();
            game.PrintUnderlines(launch.WordToGuess);  // printing underlines for the selected word
            Console.WriteLine(); //break
            Console.WriteLine(); //break
            game.AddLettersToTheGuessingList(launch.WordToGuess);  //giving the word to the Game class
            game.GuessingPart(launch.WordToGuess);
            Console.WriteLine(); //break
            Console.WriteLine(); //break
        }
    }
}
